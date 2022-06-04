using SQLite;
using System.Collections.Generic;
using Android.Util;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using AppMobile.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Essentials;
using Android.Content.Res;
using Android.Widget;
using MobileApp.Models;
using System.Net;

namespace MobileApp
{
    internal class Database{

        private readonly string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private HttpClient client = new HttpClient();
        private const string Ipv4 = "192.168.0.16";
        private readonly string baseAddress = "https://" + Ipv4 + ":5001/api/";
        private readonly string baseAddress2 = "http://" + Ipv4 + ":5000/api/";
        public bool CreateDatabase(){
            try{
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                connection.CreateTable<Athlete>();
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        //SQL Synchronization 
        public async Task SyncAsync(){
            client.BaseAddress = new Uri(baseAddress);
            await SyncAthleteAsync();
        }
        private async Task SyncAthleteAsync() {

            var request = new HttpRequestMessage();
            //http://192.168.0.16:5000/api/Athlete
            //https://jsonplaceholder.typicode.com/posts
            //https://localhost:5001/api/Activity

            request.RequestUri = new Uri("https://localhost:5001/api/Activity");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accpet", "application/json");
            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                //var resultado = JsonConvert.DeserializeObject<List<Database>>(content);
            }

            //string url = "Athlete";
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            ////Aqui se cae 
            //string response = await client.GetStringAsync(url);
            

            //List<Athlete> serverList = JsonConvert.DeserializeObject<List<Athlete>>(response);
            //List<Athlete> newList = serverList; 

            //List<AthleteLocal> localList = GetLocalAthlete();
            //foreach (AthleteLocal item in localList)
            //{
            //    newList.Add(item);

            //    // Post to server
            //    string json = JsonConvert.SerializeObject(item);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //    await client.PostAsync(url, content);
            //}

            //// Clear all data in Customer Table
            //using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
            //connection.Query<Athlete>("DELETE FROM Athlete");
            //connection.Query<AthleteLocal>("DELETE FROM AthleteLocal");

            //// Add updated data to app database
            //foreach (Athlete c in newList)
            //{
            //    connection.Insert(c);
            //}
        }

        //Get all the Athletes in the local db
        public List<AthleteLocal> GetLocalAthlete()
        {
            try
            {
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                return connection.Table<AthleteLocal>().ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
        //Get single Athlete
        public Athlete GetAthlete(string username){
            try{
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                List<Athlete> athlete = connection.Query<Athlete>("SELECT * FROM Athlete Where username=?", username);
                return athlete.Find(athlete => athlete.username == username);
            }catch (SQLiteException ex){
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }

        }
        //Put a new Athlete
        public bool PutAthlete(Athlete user){
            try
            {
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                connection.Insert(user);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Put a new Athlete in the db
        public bool PutAthletLocal(AthleteLocal user){
            try
            {
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                connection.Insert(user);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}