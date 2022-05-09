using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AppMobile.Activities{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class login : AppCompatActivity{

        private EditText editTextIdIn;
        private EditText editTextPassIn;
        private Button sendSignIn;
        protected override void OnCreate(Bundle savedInstanceState){

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            editTextIdIn = FindViewById<EditText>(Resource.Id.editTextIdIn);
            editTextPassIn = FindViewById<EditText>(Resource.Id.editTextPassIn);
            sendSignIn = FindViewById<Button>(Resource.Id.sendSignIn);           
        
        }
    }
}