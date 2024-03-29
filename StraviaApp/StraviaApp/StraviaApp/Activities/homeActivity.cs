﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.AppCompat.App;
using StraviaApp;
using StraviaApp.Models;
using System.Globalization;

namespace StraviaApp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class homeActivity : AppCompatActivity
    {
        private Button btnstartActivity;

        private EditText editTextIdActivity;
        private EditText editTextNameActivity;
        private TextView txtCurrentDay;
        private TextView txtCurrentTime;
        private TextView txtTotalDistance;
        private TextView txtIdUser;
        private Spinner spin;
        private Database db;
        private string messengerText;
        private string user;
        private string gpx;
        private string txtType;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.homeActivity);

            db = new Database();
            db.CreateDatabase();

            //Set all variable of the interface
            btnstartActivity = FindViewById<Button>(Resource.Id.btnRegActivity);
            editTextIdActivity = FindViewById<EditText>(Resource.Id.editTextIDActivity);
            editTextNameActivity = FindViewById<EditText>(Resource.Id.editTextNameActivity);
            txtCurrentDay = FindViewById<TextView>(Resource.Id.txtDisplayDay);
            txtCurrentTime = FindViewById<TextView>(Resource.Id.txtDisplayTimer);
            txtTotalDistance = FindViewById<TextView>(Resource.Id.txtDisplayDistance);
            txtIdUser = FindViewById<TextView>(Resource.Id.txtDisplayIdUser);
            spin = FindViewById<Spinner>(Resource.Id.spinner1);

            //Set values of the currents variables
            txtIdUser.Text = "Usuario:" + Intent.GetStringExtra("idUser");
            txtCurrentDay.Text = "Fecha:" + DateTime.Now.ToString("yyyy-MM-dd");
            txtCurrentTime.Text = "Tiempo:" + Intent.GetStringExtra("totalTime");
            txtTotalDistance.Text = "Distancia:" + Intent.GetStringExtra("totalDistance");
            user = Intent.GetStringExtra("idUser");
            gpx = Intent.GetStringExtra("gpx");

            spin.ItemSelected += (s, e) =>
            {
                txtType = e.Parent.GetItemAtPosition(e.Position).ToString();
            };
            btnstartActivity.Click += (sender, e) =>
            {
                if (editTextIdActivity.Text.Equals("") || editTextNameActivity.Text.Equals(""))
                {
                    this.messengerText = "Por favor llene todos los espacios requeridos";
                }
                else
                {
                    string idActivity;
                    string nameActivity = editTextNameActivity.Text;
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd").ToString();
                    string currentDuration = Intent.GetStringExtra("totalTime").ToString();
                    string km = Intent.GetStringExtra("totalDistance").ToString();

                    if (editTextIdActivity.Text.Equals(""))
                    {
                        idActivity = "";
                    }
                    else
                    {
                        idActivity = editTextIdActivity.Text.ToString();
                    }

                    ActivityModel newActivityModel = new ActivityModel
                    {
                        id = idActivity,
                        name = nameActivity,
                        route = gpx,
                        date = currentDate,
                        duration = currentDuration,
                        kilometers = km,
                        type = txtType,
                        athleteUsername = user.ToString(),
                    };
                    if (db.PutActivityModel(newActivityModel))
                    {
                        // Local Table
                        ActivityModelLocal newActivityModelLocal = new ActivityModelLocal
                        {
                            id = idActivity,
                            name = nameActivity,
                            route = gpx,
                            date = currentDate,
                            duration = currentDuration,
                            kilometers = km,
                            type = txtType,
                            athleteUsername = user.ToString(),
                        };
                        db.PutActivityModelLocal(newActivityModelLocal);
                        this.messengerText = "Registro exitoso";
                        Intent intent = new Intent(this, typeof(home));
                        OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        this.messengerText = "Este ID ya se encuentra registrado";
                    }
                }
                Toast.MakeText(this, messengerText, ToastLength.Short).Show();
            };
        }
    }
}