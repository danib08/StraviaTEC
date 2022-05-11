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

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CompetitionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetCompetitions()
        {
            string query = @"
                             select * from dbo.Competition
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

        [HttpGet("{id}")]
        public JsonResult GetCompetition(string id)
        {
            string query = @"
                             select * from dbo.Competition
                             where Id = @Id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public ActionResult PostCompetition(Competition competition)
        {

            //Validaciones de Primary Key

            string query = @"
                             insert into dbo.Competition
                             values (@Id,@Name,@Route,@Date,@Privacy,@BankAccount,@Price, @ActivityID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);

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
                myCon.Close();

            }

            return Ok();

        }

        [HttpPut]
        public ActionResult PutCompetition(Competition competition)
        {
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
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
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
                    myCon.Close();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteCompetition(string id)
        {
            string query = @"
                             delete from dbo.Competition
                             where Id = @Id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok();
        }

    }
}
