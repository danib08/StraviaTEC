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
using System.Globalization;


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
                            exec proc_competition '','','','','','',0.0,'','Select'
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

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns info from table
        }

        /// <summary>
        /// Method to get a specific competition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Required competition</returns>
        
        [HttpGet("{id}")]
        public string GetCompetition(string id)
        {
            string lbl_id;
            string lbl_name;
            string lbl_route;
            string lbl_date;
            string lbl_privacy;
            string lbl_bankaccount;
            string lbl_price;
            string lbl_activityid;

            //SQL Query
            string query = @"
                            exec proc_competition @id,'','','','','',0.0,'','Select One'
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

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_id = row["id"].ToString();
                lbl_name = row["name"].ToString();
                lbl_route = row["route"].ToString();
                lbl_date = row["date"].ToString();
                lbl_privacy = row["privacy"].ToString();
                lbl_bankaccount = row["bankAccount"].ToString();
                lbl_price = row["price"].ToString();
                lbl_activityid = row["activityID"].ToString();

                var data = new JObject(new JProperty("id", lbl_id), new JProperty("name", lbl_name),
                   new JProperty("route", lbl_route), new JProperty("date", DateTime.Parse(lbl_date)), new JProperty("privacy", lbl_privacy),
                   new JProperty("bankaccount", lbl_bankaccount), new JProperty("price", float.Parse(lbl_price)), new JProperty("activityid", lbl_activityid));

                return data.ToString();
            }
            else
            {
                var data = new JObject(new JProperty("Existe", "no"));
                return data.ToString();
            }

        }




        /// <summary>
        /// Method to add competitions
        /// </summary>
        /// <param name="competition"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public JsonResult PostCompetition(Competition competition)
        {

            //Validaciones de Primary Key

            //SQL Query sent
            string query = @"
                             exec proc_competition @id,@name,@route,@date,@privacy,@bankaccount,@price,@activityid,'Insert'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection started
            {
                myCon.Open();//Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with required values
                myCommand.Parameters.AddWithValue("@id", competition.id);
                myCommand.Parameters.AddWithValue("@name", competition.name);
                myCommand.Parameters.AddWithValue("@route", competition.route);
                myCommand.Parameters.AddWithValue("@date", competition.date);
                myCommand.Parameters.AddWithValue("@privacy", competition.privacy);
                myCommand.Parameters.AddWithValue("@bankaccount", competition.bankaccount);
                myCommand.Parameters.AddWithValue("@price", competition.price);
                myCommand.Parameters.AddWithValue("@activityid", competition.activityid);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return new JsonResult(table);//Returns info from table

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
                             exec proc_competition @id,@name,@route,@date,@privacy,@bankaccount,@price,@activityid,'Update'
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
                    myCommand.Parameters.AddWithValue("@id", competition.id);
                    myCommand.Parameters.AddWithValue("@name", competition.name);
                    myCommand.Parameters.AddWithValue("@route", competition.route);
                    myCommand.Parameters.AddWithValue("@date", competition.date);
                    myCommand.Parameters.AddWithValue("@privacy", competition.privacy);
                    myCommand.Parameters.AddWithValue("@bankaccount", competition.bankaccount);
                    myCommand.Parameters.AddWithValue("@price", competition.price);
                    myCommand.Parameters.AddWithValue("@activityid", competition.activityid);

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
        [HttpDelete("{id}")]
        public ActionResult DeleteCompetition(string id)
        {
            //SQL Query
            string query = @"
                            exec proc_competition @id,'','','','','',0.0,'','Delete'
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
