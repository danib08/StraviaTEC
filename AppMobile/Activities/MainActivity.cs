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
using Android.Content;
using MobileApp;
using System.Threading.Tasks;
using Java.Util.Jar;
using AppMobile.Models;

namespace AppMobile.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity{
        private Button buttonlogin;
        private Button buttonregister;
        private Database db;
        protected override async void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            //Database synchronization
            db = new Database();
            db.CreateDatabase();
            //await db.SyncAsync();

            SetContentView(Resource.Layout.activity_main);
            this.buttonlogin = FindViewById<Button>(Resource.Id.btnlogin);
            this.buttonregister = FindViewById<Button>(Resource.Id.btnregister);
            
            buttonlogin.Click += (sender, e) =>{
                //Intent intent = new Intent(this, typeof(homeActivity));
                Intent intent = new Intent(this, typeof(login));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                //intent.PutExtra("totalDistance", "5332");
                //intent.PutExtra("totalTime", "455");
                //intent.PutExtra("gpx", "46");
                //intent.PutExtra("idUser", "ad");
                StartActivity(intent);
            };
            buttonregister.Click += (sender, e) =>{
                Intent intent = new Intent(this, typeof(register));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }

    }
}