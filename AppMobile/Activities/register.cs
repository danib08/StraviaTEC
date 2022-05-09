using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AppMobile.Activities{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class register : Activity{

        private EditText editTextId;
        private EditText editTextPass;
        private EditText editTextName;
        private EditText editTextLastName;
        private EditText editTextbirthDate;
        private EditText editTextNationality;

        private Button sendSignUp;

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.register);
            
        }
    }
}