using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AppMobile.Models;
using MobileApp;
using MobileApp.Models;
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
        private string messengerText;
        private Database db;

        protected override void OnCreate(Bundle savedInstanceState){
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.register);

            db = new Database();
            db.CreateDatabase();

            buttonPhoto = FindViewById<Button>(Resource.Id.photo);
            bottonsendHome = FindViewById<Button>(Resource.Id.sendHome2);
            editTextId = FindViewById<EditText>(Resource.Id.editTextId);
            editTextPass = FindViewById<EditText>(Resource.Id.editTextPass);
            editTextName = FindViewById<EditText>(Resource.Id.editTextName);
            editTextLastName = FindViewById<EditText>(Resource.Id.editTextLastName);
            editTextbirthDate = FindViewById<EditText>(Resource.Id.editbirthDate);
            editTextNationality = FindViewById<EditText>(Resource.Id.editTextNationality);

            buttonPhoto.Click += (sender, e) =>{
                UploadPhoto();
            };

            bottonsendHome.Click += (sender, e) =>
            {
                if (editTextName.Text.Equals("") || editTextLastName.Text.Equals("") || editTextPass.Text.Equals("") 
                || editTextbirthDate.Text.Equals("") || editTextNationality.Text.Equals(""))
                {
                    this.messengerText = "Por favor llene todos los espacios requeridos";
                }else{
                    string username;
                    if (editTextId.Text.Equals("")){
                        username = "";
                    }else{
                        username = editTextId.Text;
                    }
                    Athlete user = new Athlete {
                        username = username,
                        name = editTextName.Text,
                        lastname = editTextLastName.Text,
                        birthdate = editTextbirthDate.Text,
                        nationality = editTextNationality.Text,
                        age = 0,
                        pass = editTextPass.Text,
                        category = "",
                        photo=""
                    };
                    if (db.PutAthlete(user))
                    {
                        // Local Table
                        AthleteLocal userLocal = new AthleteLocal
                        {
                            username = username,
                            name = editTextName.Text,
                            lastname = editTextLastName.Text,
                            birthdate = editTextbirthDate.Text,
                            nationality = editTextNationality.Text,
                            age = 0,
                            pass = editTextPass.Text,
                            category = "",
                            photo = ""
                        };
                        db.PutAthletLocal(userLocal);
                        this.messengerText = "Registro exitoso";
                        Finish();
                    }else{
                        this.messengerText = "Esta cédula ya se encuentra registrada";
                    }
                }
                Toast.MakeText(this, messengerText, ToastLength.Short).Show();
            };

            //Method for open the gallery
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