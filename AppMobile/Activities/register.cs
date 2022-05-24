using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Plugin.Media;
using System.Drawing;

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
                UploadPhoto();
            };

            bottonsendHome.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(home));
                OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                StartActivity(intent);
            };
            async void UploadPhoto(){
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    Toast.MakeText(this, "Upload not supported on this device", ToastLength.Short).Show();
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    CompressionQuality = 40

                });
                // Convert file to byre array, to bitmap and set it to our ImageView
                //byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            }
        }
    }
}