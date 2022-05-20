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

namespace AppMobile.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
                                                //for google map,    for gps location
    public class activity : AppCompatActivity,IOnMapReadyCallback, ILocationListener
    {
        // <summary>
        // Google maps
        // </summary>
        private GoogleMap mMap;
        private LocationManager locationManager;
        private string locationProvider;
        private LatLng originPosition;
        private LatLng lastPosition;
        private LatLng myPosition;
        private double totalDistance;
        private TextView textTotalDistance;
        private bool stop;
        private Chronometer chronometer;

        private Button buttonStop;
        private Button buttonFinish;


        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity);

            this.chronometer = FindViewById<Chronometer>(Resource.Id.chronometer);
            this.textTotalDistance = FindViewById<TextView>(Resource.Id.textDistance);
            this.buttonFinish = FindViewById<Button>(Resource.Id.finishActivity);
            this.buttonStop = FindViewById<Button>(Resource.Id.stopActivity);
            this.stop = true;
            this.chronometer.Start();

            SetUpMap();
            InitializeLocationManager();
            initializeMyLocation();

            buttonFinish.Click += (sender, e) => {
                
            };
            buttonStop.Click += (sender, e) => {
                if (stop){
                    OnPause();
                    this.chronometer.Stop();
                    this.stop=false;
                }else {
                    OnResume();
                    this.chronometer.Start();
                    this.stop = true;
                }
            };
        }

        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any()){
                locationProvider = acceptableLocationProviders.First();
            }else{
                locationProvider = string.Empty;
            }

        }
        private void SetUpMap()
        {
            if (mMap == null){
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapGoogle).GetMapAsync(this);
            }
        }
        private async void initializeMyLocation() {
            var location = await Geolocation.GetLastKnownLocationAsync();
            this.originPosition = new LatLng(location.Latitude,location.Longitude);
            this.myPosition = this.originPosition;
            this.lastPosition = this.originPosition;
            this.totalDistance = 0;
            this.chronometer.Start();
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            mMap = googleMap;
            mMap.MyLocationEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }
        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(locationProvider, 1000, 1, this);
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            Double lat, lng;
            lat = location.Latitude;
            lng = location.Longitude;
            this.lastPosition = this.myPosition;
            this.myPosition = new LatLng(lat, lng);

            double distance = Xamarin.Essentials.Location.CalculateDistance(lastPosition.Latitude, lastPosition.Longitude, myPosition.Latitude, myPosition.Longitude, DistanceUnits.Kilometers);
            this.totalDistance += distance;
            this.textTotalDistance.Text = this.totalDistance.ToString("0.00");

            CameraUpdate camUpdate = CameraUpdateFactory.NewLatLngZoom(myPosition, 17);
            mMap.MoveCamera(camUpdate);

            Polyline line = mMap.AddPolyline(new PolylineOptions().Add(lastPosition, myPosition));
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