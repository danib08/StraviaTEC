using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StraviaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Activity_In_ChallengeController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public Activity_In_ChallengeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
		
		/// <summary>
        /// Get method for all activity in challenges
        /// </summary>
        /// <returns>All activity in challenges</returns>

        [HttpGet]
        public JsonResult GetActivitiesChallenges()
        {
            string query = @"
                             exec get_all_ActChallenge
                            "; //Select query sent to SQL Server
            DataTable table = new DataTable(); //Created table to save data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Load data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table 
        }
		
		/// <summary>
        /// Get method of a specific activity for a specific challenge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendid"></param>
        /// <returns>Requested activity in a challenge</returns>

        [HttpGet("{activityid}/{challengeid}")]
        public string GetActChallenge(string activityid, string challengeid)
        {
            string lbl_activityid;
            string lbl_challengeid;

            string query = @"
                             exec get_ActChallenge @activityid,@challengeid
                            "; //Select query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@activityid", activityid);
                    myCommand.Parameters.AddWithValue("@challengeid", challengeid);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Loads info to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_activityid = row["ActivityID"].ToString();
                lbl_challengeid = row["ChallengeID"].ToString();

                var data = new JObject(new JProperty("activityID", lbl_activityid), new JProperty("challengeID", lbl_challengeid));

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
        [HttpGet("Activity/{id}")]
        public JsonResult get_OneAct_Challenges(string id)
        {

            //SQL Query
            string query = @"
                             exec get_OneAct_Challenges @id
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);

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
        [HttpGet("Challenge/{id}")]
        public JsonResult get_Acts_OneChallenge(string id)
        {

            //SQL Query
            string query = @"
                             exec get_Acts_OneChallenge @id
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);

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
        /// Post method for activity in challenge
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult PostActChallenge(Activity_In_Challenge actChallenge)
        {
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            //Primary Key validations



            string query = @"
                             exec post_ActChallenge @activityid,@challengeid
                            "; //Insert query sent to sql server
            DataTable table = new DataTable();
            
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open(); //Opens connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added
                myCommand.Parameters.AddWithValue("@activityid", actChallenge.activityid);
                myCommand.Parameters.AddWithValue("@challengeid", actChallenge.challengeid);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return new JsonResult(table); //Returns table

        }
		
		/// <summary>
        /// Delte method for activities in challenge
        /// </summary>
        /// <param name="activityid"></param>
        /// <param name="challengeid"></param>
        /// <returns>Result of query</returns>

        [HttpDelete("{activityid}/{challengeid}")]
        public ActionResult DeleteAthFriend(string activityid, string challengeid)
        {
            string query = @"
                             exec delete_ActChallenge @activityid,@challengeid
                            "; //Delete query sent to sql
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command eith query and connection
                {

                    //Added parameters with values
                    myCommand.Parameters.AddWithValue("@activityid", activityid);
                    myCommand.Parameters.AddWithValue("@challengeid", challengeid);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }
            return Ok(); //Returns Acceptance
        }

    }
}
