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
/// Competition controller with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        //Configuration setted to get connection string
        private readonly IConfiguration _configuration;

        public CompetitionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all competitions
        /// </summary>
        /// <returns>List of competitions</returns>
        [HttpGet]
        public JsonResult GetCompetitions()
        {
            //SQL Query
            string query = @"
                             exec get_all_competition
                            ";
            DataTable table = new DataTable(); //Table created to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return new JsonResult(table);//Returns info from table
        }

        /// <summary>
        /// Method to get a specific competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Required competition</returns>
        
        [HttpGet("{id}")]
        public JsonResult GetCompetition(string id)
        {
            //SQL Query
            string query = @"
                             exec get_competition @id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //sql connection
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table);//Returns info from table
        }

        /// <summary>
        /// Method to add competitions
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public ActionResult PostCompetition(Competition competition)
        {

            //Validaciones de Primary Key

            //SQL Query sent
            string query = @"
                             exec post_competition @id,@name,@route,@date,@privacy,@bankaccount,@price,@activityid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open();//Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with required values
                myCommand.Parameters.AddWithValue("@id", competition.Id);
                myCommand.Parameters.AddWithValue("@name", competition.Name);
                myCommand.Parameters.AddWithValue("@route", competition.Route);
                myCommand.Parameters.AddWithValue("@date", competition.Date);
                myCommand.Parameters.AddWithValue("@privacy", competition.Privacy);
                myCommand.Parameters.AddWithValue("@bankaccount", competition.BankAccount);
                myCommand.Parameters.AddWithValue("@price", competition.Price);
                myCommand.Parameters.AddWithValue("@activityid", competition.ActivityID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return Ok();//Returns acceptance

        }

        /// <summary>
        /// Method to update competitions
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Query result</returns>
        [HttpPut]
        public ActionResult PutCompetition(Competition competition)
        {
            //SQL Query
            string query = @"
                             exec put_competition @id,@name,@route,@date,@privacy,@bankaccount,@price,@activityid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();//Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Parameters added
                    myCommand.Parameters.AddWithValue("@id", competition.Id);
                    myCommand.Parameters.AddWithValue("@name", competition.Name);
                    myCommand.Parameters.AddWithValue("@route", competition.Route);
                    myCommand.Parameters.AddWithValue("@date", competition.Date);
                    myCommand.Parameters.AddWithValue("@privacy", competition.Privacy);
                    myCommand.Parameters.AddWithValue("@bankaccount", competition.BankAccount);
                    myCommand.Parameters.AddWithValue("@price", competition.Price);
                    myCommand.Parameters.AddWithValue("@activityid", competition.ActivityID);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Connection closed
                }
            }
            return Ok();//Returns acceptance
        }

        /// <summary>
        /// Method to delete competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Query result</returns>
        [HttpDelete]
        public ActionResult DeleteCompetition(string id)
        {
            //SQL Query
            string query = @"
                             exec delete_competition @id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection setted
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok();//Returns accpetance
        }

    }
}
