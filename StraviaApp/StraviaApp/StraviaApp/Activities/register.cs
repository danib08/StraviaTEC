using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using StraviaApp.Models;
using StraviaApp;
using Plugin.Media;
using System;
using System.Drawing;

namespace StraviaApp.Activities
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class register : AppCompatActivity
    {

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
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

            buttonPhoto.Click += (sender, e) => {
                UploadPhoto();
            };

            bottonsendHome.Click += (sender, e) =>
            {
                if (editTextName.Text.Equals("") || editTextLastName.Text.Equals("") || editTextPass.Text.Equals("")
                || editTextbirthDate.Text.Equals("") || editTextNationality.Text.Equals(""))
                {
                    this.messengerText = "Por favor llene todos los espacios requeridos";
                }
                else
                {
                    string username;
                    if (editTextId.Text.Equals(""))
                    {
                        username = "";
                    }
                    else
                    {
                        username = editTextId.Text;
                    }
                    int age = GetCurrentAge();
                    string categoria = GetCategory(age);

                    Athlete user = new Athlete
                    {
                        username = username,
                        name = editTextName.Text,
                        lastname = editTextLastName.Text,
                        birthdate = editTextbirthDate.Text,
                        nationality = editTextNationality.Text,
                        age = age,
                        pass = editTextPass.Text,
                        category = categoria,
                        photo = ""
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
                            age = age,
                            pass = editTextPass.Text,
                            category = categoria,
                            photo = ""
                        };
                        db.PutAthletLocal(userLocal);
                        this.messengerText = "Registro exitoso";
                        Finish();
                    }
                    else
                    {
                        this.messengerText = "Esta cédula ya se encuentra registrada";
                    }
                }
                Toast.MakeText(this, messengerText, ToastLength.Short).Show();
            };

            //Method for open the gallery
            async void UploadPhoto()
            {
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
        //Method for getting the current Age of the user
        public int GetCurrentAge()
        {
            DateTime oDate = Convert.ToDateTime(editTextbirthDate.Text);
            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(oDate.ToString("yyyyMMdd"));
            int currentAge = (now - dob) / 10000;
            return currentAge;
        }
        //Method for getting the category of the user
        public string GetCategory(int age)
        {
            string categoria;
            if (age < 15)
            {
                categoria = "Junior";
            }
            else if (15 < age && age < 23)
            {
                categoria = "Sub";
            }
            else if (24 < age && age < 30)
            {
                categoria = "Open";
            }
            else if (30 < age && age < 40)
            {
                categoria = "Master A";
            }
            else if (41 < age && age < 50)
            {
                categoria = "Master B";
            }
            else
            {
                categoria = "Master C";
            }
            return categoria;
        }
    }
}
