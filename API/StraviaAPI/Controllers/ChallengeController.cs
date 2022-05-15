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
/// Challenge controler with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public ChallengeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all callenges
        /// </summary>
        /// <returns>List of challenges</returns>
        [HttpGet]
        public JsonResult GetChallenges()
        {
            //Query sent to SQL Server
            string query = @"
                             exec get_all_challenges
                            ";
            DataTable table = new DataTable(); //Table created to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loading data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table data
        }

        /// <summary>
        /// Method to get challenge by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json with challenge information</returns>
        [HttpGet("{id}")]
        public JsonResult GetChallenge(string id)
        {
            //SQL Query
            string query = @"
                             exec get_challenge @id
                            ";
            DataTable table = new DataTable();//Created table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Load data to table
                    myReader.Close();
                    myCon.Close(); //Close connection
                }
            }
            return new JsonResult(table); //Returns json with data
        }

        /// <summary>
        /// Post method to create challenges
        /// </summary>
        /// <param name="challenge"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public ActionResult PostChallenge(Challenge challenge)
        {
            //Validaciones de Primary Key

            //SQL Query
            string query = @"
                             exec post_challenge @id,@name,@startdate,@enddate,@privacy,@kilometers,@type
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection stablished
            {
                myCon.Open(); //Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);

                //Parameters added with values
                myCommand.Parameters.AddWithValue("@id", challenge.Id);
                myCommand.Parameters.AddWithValue("@name", challenge.Name);
                myCommand.Parameters.AddWithValue("@startdate", challenge.StartDate);
                myCommand.Parameters.AddWithValue("@enddate", challenge.EndDate);
                myCommand.Parameters.AddWithValue("@privacy", challenge.Privacy);
                myCommand.Parameters.AddWithValue("@kilometers", challenge.Kilometers);
                myCommand.Parameters.AddWithValue("@type", challenge.Type);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return Ok(); //Returns acceptance

        }

        /// <summary>
        /// Method to update challenges
        /// </summary>
        /// <param name="challenge"></param>
        /// <returns>Query result</returns>
        [HttpPut]
        public ActionResult PutActivity(Challenge challenge)
        {
            //SQL Query
            string query = @"
                             exec put_challenge @id,@name,@startdate,@enddate,@privacy,@kilometers,@type
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open(); //Connection closed
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Sql command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@id", challenge.Id);
                    myCommand.Parameters.AddWithValue("@name", challenge.Name);
                    myCommand.Parameters.AddWithValue("@startdate", challenge.StartDate);
                    myCommand.Parameters.AddWithValue("@enddate", challenge.EndDate);
                    myCommand.Parameters.AddWithValue("@privacy", challenge.Privacy);
                    myCommand.Parameters.AddWithValue("@kilometers", challenge.Kilometers);
                    myCommand.Parameters.AddWithValue("@type", challenge.Type);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }

        /// <summary>
        /// Delete method for challenges
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Query result</returns>
        [HttpDelete]
        public ActionResult DeleteActivity(string id)
        {
            //SQL Query
            string query = @"
                             exec delete_challenge @id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }

    }
}
