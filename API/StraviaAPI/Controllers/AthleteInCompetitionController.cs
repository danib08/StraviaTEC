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
/// Athlete in Competition controller with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteInCompetitionController : ControllerBase
    {
        // Configuration to get connection string
        private readonly IConfiguration _configuration;

        public AthleteInCompetitionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all athletes in all competitions
        /// </summary>
        /// <returns>List of all athletes in competition</returns>
        [HttpGet]
        public JsonResult GetAthCompetitions()
        {
            //Query sent to SQL Server
            string query = @"
                             exec get_all_AICO
                            ";
            DataTable table = new DataTable(); //Created table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) // Connection created
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader(); //Reading command's data
                    table.Load(myReader); //Loads data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns table data
        }

        /// <summary>
        /// Method to get a specific athlete in a specific competition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="competition"></param>
        /// <returns>Json result with athlete in a competition</returns>

        [HttpGet("{id}/{competition}")]
        public string GetAthsComps(string id, string competition)
        {
            string lbl_athleteid;
            string lbl_competitionid;
            string lbl_status;

            //SQL query sent
            string query = @"
                             exec get_Athlete_Competition @athleteid,@competitionid
                            ";
            DataTable table = new DataTable(); //Created table to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection gettted
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Sql command with query and connection
                {
                    //Parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@competitionid", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Table filled
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_athleteid = row["AthleteID"].ToString();
                lbl_competitionid = row["CompetitionID"].ToString();
                lbl_status = row["Status"].ToString();
                

                var data = new JObject(new JProperty("AthleteID", lbl_athleteid), new JProperty("CompetitionID", lbl_competitionid),
                    new JProperty("Status", lbl_status) );

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
        [HttpGet("Athlete/{username}")]
        public JsonResult get_OneAth_Competitions(string username)
        {

            //SQL Query
            string query = @"
                             exec get_OneAth_Competitions @username
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Competition/{competition}")]
        public JsonResult get_Ath_OneCompetition(string competition)
        {

            //SQL Query
            string query = @"
                             exec get_Ath_OneCompetition @competition
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@competition", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Waiting/{competition}")]
        public JsonResult get_Ath_OneCompetition_Waiting(string competition)
        {

            //SQL Query
            string query = @"
                             exec get_Ath_OneCompetition_Waiting @competition
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@competition", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            return new JsonResult(table);//Returns table info
        }





        /// <summary>
        /// Method to add athlete in competition
        /// </summary>
        /// <param name="athlete_In_Comp"></param>
        /// <returns>Result of query</returns>

        [HttpPost]
        public JsonResult PostAthCompetition(Athlete_In_Competition athlete_In_Comp)
        {

            //Validaciones de Primary Key

            //SQL Server query sent
            string query = @"
                             exec post_Athlete_Competition @athleteid,@competitionid,@status
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader; 
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with its values
                myCommand.Parameters.AddWithValue("@athleteid", athlete_In_Comp.AthleteID);
                myCommand.Parameters.AddWithValue("@competitionid", athlete_In_Comp.CompetitionID);
                myCommand.Parameters.AddWithValue("@status", athlete_In_Comp.Status);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return new JsonResult(table); //Returns info stored in table

        }

        /// <summary>
        /// Delete method for athletes in competition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="competition"></param>
        /// <returns>Query result</returns>
        [HttpDelete]
        public ActionResult DeleteAthCompetition(string id, string competition)
        {
            //SQL Query
            string query = @"
                             exec delete_Athlete_Competition @athleteid,@competitionid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Created command with query and connection
                {
                    //Adding parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@competitionid", competition);

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
