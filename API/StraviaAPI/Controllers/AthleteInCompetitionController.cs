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
    public class AthleteInCompetitionController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AthleteInCompetitionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult GetAthCompetitions()
        {
            string query = @"
                             select * from dbo.Athlete_In_Competition
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

        [HttpGet("{id}/{competition}")]
        public JsonResult GetAthCompetition(string id, string competition)
        {
            string query = @"
                             select * from dbo.Athlete_In_Competition
                             where AthleteID = @AthleteID and CompetitionID = @CompetitionID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@CompetitionID", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public ActionResult PostAthCompetition(Athlete_In_Competition athlete_In_Comp)
        {

            //Validaciones de Primary Key

            string query = @"
                             insert into dbo.Athlete_In_Competition
                             values (@AthleteID,@CompetitionID,@Position,@Time)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);

                myCommand.Parameters.AddWithValue("@AthleteID", athlete_In_Comp.AthleteID);
                myCommand.Parameters.AddWithValue("@CompetitionID", athlete_In_Comp.CompetitionID);
                myCommand.Parameters.AddWithValue("@Position", athlete_In_Comp.Position);
                myCommand.Parameters.AddWithValue("@Time", athlete_In_Comp.Time);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();

            }

            return Ok();

        }

        [HttpDelete]
        public ActionResult DeleteAthCompetition(string id, string competition)
        {
            string query = @"
                             delete from dbo.Athlete_In_Competition
                             where AthleteID = @AthleteID and  CompetitionID= @CompetitionID
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@AthleteID", id);
                    myCommand.Parameters.AddWithValue("@CompetitionID", competition);

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
