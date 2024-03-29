﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using StraviaApp.Models;
using StraviaApp;

namespace StraviaApp.Activities
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class login : AppCompatActivity
    {

        private EditText editTextIdIn;
        private EditText editTextPassIn;
        private Button bottonsendHome;
        private Database db;
        private string messengerText;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            db = new Database();
            db.CreateDatabase();

            //Set all variable of the interface
            editTextIdIn = FindViewById<EditText>(Resource.Id.editTextIdIn);
            editTextPassIn = FindViewById<EditText>(Resource.Id.editTextPassIn);
            bottonsendHome = FindViewById<Button>(Resource.Id.sendHome);

            //btn home
            bottonsendHome.Click += (sender, e) => {
                if (editTextIdIn.Text.Equals("") || editTextPassIn.Text.Equals(""))
                {
                    this.messengerText = "Por favor llene todos los espacios requeridos";
                }
                else
                {
                    Athlete user = db.GetAthlete(editTextIdIn.Text.ToString());
                    if (user == null)
                    {
                        this.messengerText = "Este usuario no se encuentra registrado";
                    }
                    else
                    {
                        if (editTextPassIn.Text.Equals(user.pass))
                        {
                            this.messengerText = "Sesión iniciada";
                            Intent intent = new Intent(this, typeof(home));
                            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                            intent.PutExtra("idUser", editTextIdIn.Text.ToString());
                            StartActivity(intent);
                            Finish();
                        }
                        else
                        {
                            this.messengerText = "Contraseña incorrecta";
                        }
                    }
                }
                Toast.MakeText(this, messengerText, ToastLength.Short).Show();
            };
        }
    }
}