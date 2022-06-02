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
/// Controller for athletes in challenges
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteInChallengeController : ControllerBase
    {
        //Configurations to get connection string

        private readonly IConfiguration _configuration;

        public AthleteInChallengeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all athletes in all challenges
        /// </summary>
        /// <returns>Athletes in challenges</returns>

        [HttpGet]
        public JsonResult GetAthChallenges()
        {
            string query = @"
                             exec get_all_AICH
                            "; //Select query sent to sql
            DataTable table = new DataTable(); //Created table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");//Sets connection string
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads data to table
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns data
        }

        /// <summary>
        /// Method to get a specific athlete in a specific challenge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="challenge"></param>
        /// <returns>A specific athlete in challenge</returns>

        [HttpGet("{id}/{challenge}")]
        public string GetAthChallenge(string id, string challenge)
        {

            string lbl_athleteid;
            string lbl_challengeid;
            string lbl_status;

            string query = @"
                             exec get_Athlete_Challenge @athleteid,@challengeid
                            "; //select query sent to sql server

            DataTable table = new DataTable(); //Table created to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@challengeid", challenge);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_athleteid = row["AthleteID"].ToString();
                lbl_challengeid = row["ChallengeID"].ToString();
                lbl_status = row["Status"].ToString();



                var data = new JObject(new JProperty("athleteID", lbl_athleteid), new JProperty("challengeID", lbl_challengeid),
                                    new JProperty("status", lbl_status));
                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
        }


        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Athlete/{username}")]
        public JsonResult get_One_Athlete_Challenges(string username)
        {

            //SQL Query
            string query = @"
                             exec get_One_Athlete_Challenges @username
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            TextInfo ti2 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti2.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Challenge/{challengeid}")]
        public JsonResult get_Ath_OneCompetition(string challengeid)
        {

            //SQL Query
            string query = @"
                             exec get_Athlete_OneChallenge @challenge
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@challenge", challengeid);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }
            TextInfo ti3 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti3.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Post method to get 
        /// </summary>
        /// <param name="athlete_In_Challenge"></param>
        /// <returns>Result of query</returns>

        [HttpPost]
        public JsonResult PostAthChallenge(Athlete_In_Challenge athlete_In_Challenge)
        {

            //Validaciones de Primary Key

            //Insert query sent to SQL Server
            string query = @"
                             exec post_Athlete_Challenge @athleteid, @challengeid,@status
                            "; 
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Connection opened
                SqlCommand myCommand = new SqlCommand(query, myCon);// SQL Command with query and connection

                //Added parameters with values
                myCommand.Parameters.AddWithValue("@athleteid", athlete_In_Challenge.athleteID);
                myCommand.Parameters.AddWithValue("@challengeid", athlete_In_Challenge.challengeID);
                myCommand.Parameters.AddWithValue("@status", athlete_In_Challenge.status);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return new JsonResult(table); //Returns table data

        }

        /// <summary>
        /// Method to delete athletes in a challenge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="challengeID"></param>
        /// <returns>Result of query</returns>
        [HttpDelete("{id}/{challenge}")]
        public ActionResult DeleteAthChallenge(string id, string challengeID)
        {
            // SQL Server query
            string query = @"
                             exec delete_Athlete_Challenge @athleteid, @challengeid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Created command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@challengeid", challengeID);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }


    }
}
