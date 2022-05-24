using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Essentials;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using AppMobile.Models;

namespace AppMobile.Activities
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

        private bool stop;
        private bool first;
        
        private double totalDistance;
        private TextView textTotalDistance;
        private TextView textVelocidad;
        private TextView txtTimer;
        private Button buttonStop;
        private Button buttonFinish;
        private Timer timer;
        private List<Gpx> totalRoute;
        private int hour = 0, min = 0, sec = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity);

            this.textVelocidad = FindViewById<TextView>(Resource.Id.textVelocidad);
            this.textTotalDistance = FindViewById<TextView>(Resource.Id.textDistance);
            this.buttonFinish = FindViewById<Button>(Resource.Id.finishActivity);
            this.buttonStop = FindViewById<Button>(Resource.Id.stopActivity);
            this.txtTimer = FindViewById<TextView>(Resource.Id.txtTimer);

            this.totalRoute = new List<Gpx>();
            this.stop = true;
            this.first = true;
            this.totalDistance = 0;

            SetUpMap();
            InitializeLocationManager();

            timer = new Timer();
            timer.Interval = 1000; // 1 second  
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            buttonFinish.Click += (sender, e) => {
                OnPause();
                string ruta = makeGpx();

            };
            buttonStop.Click += (sender, e) => {
                if (stop){
                    OnPause();
                    this.stop = false;
                }else{
                    
                    OnResume();
                    this.stop = true;
                }
            };
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            sec++;
            if (sec == 60){
                min++;
                sec = 0;
            }if (min == 60){
                hour++;
                double res = this.totalDistance / hour;
                this.textVelocidad.Text = res.ToString("0.00");
                min = 0;
            }RunOnUiThread(() => { txtTimer.Text = $"{hour}:{min}:{sec}"; });
        }

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
        private void SetUpMap(){
            if (mMap == null){
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapGoogle).GetMapAsync(this);
            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            mMap.MyLocationEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

        }
        public string makeGpx(){
            string trkpt = "";
            for (int i = 0; i < this.totalRoute.Count; i++){
                trkpt += 
                    "<trkpt"+ "lat="+this.totalRoute[i].lat.ToString("s") +"lon="+this.totalRoute[i].lon.ToString("s") +">"
                    +"<ele>"+ this.totalRoute[i].ele.ToString("s") + "</ele>"
                    +"<time>" + this.totalRoute[i].time.ToString("s") + "Z</time>"
                    +"</trkpt>";
            }
            string gpx = "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
            + "<gpx xmlns=\"http://www.topografix.com/GPX/1/1\" xmlns:gpxtpx=\"http://www.garmin.com/xmlschemas/TrackPointExtension/v1\" xmlns:gpxx=\"http://www.garmin.com/xmlschemas/GpxExtensions/v3\" xmlns:ns1=\"http://www.cluetrust.com/XML/GPXDATA/1/0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" creator=\"Zamfit\" version=\"1.3\" xsi:schemaLocation=\"http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd http://www.garmin.com/xmlschemas/GpxExtensions/v3 http://www.garmin.com/xmlschemas/GpxExtensionsv3.xsd http://www.garmin.com/xmlschemas/TrackPointExtension/v1 http://www.garmin.com/xmlschemas/TrackPointExtensionv1.xsd\">"
            + "<metadata><time>2022-01-01T00:00:00Z</time></metadata>"
            + "<trk>"
            + "<name>Activity Name</name>"
            + "<trkseg>"
            + trkpt
            + "</trkseg>"
            + "</trk>"
            + "</gpx>";
            return gpx;
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.timer.Stop();
            locationManager.RemoveUpdates(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            this.timer.Start();
            locationManager.RequestLocationUpdates(locationProvider, 1000, 1, this);
        }

        public void OnLocationChanged(Android.Locations.Location location){
            Double lat, lng, ele;
            lat = location.Latitude;
            lng = location.Longitude;
            ele = location.Altitude;
            Gpx gps = new Gpx(lat, lng, ele, DateTime.Now);

            if (this.first){
                this.myPosition = new LatLng(lat, lng);
                this.lastPosition = this.myPosition;
                this.first = false;
                
            }else{

                this.lastPosition = this.myPosition;
                this.myPosition = new LatLng(lat, lng);

                double distance = Xamarin.Essentials.Location.CalculateDistance(lastPosition.Latitude, lastPosition.Longitude, myPosition.Latitude, myPosition.Longitude, DistanceUnits.Kilometers);
                this.totalDistance += distance;
                this.textTotalDistance.Text = this.totalDistance.ToString("0.00");

                Polyline line = mMap.AddPolyline(new PolylineOptions().Add(lastPosition, myPosition));

                CameraUpdate camUpdate = CameraUpdateFactory.NewLatLngZoom(myPosition, 17);
                mMap.MoveCamera(camUpdate);
            }
            this.totalRoute.Add(gps);

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