using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StraviaAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AthleteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                             select * from dbo.Athlete
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
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

        [HttpGet("{username}")]
        public JsonResult GetAthlete(string username)
        {
            string query = @"
                             select * from dbo.Athlete
                             where Username = @Username
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", username);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public ActionResult Post(Athlete athlete)
        {

            

            string query = @"
                             insert into dbo.Athlete
                             values (@Username,@Name,@LastName,@Photo,@Age,@BirthDate,@Pass,@Nationality,@Category)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);
                
                myCommand.Parameters.AddWithValue("@Username", athlete.Username);
                myCommand.Parameters.AddWithValue("@Name", athlete.Name);
                myCommand.Parameters.AddWithValue("@LastName", athlete.LastName);
                myCommand.Parameters.AddWithValue("@Photo", athlete.Photo);
                myCommand.Parameters.AddWithValue("@Age", athlete.Age);
                myCommand.Parameters.AddWithValue("@BirthDate", athlete.BirthDate);
                myCommand.Parameters.AddWithValue("@Pass", athlete.Pass);
                myCommand.Parameters.AddWithValue("@Nationality", athlete.Nationality);
                myCommand.Parameters.AddWithValue("@Category", athlete.Category);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
                
            }

            return Ok();

        }

        

        [HttpPut]
        public ActionResult Put(Athlete athlete)
        {
            string query = @"
                             update dbo.Athlete
                             set Name = @Name, LastName = @LastName, Photo = @Photo, 
                                 Pass = @Pass, Nationality = @Nationality, Category = @Category
                             where Username = @Username
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", athlete.Username);
                    myCommand.Parameters.AddWithValue("@Name", athlete.Name);
                    myCommand.Parameters.AddWithValue("@LastName", athlete.LastName);
                    myCommand.Parameters.AddWithValue("@Photo", athlete.Photo);
                    myCommand.Parameters.AddWithValue("@Age", athlete.Age);
                    myCommand.Parameters.AddWithValue("@BirthDate", athlete.BirthDate);
                    myCommand.Parameters.AddWithValue("@Pass", athlete.Pass);
                    myCommand.Parameters.AddWithValue("@Nationality", athlete.Nationality);
                    myCommand.Parameters.AddWithValue("@Category", athlete.Category);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(string username)
        {
            string query = @"
                             delete from dbo.Athlete
                             where Username = @Username
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", username);
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
