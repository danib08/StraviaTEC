﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;


namespace AppMobile.Activities{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class login : AppCompatActivity{

        private EditText editTextIdIn;
        private EditText editTextPassIn;
        private Button bottonsendHome;
        protected override void OnCreate(Bundle savedInstanceState){

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            editTextIdIn = FindViewById<EditText>(Resource.Id.editTextIdIn);
            editTextPassIn = FindViewById<EditText>(Resource.Id.editTextPassIn);
            bottonsendHome = FindViewById<Button>(Resource.Id.sendHome);

            bottonsendHome.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(home));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
        }
    }
}