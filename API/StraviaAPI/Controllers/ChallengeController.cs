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
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table data
        }

        /// <summary>
        /// Method to get challenge by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Json with challenge information</returns>
        [HttpGet("{id}")]
        public string GetChallenge(string id)
        {

            string lbl_id;
            string lbl_name;
            string lbl_startdate;
            string lbl_enddate;
            string lbl_privacy;
            string lbl_kilometers;
            string lbl_type;

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

            if (table.Rows.Count > 0)
            {

                DataRow row = table.Rows[0];

                lbl_id = row["id"].ToString();
                lbl_name = row["name"].ToString();
                lbl_startdate = row["startDate"].ToString();
                lbl_enddate = row["endDate"].ToString();
                lbl_privacy = row["privacy"].ToString();
                lbl_kilometers = row["kilometers"].ToString();
                lbl_type = row["type"].ToString();

                var data = new JObject(new JProperty("id", lbl_id), new JProperty("name", lbl_name),
                   new JProperty("startDate", DateTime.Parse(lbl_startdate)), new JProperty("endDate", DateTime.Parse(lbl_enddate)), new JProperty("privacy", lbl_privacy),
                   new JProperty("kilometers", float.Parse(lbl_kilometers)), new JProperty("type", lbl_type));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }

        }

        /// <summary>
        /// Post method to create challenges
        /// </summary>
        /// <param name="challenge"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public JsonResult PostChallenge(Challenge challenge)
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
                myCommand.Parameters.AddWithValue("@id", challenge.id);
                myCommand.Parameters.AddWithValue("@name", challenge.name);
                myCommand.Parameters.AddWithValue("@startdate", challenge.startDate);
                myCommand.Parameters.AddWithValue("@enddate", challenge.endDate);
                myCommand.Parameters.AddWithValue("@privacy", challenge.privacy);
                myCommand.Parameters.AddWithValue("@kilometers", challenge.kilometers);
                myCommand.Parameters.AddWithValue("@type", challenge.type);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return new JsonResult(table); //Returns table with info

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
                    myCommand.Parameters.AddWithValue("@id", challenge.id);
                    myCommand.Parameters.AddWithValue("@name", challenge.name);
                    myCommand.Parameters.AddWithValue("@startdate", challenge.startDate);
                    myCommand.Parameters.AddWithValue("@enddate", challenge.endDate);
                    myCommand.Parameters.AddWithValue("@privacy", challenge.privacy);
                    myCommand.Parameters.AddWithValue("@kilometers", challenge.kilometers);
                    myCommand.Parameters.AddWithValue("@type", challenge.type);

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
        [HttpDelete("{id}")]
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
