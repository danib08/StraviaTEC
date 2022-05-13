using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
                             select * from dbo.Athlete_In_Challenge
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
        public JsonResult GetAthChallenge(string id, string challenge)
        {
            string query = @"
                             select * from dbo.Athlete_In_Challenge
                             where AthleteID = @AthleteID and ChallengeID = @ChallengeID)
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
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@ChallengeID", challenge);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table data
        }

        /// <summary>
        /// Post method to get 
        /// </summary>
        /// <param name="athlete_In_Challenge"></param>
        /// <returns>Result of query</returns>

        [HttpPost]
        public ActionResult PostAthChallenge(Athlete_In_Challenge athlete_In_Challenge)
        {

            //Validaciones de Primary Key

            //Insert query sent to SQL Server
            string query = @"
                             insert into dbo.Athlete_In_Challenge
                             values (@AthleteID,@ChallengeID)
                            "; 
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Connection opened
                SqlCommand myCommand = new SqlCommand(query, myCon);// SQL Command with query and connection

                //Added parameters with values
                myCommand.Parameters.AddWithValue("@AthleteID", athlete_In_Challenge.AthleteID);
                myCommand.Parameters.AddWithValue("@ChallengeID", athlete_In_Challenge.ChallengeID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return Ok(); //Returns acceptance

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
                             delete from dbo.Athlete_In_Challenge
                             where AthleteID = @AthleteID and  ChallengeID= @ChallengeID
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
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@ChallengeID", challengeID);

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
