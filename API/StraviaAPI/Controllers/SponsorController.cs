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
    public class SponsorController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public SponsorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetSponsors()
        {
            string query = @"
                             select * from dbo.Sponsor
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
        public JsonResult GetSponsor(string id)
        {
            string query = @"
                             select * from dbo.Sponsor
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
        public ActionResult PostSponsor(Sponsor sponsor)
        {

            //Validaciones de Primary Key

            string query = @"
                             insert into dbo.Sponsor
                             values (@Id,@Name,@BankAccount,@CompetitionID,@ChallengeID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);

                myCommand.Parameters.AddWithValue("@Id", sponsor.Id);
                myCommand.Parameters.AddWithValue("@Name", sponsor.Name);
                myCommand.Parameters.AddWithValue("@BankAccount", sponsor.BankAccount);
                myCommand.Parameters.AddWithValue("@CompetitionID", sponsor.CompetitionID);
                myCommand.Parameters.AddWithValue("@ChallengeID", sponsor.ChallengeID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();

            }

            return Ok();

        }

        [HttpPut]
        public ActionResult PutSponsor(Sponsor sponsor)
        {
            string query = @"
                             update dbo.Sponsor
                             set Name = @Name, BankAccount = @BankAccount,  
                                 CompetitionID = @CompetitionID, ChallengeID = @ChallengeID
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
                    myCommand.Parameters.AddWithValue("@Id", sponsor.Id);
                    myCommand.Parameters.AddWithValue("@Name", sponsor.Name);
                    myCommand.Parameters.AddWithValue("@BankAccount", sponsor.BankAccount);
                    myCommand.Parameters.AddWithValue("@CompetitionID", sponsor.CompetitionID);
                    myCommand.Parameters.AddWithValue("@ChallengeID", sponsor.ChallengeID);

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
