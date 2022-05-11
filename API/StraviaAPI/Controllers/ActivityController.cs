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
    public class ActivityController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ActivityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetActivities()
        {
            string query = @"
                             select * from dbo.Activity
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
        public JsonResult GetActivity(string id)
        {
            string query = @"
                             select * from dbo.Activity
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
        public ActionResult PostActivity(Activity activity)
        {

            //Validaciones de Primary Key

            string query = @"
                             insert into dbo.Activity
                             values (@Id,@Name,@Route,@Date,@Kilometers,@Type,@ChallengeID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);

                myCommand.Parameters.AddWithValue("@Id", activity.Id);
                myCommand.Parameters.AddWithValue("@Name", activity.Name);
                myCommand.Parameters.AddWithValue("@Route", activity.Route);
                myCommand.Parameters.AddWithValue("@Date", activity.Date);
                myCommand.Parameters.AddWithValue("@Kilometers", activity.Kilometers);
                myCommand.Parameters.AddWithValue("@Type", activity.Type);
                myCommand.Parameters.AddWithValue("@ChallengeID", activity.ChallengeID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();

            }

            return Ok();

        }

        [HttpPut]
        public ActionResult PutActivity(Activity activity)
        {
            string query = @"
                             update dbo.Activity
                             set Name = @Name, Route = @Route, Date = @Date, 
                                 Kilometers = @Kilometers, Type = @Type, ChallengeID = @ChallengeID
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
                    myCommand.Parameters.AddWithValue("@Id", activity.Id);
                    myCommand.Parameters.AddWithValue("@Name", activity.Name);
                    myCommand.Parameters.AddWithValue("@Route", activity.Route);
                    myCommand.Parameters.AddWithValue("@Date", activity.Date);
                    myCommand.Parameters.AddWithValue("@Kilometers", activity.Kilometers);
                    myCommand.Parameters.AddWithValue("@Type", activity.Type);
                    myCommand.Parameters.AddWithValue("@ChallengeID", activity.ChallengeID);

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
                             delete from dbo.Activity
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
