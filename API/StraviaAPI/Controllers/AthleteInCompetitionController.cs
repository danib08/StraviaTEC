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
                             select * from dbo.Athlete_In_Competition
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
        public JsonResult GetAthCompetition(string id, string competition)
        {
            //SQL query sent
            string query = @"
                             select * from dbo.Athlete_In_Competition
                             where AthleteID = @AthleteID and CompetitionID = @CompetitionID)
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
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@CompetitionID", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Table filled
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }
            return new JsonResult(table); //Returns info stored in table
        }

        /// <summary>
        /// Method to add athlete in competition
        /// </summary>
        /// <param name="athlete_In_Comp"></param>
        /// <returns>Result of query</returns>

        [HttpPost]
        public ActionResult PostAthCompetition(Athlete_In_Competition athlete_In_Comp)
        {

            //Validaciones de Primary Key

            //SQL Server query sent
            string query = @"
                             insert into dbo.Athlete_In_Competition
                             values (@AthleteID,@CompetitionID,@Position,@Time)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader; 
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with its values
                myCommand.Parameters.AddWithValue("@AthleteID", athlete_In_Comp.AthleteID);
                myCommand.Parameters.AddWithValue("@CompetitionID", athlete_In_Comp.CompetitionID);
                myCommand.Parameters.AddWithValue("@Position", athlete_In_Comp.Position);
                myCommand.Parameters.AddWithValue("@Time", athlete_In_Comp.Time);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return Ok(); //Returns acceptance

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
                             delete from dbo.Athlete_In_Competition
                             where AthleteID = @AthleteID and  CompetitionID= @CompetitionID
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
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@CompetitionID", competition);

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
