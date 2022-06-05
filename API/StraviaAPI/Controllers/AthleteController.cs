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
using System.Globalization;

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
                             exec proc_athlete '','','','',0,'2000-12-12','','','','Select'
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
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table
        }

        /// <summary>
        /// Get method for all athletes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Feed/{username}")]
        public JsonResult GeFeed(string username)
        {
            string query = @"
                             exec proc_athlete @username,'','','',0,'2000-12-12','','','','Feed'
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Table to save information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Opened Connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command created with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username); //Added parameter username
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Data loaded to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table
        }

        /// <summary>
        /// Get method for all athletes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("CompetitionCreator/{username}")]
        public JsonResult GeCompsCreator(string username)
        {
            string query = @"
                             exec proc_athlete @username,'','','',0,'2000-12-12','','','','CompCreator'
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Table to save information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Opened Connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command created with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username); //Added parameter username
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Data loaded to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table
        }


        /// <summary>
        /// Get method for all athletes 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ChallengeCreator/{username}")]
        public JsonResult GeChallCreator(string username)
        {
            string query = @"
                             exec proc_athlete @username,'','','',0,'2000-12-12','','','','ChallCreator' 
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Table to save information
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Opened Connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command created with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username); //Added parameter username
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Data loaded to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table
        }


        /// <summary>
        /// Get method for a specific athlete
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Athlete by username</returns>

        [HttpGet("{username}")]
        public string GetAthlete(string username)
        {
            string lbl_username;
            string lbl_name;
            string lbl_lastname;
            string lbl_photo;
            string lbl_age;
            string lbl_birthdate;
            string lbl_pass;
            string lbl_nationality;
            string lbl_category;


            string query = @"
                             exec proc_athlete @user,'','','',0,'2000-12-12','','','','Select One'
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

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_username = row["username"].ToString();
                lbl_name = row["name"].ToString();
                lbl_lastname = row["lastname"].ToString();
                lbl_photo = row["photo"].ToString();
                lbl_age = row["age"].ToString();
                lbl_birthdate = row["birthDate"].ToString();
                lbl_pass = row["pass"].ToString();
                lbl_nationality = row["nationality"].ToString();
                lbl_category = row["category"].ToString();

                var data = new JObject(new JProperty("username", lbl_username), new JProperty("name", lbl_name),
                    new JProperty("lastname", lbl_lastname), new JProperty("photo", lbl_photo), new JProperty("age", Int32.Parse(lbl_age)),
                    new JProperty("birthDate", DateTime.Parse(lbl_birthdate)), new JProperty("pass", lbl_pass), new JProperty("nationality", lbl_nationality),
                    new JProperty("category", lbl_category));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
        }

        /// <summary>
        /// Method post to search athletes
        /// </summary>
        /// <param name="data"></param>
        /// <returns>List of athletes that have the specified name and lastname</returns>

        [HttpPost("Search")]
        public JsonResult SearchAthletes(JObject data)
        {
            DataTable table = new DataTable(); //Table created to store information

            var name = data["name"].ToString();
            var lastname = data["lastname"].ToString();

            if (lastname.Equals("")){

                string query = @"
                             exec proc_athlete '',@name,'','',0,'2000-12-12','','','','SearchName'
                            "; //Select query sent to SQL Server


                string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Created connection
                {
                    myCon.Open(); //Opens connection
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))//Created command with query and connection
                    {
                        myCommand.Parameters.AddWithValue("@name", name); //Added parameter name
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);//Information loades to table
                        myReader.Close();
                        myCon.Close(); //Closed connection
                    }
                }
            }
            else
            {
                string query = @"
                             exec proc_athlete '',@name,@lastname,'',0,'2000-12-12','','','','SearchLastName'
                            "; //Select query sent to SQL Server

                
                string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Created connection
                {
                    myCon.Open(); //Opens connection
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))//Created command with query and connection
                    {
                        myCommand.Parameters.AddWithValue("@name", name); //Added parameter name
                        myCommand.Parameters.AddWithValue("@lastname", lastname);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);//Information loades to table
                        myReader.Close();
                        myCon.Close(); //Closed connection
                    }
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table
        }

        /// <summary>
        /// Post method for athletes
        /// </summary>
        /// <param name="athlete"></param>
        /// <returns>Action result of query</returns>
        [HttpPost("LogIn")]
        public string LogInAthlete(Athlete athlete)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");

            //Insert query sent to SQL Server 

            string query = @"
                             exec proc_athlete @username,'','','',0,'2000-12-12',@pass,'','','LogIn'
                            ";
            DataTable table = new DataTable();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                SqlCommand myCommand = new SqlCommand(query, myCon);

                //Added parameters with values
                myCommand.Parameters.AddWithValue("@username", athlete.username);
                myCommand.Parameters.AddWithValue("@pass", athlete.pass);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            if (table.Rows.Count>0)
            {
               var data = new JObject(new JProperty("Existe", "Si"));

                return data.ToString();  //Returns table with info
            }
            else
            {
                var data =  new JObject(new JProperty("Existe", "No"));
                return data.ToString();  //Returns table with info
            } 


        }


        /// <summary>
        /// Post method for athletes
        /// </summary>
        /// <param name="athlete"></param>
        /// <returns>Action result of query</returns>
        [HttpPost]
        public JsonResult PostAthlete(Athlete athlete)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
           
            //Insert query sent to SQL Server 

            string query = @"
                             exec proc_athlete @username,@name,@lastname,@photo,@age,@birthdate,@pass,@nationality,@category,'Insert'
                            "; 
            DataTable table = new DataTable();
            
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                SqlCommand myCommand = new SqlCommand(query, myCon);
                
                //Added parameters with values
                myCommand.Parameters.AddWithValue("@username", athlete.username);
                myCommand.Parameters.AddWithValue("@name", athlete.name);
                myCommand.Parameters.AddWithValue("@lastname", athlete.lastname);
                myCommand.Parameters.AddWithValue("@photo", athlete.photo);
                myCommand.Parameters.AddWithValue("@age", athlete.age);
                myCommand.Parameters.AddWithValue("@birthdate", athlete.birthdate);
                myCommand.Parameters.AddWithValue("@pass", athlete.pass);
                myCommand.Parameters.AddWithValue("@nationality", athlete.nationality);
                myCommand.Parameters.AddWithValue("@category", athlete.category);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection
                
            }
  

            return new JsonResult(table); //Returns table with info

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
                             exec proc_athlete @username,@name,@lastname,@photo,@age,@birthdate,@pass,@nationality,@category,'Update'
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
                    myCommand.Parameters.AddWithValue("@username", athlete.username);
                    myCommand.Parameters.AddWithValue("@name", athlete.name);
                    myCommand.Parameters.AddWithValue("@lastname", athlete.lastname);
                    myCommand.Parameters.AddWithValue("@photo", athlete.photo);
                    myCommand.Parameters.AddWithValue("@age", athlete.age);
                    myCommand.Parameters.AddWithValue("@birthdate", athlete.birthdate);
                    myCommand.Parameters.AddWithValue("@pass", athlete.pass);
                    myCommand.Parameters.AddWithValue("@nationality", athlete.nationality);
                    myCommand.Parameters.AddWithValue("@category", athlete.category);

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

        [HttpDelete("{username}")]
        public ActionResult DeleteAthlete(string username)
        {
            string query = @"
                            exec proc_athlete @username,'','','',0,'2000-12-12','','','','Delete'
                            "; //delete query sent to SQL Server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");//Connection getted
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username); //Parameter added
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
