using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AppMobile.Activities{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class register : AppCompatActivity{

        private EditText editTextId;
        private EditText editTextPass;
        private EditText editTextName;
        private EditText editTextLastName;
        private EditText editTextbirthDate;
        private EditText editTextNationality;

        private Button buttonPhoto;
        private Button bottonsendHome;

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.register);

            buttonPhoto = FindViewById<Button>(Resource.Id.photo);
            bottonsendHome = FindViewById<Button>(Resource.Id.sendHome2);

            buttonPhoto.Click += (sender, e) =>{

            };

            bottonsendHome.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(home));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
        }
    }
}