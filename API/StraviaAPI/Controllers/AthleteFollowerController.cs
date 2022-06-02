using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StraviaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

/// <summary>
/// Controller to manage athlete's friends
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteFollowerController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public AthleteFollowerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for all friends
        /// </summary>
        /// <returns>All friends of all athletes</returns>

        [HttpGet]
        public JsonResult GetAthletesFriends()
        {
            string query = @"
                             exec get_all_followers
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Created table to save data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Load data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table 
        }

        /// <summary>
        /// Get method of a specific friend for a specific athlete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendid"></param>
        /// <returns>Athlete's requested friend</returns>

        [HttpGet("{id}/{followerid}")]
        public string GetAthFriend(string id, string followerid)
        {
            string lbl_athleteid;
            string lbl_followerid;

            string query = @"
                             exec get_follower @athleteid,@followerid
                            "; //Select query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@followerid", followerid);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads info to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_athleteid = row["AthleteID"].ToString();
                lbl_followerid = row["FollowerID"].ToString();

                var data = new JObject(new JProperty("athleteID", lbl_athleteid), new JProperty("followerID", lbl_followerid));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
        }

        /// <summary>
        /// Get method of a specific friend for a specific athlete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendid"></param>
        /// <returns>Athlete's requested friend</returns>

        [HttpGet("Followers/{id}")]
        public JsonResult GetAthFriends(string id)
        {

            string query = @"
                             exec get_ath_followers @athleteid
                            "; //Select query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    

                    myReader  = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads info to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            TextInfo ti2 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti2.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table 
        }

        /// <summary>
        /// Method to get who follows the athlete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendid"></param>
        /// <returns>Athlete's requested friend</returns>

        [HttpGet("FollowedBy/{id}")]
        public JsonResult get_aths_follower(string id)
        {

            string query = @"
                             exec get_aths_follower @athleteid
                            "; //Select query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads info to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            TextInfo ti3 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti3.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table 
        }


        /// <summary>
        /// Post method for athlete's friend
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult PostAthFriend(Athlete_Followers follower)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            //Primary Key validations



            string query = @"exec post_follower @athleteid,@followerid
                            "; //Insert query sent to sql server
            DataTable table = new DataTable();
            
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open(); //Opens connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added
                myCommand.Parameters.AddWithValue("@athleteid", follower.athleteID);
                myCommand.Parameters.AddWithValue("@followerid", follower.followerID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return new JsonResult(table); //Returns table

        }

        /// <summary>
        /// Delte method for athlete's friend
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendID"></param>
        /// <returns>Reusult of query</returns>

        [HttpDelete("{id}/{followerid}")]
        public ActionResult DeleteAthFriend(string id, string followerID)
        {
            string query = @"
                             exec delete_follower @athleteid,@followerid
                            "; //Delete query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command eith query and connection
                {

                    //Added parameters with values
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@followerid", followerID);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }
            return Ok(); //Returns Acceptance
        }

    }
}
