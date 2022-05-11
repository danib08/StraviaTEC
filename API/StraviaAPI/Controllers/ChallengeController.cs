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
    public class ChallengeController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ChallengeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetChallenges()
        {
            string query = @"
                             select * from dbo.Challenge
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
        public JsonResult GetChallenge(string id)
        {
            string query = @"
                             select * from dbo.Challenge
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
        public ActionResult PostChallenge(Challenge challenge)
        {

            //Validaciones de Primary Key

            string query = @"
                             insert into dbo.Challenge
                             values (@Id,@Name,@StartDate,@EndDate,@Privacy,@Kilometers,@Type)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);

                myCommand.Parameters.AddWithValue("@Id", challenge.Id);
                myCommand.Parameters.AddWithValue("@Name", challenge.Name);
                myCommand.Parameters.AddWithValue("@StartDate", challenge.StartDate);
                myCommand.Parameters.AddWithValue("@EndDate", challenge.EndDate);
                myCommand.Parameters.AddWithValue("@Privacy", challenge.Privacy);
                myCommand.Parameters.AddWithValue("@Kilometers", challenge.Kilometers);
                myCommand.Parameters.AddWithValue("@Type", challenge.Type);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();

            }

            return Ok();

        }

        [HttpPut]
        public ActionResult PutActivity(Challenge challenge)
        {
            string query = @"
                             update dbo.Challenge
                             set Name = @Name, StartDate = @StartDate, EndDate = @EndDate, 
                                 Privacy = @Privacy, Kilometers = @Kilometers, Type = @Type
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
                    myCommand.Parameters.AddWithValue("@Id", challenge.Id);
                    myCommand.Parameters.AddWithValue("@Name", challenge.Name);
                    myCommand.Parameters.AddWithValue("@StartDate", challenge.StartDate);
                    myCommand.Parameters.AddWithValue("@EndDate", challenge.EndDate);
                    myCommand.Parameters.AddWithValue("@Privacy", challenge.Privacy);
                    myCommand.Parameters.AddWithValue("@Kilometers", challenge.Kilometers);
                    myCommand.Parameters.AddWithValue("@Type", challenge.Type);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteActivity(string id)
        {
            string query = @"
                             delete from dbo.Challenge
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
