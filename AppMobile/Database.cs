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

namespace MobileApp
{
    internal class Database
    {
        private readonly string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        
        private static readonly HttpClient client = new HttpClient();
        private const string Ipv4 = "192.168.0.16";
        private readonly string BaseAddress = "https://" + Ipv4 + ":44319/api/";
        public bool CreateDatabase()
        {
            try
            {
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
            client.BaseAddress = new Uri(BaseAddress);
            await SyncAthleteAsync();
        }

        private async Task SyncAthleteAsync(){
            string url = "Athlete";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string response = await client.GetStringAsync(url);
            if (response == null){
                Debug.WriteLine("---------- False!");
            }
            else {
                Debug.WriteLine("---------- True!");
            }
            List<Athlete> serverList = JsonConvert.DeserializeObject<List<Athlete>>(response);
            List<Athlete> localList = GetAthletes();
            List<Athlete> newList = serverList;  // Copy server data to local app data
                                                 // Check for new data on app in order to keep it
            //foreach (Athlete local in localList)
            //{
            //    bool isLocalChange = true;

            //    foreach (Athlete server in serverList)
            //    {
            //        // Checks if data was already on the server
            //        if (local.username == server.username)
            //        {
            //            isLocalChange = false;
            //            break;
            //        }
            //    }
            //    if (isLocalChange)
            //    {
            //        // Adds new data if it wasn't yet on the server
            //        newList.Add(local);

            //        // Post to server
            //        string json = JsonConvert.SerializeObject(local);
            //        var content = new StringContent(json, Encoding.UTF8, "application/json");
            //        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //        await client.PostAsync(url, content);
            //    }
            //}

            //// Clear all data in Customer Table
            //using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
            //connection.Query<Athlete>("DELETE FROM Athlete");

            //// Adds updated data to local database
            //foreach (Athlete c in newList)
            //{
            //    connection.Insert(c);
            //}
        }
        public List<Athlete> GetAthletes()
        {
            try
            {
                using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
                return connection.Table<Athlete>().ToList();
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
        //    public Athlete GetAthlete(string username)
        //    {
        //        try
        //        {
        //            using var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "StraviaTec.db"));
        //            List<Athlete> customers = connection.Query<Athlete>("SELECT * FROM Athlete Where Username=?", username);
        //            return customers.Find(customer => customer.username == username); ;
        //        }
        //        catch (SQLiteException ex)
        //        {
        //            Log.Info("SQLiteEx", ex.Message);
        //            return null;
        //        }

        //    }

    }
}