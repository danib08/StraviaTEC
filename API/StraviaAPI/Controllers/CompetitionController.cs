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
                             select * from dbo.Competition
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
                             select * from dbo.Competition
                             where Id = @Id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //sql connection
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
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
                             insert into dbo.Competition
                             values (@Id,@Name,@Route,@Date,@Privacy,@BankAccount,@Price, @ActivityID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open();//Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with required values
                myCommand.Parameters.AddWithValue("@Id", competition.Id);
                myCommand.Parameters.AddWithValue("@Name", competition.Name);
                myCommand.Parameters.AddWithValue("@Route", competition.Route);
                myCommand.Parameters.AddWithValue("@Date", competition.Date);
                myCommand.Parameters.AddWithValue("@Privacy", competition.Privacy);
                myCommand.Parameters.AddWithValue("@BankAccount", competition.BankAccount);
                myCommand.Parameters.AddWithValue("@Price", competition.Price);
                myCommand.Parameters.AddWithValue("@ActivityID", competition.ActivityID);

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
                             update dbo.Competition
                             set Name = @Name, Route = @Route, Date = @Date, 
                                 Privacy = @Privacy, BankAccount = @BankAccount, Price = @Price, ActivityID = @ActivityID
                             where Id = @Id
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
                    myCommand.Parameters.AddWithValue("@Id", competition.Id);
                    myCommand.Parameters.AddWithValue("@Name", competition.Name);
                    myCommand.Parameters.AddWithValue("@Route", competition.Route);
                    myCommand.Parameters.AddWithValue("@Date", competition.Date);
                    myCommand.Parameters.AddWithValue("@Privacy", competition.Privacy);
                    myCommand.Parameters.AddWithValue("@BankAccount", competition.BankAccount);
                    myCommand.Parameters.AddWithValue("@Price", competition.Price);
                    myCommand.Parameters.AddWithValue("@ActivityID", competition.ActivityID);

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
                             delete from dbo.Competition
                             where Id = @Id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection setted
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
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
