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
/// Competition categories controller with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionCategoriesController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public CompetitionCategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for competition categories
        /// </summary>
        /// <returns>List of competition categories</returns>
        [HttpGet]
        public JsonResult GetCompCategories()
        {
            //SQL Query
            string query = @"
                             exec get_all_compCategories
                            ";
            DataTable table = new DataTable(); //Create table to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return new JsonResult(table); //Returns json with all categories
        }

        /// <summary>
        /// Get method to get a specific category in a specific competition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns>Required categories in competition</returns>
        
        [HttpGet("{id}/{category}")]
        public string GetCompCategory(string id, string category)
        {
            string lbl_competitionid;
            string lbl_category;

            //SQL Query
            string query = @"
                             exec get_compCategories @competitionid,@category
                            ";
            DataTable table = new DataTable(); //Table created to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    //Parameters added with values
                    myCommand.Parameters.AddWithValue("@competitionid", id);
                    myCommand.Parameters.AddWithValue("@category", category);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Loaded data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_competitionid = row["CompetitionID"].ToString();
                lbl_category = row["Category"].ToString();

                var data = new JObject(new JProperty("CompetitionID", lbl_competitionid), new JProperty("Category", lbl_category));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Competition/{competition}")]
        public JsonResult get_OnecompCategories(string competition)
        {

            //SQL Query
            string query = @"
                             exec get_OnecompCategories @competition
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@competition", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Category/{category}")]
        public JsonResult get_comps_OneCategory(string category)
        {

            //SQL Query
            string query = @"
                             exec get_comps_OneCategory @category
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@category", category);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            return new JsonResult(table);//Returns table info
        }




        /// <summary>
        /// Post method to add categories in competitions
        /// </summary>
        /// <param name="compCategories"></param>
        /// <returns>Result of query</returns>
        [HttpPost]
        public JsonResult PostCompCategory(Competition_Categories compCategories)
        {

            //Validaciones de Primary Key

            //SQL Query
            string query = @"
                             exec post_compCategories @competitionid,@category
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open();//Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command qith query and connection

                //Parameters added
                myCommand.Parameters.AddWithValue("@competitionid", compCategories.CompetitionID);
                myCommand.Parameters.AddWithValue("@category", compCategories.Category);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return new JsonResult(table);//Returns required category in competition

        }

        /// <summary>
        /// Delete method for competition categories
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns>Query result</returns>
        [HttpDelete("{id}/{category}")]
        public ActionResult DeleteCompCategory(string id, string category)
        {
            //SQL Query sent 
            string query = @"
                             delete_compCategories @competitionid,@category
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection started
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    //Parameters added
                    myCommand.Parameters.AddWithValue("@competitionid", id);
                    myCommand.Parameters.AddWithValue("@category", category);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }
    }
}
