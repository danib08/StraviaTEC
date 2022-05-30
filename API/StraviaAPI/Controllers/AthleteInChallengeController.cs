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



                var data = new JObject(new JProperty("AthleteID", lbl_athleteid), new JProperty("ChallengeID", lbl_challengeid),
                                    new JProperty("Status", lbl_status));
                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
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
                myCommand.Parameters.AddWithValue("@athleteid", athlete_In_Challenge.AthleteID);
                myCommand.Parameters.AddWithValue("@challengeid", athlete_In_Challenge.ChallengeID);
                myCommand.Parameters.AddWithValue("@status", athlete_In_Challenge.Status);

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
        [HttpDelete]
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
