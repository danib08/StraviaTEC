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

namespace MobileApp
{
    internal class Database
    {
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
            string url = "Athlete";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            //Aqui se cae 
            string response = await client.GetStringAsync(url);
            

            List<Athlete> serverList = JsonConvert.DeserializeObject<List<Athlete>>(response);
            List<Athlete> newList = serverList; 

            List<AthleteLocal> localList = GetLocalAthlete();
            foreach (AthleteLocal item in localList)
            {
                newList.Add(item);

                // Post to server
                string json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await client.PostAsync(url, content);
            }

            // Clear all data in Customer Table
            using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
            connection.Query<Athlete>("DELETE FROM Athlete");
            connection.Query<AthleteLocal>("DELETE FROM AthleteLocal");

            // Add updated data to app database
            foreach (Athlete c in newList)
            {
                connection.Insert(c);
            }
        }
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

    }
}