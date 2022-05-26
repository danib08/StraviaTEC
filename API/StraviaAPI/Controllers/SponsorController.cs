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
/// Sponsor controller
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public SponsorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for all sponsor
        /// </summary>
        /// <returns>List of all sponsors</returns>
        [HttpGet]
        public JsonResult GetSponsors()
        {
            //Query sent 
            string query = @"
                             exec get_all_sponsors
                            ";
            DataTable table = new DataTable(); //Data table created to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns data table
        }

        /// <summary>
        /// Method to get a specific sponsor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Required sponsor</returns>
        [HttpGet("{id}")]
        public string GetSponsor(string id)
        {
            string lbl_id;
            string lbl_name;
            string lbl_bankaccount;
            string lbl_competitionid;

            //SQL Query
            string query = @"
                             exec get_sponsors @id
                            ";
            DataTable table = new DataTable();//Datatable to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Connection closed
                }
            }

            DataRow row = table.Rows[0];

            lbl_id = row["Id"].ToString();
            lbl_name = row["Name"].ToString();
            lbl_bankaccount = row["BankAccount"].ToString();
            lbl_competitionid = row["CompetitionID"].ToString();

            var data = new JObject(new JProperty("Id", lbl_id), new JProperty("Name", lbl_name),
                new JProperty("BankAccount", lbl_bankaccount), new JProperty("CompetitionID", lbl_competitionid));
            
            return data.ToString();
        }

        /// <summary>
        /// Method to add new sponsor
        /// </summary>
        /// <param name="sponsor"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public JsonResult PostSponsor(Sponsor sponsor)
        {

            //Validaciones de Primary Key
            //SQL Query

            string query = @"
                             exec post_sponsors @id,@name,@bankaccount,@competitionid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection stablished
            {
                myCon.Open();//Opened Connection
                SqlCommand myCommand = new SqlCommand(query, myCon);

                //Parameters added with value
                myCommand.Parameters.AddWithValue("@id", sponsor.Id);
                myCommand.Parameters.AddWithValue("@name", sponsor.Name);
                myCommand.Parameters.AddWithValue("@bankaccount", sponsor.BankAccount);
                myCommand.Parameters.AddWithValue("@competitionid", sponsor.CompetitionID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return new JsonResult(table); //Returns data table

        }

        /// <summary>
        /// Put method to update a sponsor
        /// </summary>
        /// <param name="sponsor"></param>
        /// <returns>Query result</returns>

        [HttpPut]
        public ActionResult PutSponsor(Sponsor sponsor)
        {
            //SQL Query
            string query = @"
                             exec put_sponsor @id,@name,@bankaccount,@competitionid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Added parameters with values
                    myCommand.Parameters.AddWithValue("@id", sponsor.Id);
                    myCommand.Parameters.AddWithValue("@name", sponsor.Name);
                    myCommand.Parameters.AddWithValue("@bankaccount", sponsor.BankAccount);
                    myCommand.Parameters.AddWithValue("@competitionid", sponsor.CompetitionID);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok();//Returns acceptance
        }

        /// <summary>
        /// Delete method for sponsors
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Query result</returns>
        [HttpDelete]
        public ActionResult DeleteGroup(string id)
        {
            //SQL Query
            string query = @"
                             exec delete_sponsors @id
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection setted
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok();//Returns acceptance
        }

    }
}


