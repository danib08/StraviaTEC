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
using System.Globalization;

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

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
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



            if(table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_id = row["id"].ToString();
                lbl_name = row["name"].ToString();
                lbl_route = row["route"].ToString();
                lbl_date = row["date"].ToString();
                lbl_duration = row["duration"].ToString();
                lbl_kilometers = row["kilometers"].ToString();
                lbl_type = row["type"].ToString();
                lbl_ahlete_username = row["athleteUsername"].ToString();


                var data = new JObject(new JProperty("id", lbl_id), new JProperty("name", lbl_name),
                    new JProperty("route", lbl_route), new JProperty("date", DateTime.Parse(lbl_date)), new JProperty("duration", DateTime.Parse(lbl_duration)),
                    new JProperty("kilometers", float.Parse(lbl_kilometers)), new JProperty("type", lbl_type), new JProperty("athleteUsername", lbl_ahlete_username));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
       
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

            string query = @"
                             exec post_activity @id,@name,@route,@date,@duration,@kilometers,@type,@athleteusername
                            "; //Query insert sent to SQL Server
            DataTable table = new DataTable(); //Table created to store data
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open(); //Open connection
                SqlCommand myCommand = new SqlCommand(query, myCon); //Created command with query and connection

                //Adding parameters and values
                myCommand.Parameters.AddWithValue("@id", activity.id);
                myCommand.Parameters.AddWithValue("@name", activity.name);
                myCommand.Parameters.AddWithValue("@route", activity.route);
                myCommand.Parameters.AddWithValue("@date", activity.date);
                myCommand.Parameters.AddWithValue("@duration", activity.duration);
                myCommand.Parameters.AddWithValue("@kilometers", activity.kilometers);
                myCommand.Parameters.AddWithValue("@type", activity.type);
                myCommand.Parameters.AddWithValue("@athleteusername", activity.athleteusername);

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
                    myCommand.Parameters.AddWithValue("@id", activity.id);
                    myCommand.Parameters.AddWithValue("@name", activity.name);
                    myCommand.Parameters.AddWithValue("@route", activity.route);
                    myCommand.Parameters.AddWithValue("@date", activity.date);
                    myCommand.Parameters.AddWithValue("@kilometers", activity.kilometers);
                    myCommand.Parameters.AddWithValue("@type", activity.type);
                    myCommand.Parameters.AddWithValue("@athleteusername", activity.athleteusername);

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

        [HttpDelete("{id}")]
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
