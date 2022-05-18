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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Activity Controller with CRUD methods 
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        //Setting configuration to access connection 

        private readonly IConfiguration _configuration;

        public ActivityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for all activities
        /// </summary>
        /// <returns>Json result with all activities</returns>

        [HttpGet]
        public JsonResult GetActivities()
        {
            string query = @"
                             exec get_all_activities
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        /// <summary>
        /// Get method for a specific Activity
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Activity with given id</returns>
        
        [HttpGet("{id}")]
        public string GetActivity(string id)
        {
            string lbl_id;
            string lbl_name;
            string lbl_route;
            string lbl_date;
            string lbl_duration;
            string lbl_kilometers;
            string lbl_type;
            string lbl_ahlete_username;

            string query = @"
                            exec get_activity @id
                            ";  //Query select sent to SQL Server
            DataTable table = new DataTable(); //Table creation for saving data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec"); 
            SqlDataReader myReader; 
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection stated
            {
                myCon.Open(); //Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Created command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id); //Adding parameter to command
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads data to table
                    myReader.Close();
                    myCon.Close(); //Closing connection
                }
            }

            DataRow row = table.Rows[0];

            lbl_id = row["Id"].ToString();
            lbl_name = row["Name"].ToString();
            lbl_route = row["Route"].ToString();
            lbl_date = row["Date"].ToString();
            lbl_duration = row["Duration"].ToString();
            lbl_kilometers = row["Kilometers"].ToString();
            lbl_type = row["Type"].ToString();
            lbl_ahlete_username = row["AthleteUsername"].ToString();


            var data = new JObject(new JProperty("Id", lbl_id), new JProperty("Name", lbl_name),
                new JProperty("Route", lbl_route), new JProperty("Date", DateTime.Parse(lbl_date)), new JProperty("Duration", DateTime.Parse(lbl_duration)),
                new JProperty("Kilometers", float.Parse(lbl_kilometers)), new JProperty("Type", lbl_type), new JProperty("Athlete_username", lbl_ahlete_username));

            return data.ToString();
        }

        /// <summary>
        /// POst method to add Activities
        /// </summary>
        /// <param name="activity"></param>
        /// <returns>Action result of query</returns>

        [HttpPost]
        public JsonResult PostActivity(Activity activity)
        {
            
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");

        /*
            //Primary Key Validations
            string validation = @"select Id from dbo.Activity";

            DataTable validTable = new DataTable(); //Created table for validations
            SqlDataReader validReader;
            using (SqlConnection validCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                validCon.Open(); //Opened validation Connection
                using (SqlCommand validCommand = new SqlCommand(validation, validCon))//Command created with query and connection
                {
                    validCommand.Parameters.AddWithValue("@Id", activity.Id);
                    validReader = validCommand.ExecuteReader();
                    validTable.Load(validReader); //Usernames Data loaded to table
                    validReader.Close();
                    validCon.Close(); //Closed connection
                }
            }

            foreach (DataRow row in validTable.Rows) //Iterates the data stored in the table
            {
                foreach (var item in row.ItemArray)
                { //Gets the item in every row

                    if (item.Equals(activity.Id))
                    { //Verifies if is equal to the primary key

                        return BadRequest(); //Returns rejection

                    }

                }
            }
        */

            string query = @"
                             exec post_activity @id,@name,@route,@date,@kilometers,@type,@athleteusername
                            "; //Query insert sent to SQL Server
            DataTable table = new DataTable(); //Table created to store data
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Open connection
                SqlCommand myCommand = new SqlCommand(query, myCon); //Created command with query and connection

                //Adding parameters and values
                myCommand.Parameters.AddWithValue("@id", activity.Id);
                myCommand.Parameters.AddWithValue("@name", activity.Name);
                myCommand.Parameters.AddWithValue("@route", activity.Route);
                myCommand.Parameters.AddWithValue("@date", activity.Date);
                myCommand.Parameters.AddWithValue("@kilometers", activity.Kilometers);
                myCommand.Parameters.AddWithValue("@type", activity.Type);
                myCommand.Parameters.AddWithValue("@athleteusername", activity.AthleteUsername);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader); //Loads data to table
                myReader.Close();
                myCon.Close(); //Closing connection

            }

            
             return new JsonResult(table); //Returns table with info
            

        }

        /// <summary>
        /// Put method for activities
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>

        [HttpPut]
        public ActionResult PutActivity(Activity activity)
        {
            string query = @"
                             exec put_activity @id,@name,@route,@date,@kilometers,@type,@athleteusername
                            "; //Update query sent to SQL Server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open(); //Opens connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //
                {

                    //Parameters added with value
                    myCommand.Parameters.AddWithValue("@id", activity.Id);
                    myCommand.Parameters.AddWithValue("@name", activity.Name);
                    myCommand.Parameters.AddWithValue("@route", activity.Route);
                    myCommand.Parameters.AddWithValue("@date", activity.Date);
                    myCommand.Parameters.AddWithValue("@kilometers", activity.Kilometers);
                    myCommand.Parameters.AddWithValue("@type", activity.Type);
                    myCommand.Parameters.AddWithValue("@athleteusername", activity.AthleteUsername);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }

        /// <summary>
        /// Delete method for activities
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result of query</returns>

        [HttpDelete]
        public ActionResult DeleteActivity(string id)
        {
            string query = @"
                             exec delete_activity @id
                            "; //Delete query sent to SQL Server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id); //Added parameter
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
