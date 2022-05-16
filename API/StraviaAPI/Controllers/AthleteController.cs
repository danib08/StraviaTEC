using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StraviaAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Athlete controller with all CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public AthleteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for all athletes 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAthletes()
        {
            string query = @"
                             exec get_all_athletes
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Table to save information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Opened Connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command created with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Data loaded to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table
        }

        /// <summary>
        /// Get method for a specific athlete
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Athlete by username</returns>

        [HttpGet("{username}")]
        public JsonResult GetAthlete(string username)
        {
            string query = @"
                             exec get_athlete @Username = @user
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Table created to store information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Created connection
            {
                myCon.Open(); //Opens connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Created command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@user", username); //Added parameter username
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Information loades to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table with info
        }

        /// <summary>
        /// Method post to search athletes
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List of athletes that have the specified name and lastname</returns>

        [HttpPost("Search")]
        public JsonResult SearchAthletes(JObject data)
        {
            var name = data["Name"].ToString();
            var lastname = data["LastName"].ToString();

            string query = @"
                             select * from dbo.Athlete
                             where Name = @Name and LastName = @LastName
                            "; //Select query sent to SQL Server

            DataTable table = new DataTable(); //Table created to store information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Created connection
            {
                myCon.Open(); //Opens connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Created command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Name", name); //Added parameter name
                    myCommand.Parameters.AddWithValue("@LastName", lastname);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Information loades to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table with info
        }


        /// <summary>
        /// Post method for athletes
        /// </summary>
        /// <param name="athlete"></param>
        /// <returns>Action result of query</returns>
        [HttpPost]
        public ActionResult PostAthlete(Athlete athlete)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
           
            //Primary key validations
            string validation = @"select Username from dbo.Athlete";

            DataTable validTable = new DataTable(); //Created table for validations
            SqlDataReader validReader;
            using (SqlConnection validCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                validCon.Open(); //Opened validation Connection
                using (SqlCommand validCommand = new SqlCommand(validation, validCon))//Command created with query and connection
                {
                    validCommand.Parameters.AddWithValue("@Username", athlete.Username);
                    validReader = validCommand.ExecuteReader();
                    validTable.Load(validReader); //Usernames Data loaded to table
                    validReader.Close();
                    validCon.Close(); //Closed connection
                }
            }

            foreach(DataRow row in validTable.Rows) //Iterates the data stored in the table
            {
                foreach(var item  in row.ItemArray){ //Gets the item in every row

                    if (item.Equals(athlete.Username)){ //Verifies if is equal to the primary key

                        return BadRequest(); //Returns rejection
                        
                    }
                    
                }
            }


            //Insert query sent to SQL Server 

            string query = @"
                             exec post_athlete @username,@name,@lastname,@photo,@age,@birthdate,@pass,@nationality,@category
                            "; 
            DataTable table = new DataTable();
            
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                SqlCommand myCommand = new SqlCommand(query, myCon);
                
                //Added parameters with values
                myCommand.Parameters.AddWithValue("@username", athlete.Username);
                myCommand.Parameters.AddWithValue("@name", athlete.Name);
                myCommand.Parameters.AddWithValue("@lastname", athlete.LastName);
                myCommand.Parameters.AddWithValue("@photo", athlete.Photo);
                myCommand.Parameters.AddWithValue("@age", athlete.Age);
                myCommand.Parameters.AddWithValue("@birthdate", athlete.BirthDate);
                myCommand.Parameters.AddWithValue("@pass", athlete.Pass);
                myCommand.Parameters.AddWithValue("@nationality", athlete.Nationality);
                myCommand.Parameters.AddWithValue("@category", athlete.Category);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection
                
            }

            return Ok(); //Returns acceptance

        }

        /// <summary>
        /// Put method fot athletes
        /// </summary>
        /// <param name="athlete"></param>
        /// <returns>Acceptance of query</returns>

        [HttpPut]
        public ActionResult PutAthlete(Athlete athlete)
        {
            string query = @"
                             exec put_athlete @username,@name,@lastname,@photo,@age,@birthdate,@pass,@nationality,@category
                            "; //update query sent to sql server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec"); //Connection started
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    //Added parameters with values
                    myCommand.Parameters.AddWithValue("@username", athlete.Username);
                    myCommand.Parameters.AddWithValue("@name", athlete.Name);
                    myCommand.Parameters.AddWithValue("@lastname", athlete.LastName);
                    myCommand.Parameters.AddWithValue("@photo", athlete.Photo);
                    myCommand.Parameters.AddWithValue("@age", athlete.Age);
                    myCommand.Parameters.AddWithValue("@birthdate", athlete.BirthDate);
                    myCommand.Parameters.AddWithValue("@pass", athlete.Pass);
                    myCommand.Parameters.AddWithValue("@nationality", athlete.Nationality);
                    myCommand.Parameters.AddWithValue("@category", athlete.Category);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }

        /// <summary>
        /// Delete method for athletes
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Accpetance of query</returns>

        [HttpDelete]
        public ActionResult DeleteAthlete(string username)
        {
            string query = @"
                             exec delete_athlete @username
                            "; //delete query sent to SQL Server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");//Connection getted
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Username", username); //Parameter added
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
