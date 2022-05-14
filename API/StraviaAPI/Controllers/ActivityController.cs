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
                             select * from dbo.Activity
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
        public JsonResult GetActivity(string id)
        {
            string query = @"
                             select * from dbo.Activity 
                             where Id = @Id
                            ";  //Query select sent to SQL Server
            DataTable table = new DataTable(); //Table creation for saving data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec"); 
            SqlDataReader myReader; 
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection stated
            {
                myCon.Open(); //Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Created command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Id", id); //Adding parameter to command
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads data to table
                    myReader.Close();
                    myCon.Close(); //Closing connection
                }
            }
            return new JsonResult(table); //Returns table
        }

        /// <summary>
        /// POst method to add Activities
        /// </summary>
        /// <param name="activity"></param>
        /// <returns>Action result of query</returns>

        [HttpPost]
        public ActionResult PostActivity(Activity activity)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");

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

            string query = @"
                             insert into dbo.Activity
                             values (@Id,@Name,@Route,@Date,@Kilometers,@Type,@ChallengeID)
                            "; //Query insert sent to SQL Server
            DataTable table = new DataTable(); //Table created to store data
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Open connection
                SqlCommand myCommand = new SqlCommand(query, myCon); //Created command with query and connection

                //Adding parameters and values
                myCommand.Parameters.AddWithValue("@Id", activity.Id);
                myCommand.Parameters.AddWithValue("@Name", activity.Name);
                myCommand.Parameters.AddWithValue("@Route", activity.Route);
                myCommand.Parameters.AddWithValue("@Date", activity.Date);
                myCommand.Parameters.AddWithValue("@Kilometers", activity.Kilometers);
                myCommand.Parameters.AddWithValue("@Type", activity.Type);
                myCommand.Parameters.AddWithValue("@ChallengeID", activity.ChallengeID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader); //Loads data to table
                myReader.Close();
                myCon.Close(); //Closing connection

            }
            //Returns accpetance
            return Ok();

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
                             update dbo.Activity
                             set Name = @Name, Route = @Route, Date = @Date, 
                                 Kilometers = @Kilometers, Type = @Type, ChallengeID = @ChallengeID
                             where Id = @Id
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
                    myCommand.Parameters.AddWithValue("@Id", activity.Id);
                    myCommand.Parameters.AddWithValue("@Name", activity.Name);
                    myCommand.Parameters.AddWithValue("@Route", activity.Route);
                    myCommand.Parameters.AddWithValue("@Date", activity.Date);
                    myCommand.Parameters.AddWithValue("@Kilometers", activity.Kilometers);
                    myCommand.Parameters.AddWithValue("@Type", activity.Type);
                    myCommand.Parameters.AddWithValue("@ChallengeID", activity.ChallengeID);

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
                             delete from dbo.Activity
                             where Id = @Id
                            "; //Delete query sent to SQL Server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Id", id); //Added parameter
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
