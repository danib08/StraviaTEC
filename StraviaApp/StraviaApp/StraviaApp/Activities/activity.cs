using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Essentials;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using StraviaApp.Models;
using Android.Content;

namespace StraviaApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    //for google map,    for gps location
    public class activity : AppCompatActivity, IOnMapReadyCallback, ILocationListener
    {
        // <summary>
        // Google maps
        // </summary>
        private GoogleMap mMap;
        private LocationManager locationManager;
        private string locationProvider;
        private LatLng lastPosition;
        private LatLng myPosition;

        private TextView textTotalDistance;
        private TextView textVelocidad;
        private TextView txtTimer;
        private Button buttonStop;
        private Button buttonFinish;
        private Timer timer;

        private List<Gpx> activityStats;
        private bool stop;
        private bool first;
        private double totalDistance;
        private int hour = 0, min = 0, sec = 0, velocidad = 0;
        private string trkpt;
        private string user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity);

            //Set all variable of the interface
            this.textVelocidad = FindViewById<TextView>(Resource.Id.textVelocidad);
            this.textTotalDistance = FindViewById<TextView>(Resource.Id.textDistance);
            this.buttonFinish = FindViewById<Button>(Resource.Id.finishActivity);
            this.buttonStop = FindViewById<Button>(Resource.Id.stopActivity);
            this.txtTimer = FindViewById<TextView>(Resource.Id.txtTimer);
            this.trkpt = "";
            this.stop = true;
            this.first = true;
            this.totalDistance = 0;
            this.activityStats = new List<Gpx>();
            this.user = Intent.GetStringExtra("idUser");

            //Set the google map
            SetUpMap();
            //Set the manager location of the user
            InitializeLocationManager();

            //chronometer 
            timer = new Timer();
            timer.Interval = 1000; // 1 second  
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            buttonFinish.Click += (sender, e) => {
                OnPause();
                string timer = DateTime.Now.ToString("s");
                //Create the GPX data
                for (int i = 0; i < this.activityStats.Count - 1; i++)
                {
                    if (this.activityStats[i].lat.Equals("") || this.activityStats[i].lon.Equals("") || this.activityStats[i].ele.Equals(""))
                    {
                        this.trkpt += "";
                    }
                    else
                    {
                        this.trkpt +=
                        "<trkpt" + "lat= " + this.activityStats[i].lat.ToString() + " lon= " + this.activityStats[i].lon.ToString() + ">"
                        + "<ele>" + this.activityStats[i].ele.ToString() + "</ele>"
                        + "<time>" + this.activityStats[i].time + "Z</time>"
                        + "</trkpt>";
                    }
                }
                string gpx = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<gpx version=\"1.0\" creator = Runkeeper-http://www.runkeeper.com"
                + "xmlns: xsi =\"http://www.w3.org/2001/XMLSchema-instance\""
                + "xmlns=\"http://www.topografix.com/GPX/1/1\""
                + "xsi: schemaLocation =\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd"
                + "xmlns:gpxtpx=\"http://www.garmin.com/xmlschemas/TrackPointExtension/v1\""
                + "<metadata><time>" + timer + "Z</time></metadata>"
                + "<trk>"
                + "<name>Activity Name</name>"
                + "<trkseg>"
                + this.trkpt
                + "</trkseg>"
                + "</trk>"
                + "</gpx>";

                string totalTime = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();

                Intent intent = new Intent(this, typeof(homeActivity));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                intent.PutExtra("totalDistance", this.totalDistance.ToString());
                intent.PutExtra("totalTime", totalTime);
                intent.PutExtra("gpx", gpx);
                intent.PutExtra("idUser", user);
                StartActivity(intent);
                Finish();

            };
            buttonStop.Click += (sender, e) => {
                if (stop)
                {
                    OnPause();
                    this.stop = false;
                }
                else
                {

                    OnResume();
                    this.stop = true;
                }
            };
        }
        //Method for the chronometer 
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;
            velocidad++;
            if (sec == 60)
            {
                min++;
                sec = 0;
                double res = this.totalDistance / (velocidad);
                this.textVelocidad.Text = res.ToString("0.00");
            }
            if (min == 60)
            {
                hour++;
                min = 0;
            }
            RunOnUiThread(() => { txtTimer.Text = $"{hour}:{min}:{sec}"; });
        }

        //Initialize the location manager
        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }

        }
        //Initialize the google maps freagment
        private void SetUpMap()
        {
            if (mMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapGoogle).GetMapAsync(this);
            }
        }
        //Initialize the google maps
        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            mMap.MyLocationEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

        }
        //Pause the aplication
        protected override void OnPause()
        {
            base.OnPause();
            this.timer.Stop();
            locationManager.RemoveUpdates(this);
        }
        //Resume the aplication
        protected override void OnResume()
        {
            base.OnResume();
            this.timer.Start();
            locationManager.RequestLocationUpdates(locationProvider, 2000, 1, this);
        }
        //Adjust map values
        public void OnLocationChanged(Android.Locations.Location location)
        {
            Double lat, lng, ele;
            lat = location.Latitude;
            lng = location.Longitude;
            ele = location.Altitude;
            string timer = DateTime.Now.ToString("s");
            this.activityStats.Add(new Gpx(lat, lng, ele, timer));

            //Initial position
            if (this.first)
            {
                this.myPosition = new LatLng(lat, lng);
                this.lastPosition = this.myPosition;
                this.first = false;
            }
            else
            {
                //Current position
                this.lastPosition = this.myPosition;
                this.myPosition = new LatLng(lat, lng);

                double distance = Xamarin.Essentials.Location.CalculateDistance(lastPosition.Latitude, lastPosition.Longitude, myPosition.Latitude, myPosition.Longitude, DistanceUnits.Kilometers);
                this.totalDistance += distance;
                this.textTotalDistance.Text = this.totalDistance.ToString("0.00");

                Polyline line = mMap.AddPolyline(new PolylineOptions().Add(lastPosition, myPosition));

                CameraUpdate camUpdate = CameraUpdateFactory.NewLatLngZoom(myPosition, 17);
                mMap.MoveCamera(camUpdate);
            }

        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}