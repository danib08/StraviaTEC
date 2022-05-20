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

namespace AppMobile.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity{
        private Button buttonlogin;
        private Button buttonregister;

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            this.buttonlogin = FindViewById<Button>(Resource.Id.btnlogin);
            this.buttonregister = FindViewById<Button>(Resource.Id.btnregister);
            
            
            buttonlogin.Click += (sender, e) =>{
                Intent intent = new Intent(this, typeof(activity));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
            buttonregister.Click += (sender, e) =>{
                Intent intent = new Intent(this, typeof(register));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
        }
       
    }
}