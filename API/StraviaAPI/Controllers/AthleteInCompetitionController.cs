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
/// Athlete in Competition controller with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteInCompetitionController : ControllerBase
    {
        // Configuration to get connection string
        private readonly IConfiguration _configuration;

        public AthleteInCompetitionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all athletes in all competitions
        /// </summary>
        /// <returns>List of all athletes in competition</returns>
        [HttpGet]
        public JsonResult GetAthCompetitions()
        {
            //Query sent to SQL Server
            string query = @"
                             exec proc_athlete_in_competition '','','','','','Select'
                            ";
            DataTable table = new DataTable(); //Created table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) // Connection created
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    myReader = myCommand.ExecuteReader(); //Reading command's data
                    table.Load(myReader); //Loads data to table
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table); //Returns table data
        }

        /// <summary>
        /// Method to get a specific athlete in a specific competition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="competition"></param>
        /// <returns>Json result with athlete in a competition</returns>

        [HttpGet("{id}/{competition}")]
        public string GetAthsComps(string id, string competition)
        {
            string lbl_athleteid;
            string lbl_competitionid;
            string lbl_status;

            //SQL query sent
            string query = @"
                             exec proc_athlete_in_competition @athleteid,@competitionid,'','','','Select'
                            ";
            DataTable table = new DataTable(); //Created table to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) //Connection gettted
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Sql command with query and connection
                {
                    //Parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@competitionid", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); //Table filled
                    myReader.Close();
                    myCon.Close(); //Connection closed
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_athleteid = row["athleteID"].ToString();
                lbl_competitionid = row["competitionID"].ToString();
                lbl_status = row["status"].ToString();
                

                var data = new JObject(new JProperty("athleteID", lbl_athleteid), new JProperty("competitionID", lbl_competitionid),
                    new JProperty("status", lbl_status) );

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
        [HttpGet("Athlete/{username}")]
        public JsonResult get_OneAth_Competitions(string username)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition @username,'','','','','AthleteCompetitions'
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }
            TextInfo ti2 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti2.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Competition/{competition}")]
        public JsonResult get_Ath_OneCompetition(string competition)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition '',@competition,'','','0','CompetitionAthletes'
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

            TextInfo ti3 = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti3.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("AcceptedAthletes/{competition}")]
        public JsonResult get_Ath_OneCompetition_Waiting(string competition)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition '',@competition,'','','','AcceptedToComp'
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

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Ended/{username}")]
        public JsonResult get_Ended_Comp(string username)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition @username,'','','','','AthleteEndedComp'
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("AcceptedComps/{username}")]
        public JsonResult getAcceptedComps(string username)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition @username,'','','','','AthleteAcceptedComp'
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("NotAcceptedAthletes/{competition}")]
        public JsonResult get_Not_Accepted_AIC(string competition)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition '', @competition,'','','','NotAccepted'
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

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }



        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("NotInscribed/{username}")]
        public JsonResult get_not_inscribed_Comp(string username)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition @username,'','','','','NotSubscribed'
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", username);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }



        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Report/{competition}")]
        public JsonResult get_comp_Report(string competition)
        {

            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition '', @competition,'','','','CompReport'
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

            TextInfo tiw = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = tiw.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }


        /// <summary>
        /// Method to add athlete in competition
        /// </summary>
        /// <param name="athlete_In_Comp"></param>
        /// <returns>Result of query</returns>

        [HttpPost]
        public JsonResult PostAthCompetition(Athlete_In_Competition athlete_In_Comp)
        {

            //Validaciones de Primary Key

            //SQL Server query sent
            string query = @"
                             exec proc_athlete_in_competition @athleteid,@competitionid,@status,@receipt,@duration,'Insert'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader; 
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);//Command with query and connection

                //Parameters added with its values
                myCommand.Parameters.AddWithValue("@athleteid", athlete_In_Comp.athleteid);
                myCommand.Parameters.AddWithValue("@competitionid", athlete_In_Comp.competitionid);
                myCommand.Parameters.AddWithValue("@status", athlete_In_Comp.status);
                myCommand.Parameters.AddWithValue("@receipt", athlete_In_Comp.receipt);
                myCommand.Parameters.AddWithValue("@duration", athlete_In_Comp.duration);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close(); //Closed connection

            }

            return new JsonResult(table); //Returns info stored in table

        }

        /// <summary>
        /// Put method fot athletes
        /// </summary>
        /// <param name="athlete"></param>
        /// <returns>Acceptance of query</returns>

        [HttpPut]
        public ActionResult PutAthleteInCompetition(Athlete_In_Competition athlete_In_Comp)
        {
            string query = @"
                             exec proc_athlete_in_competition @athleteid,@competitionid,@status,@receipt,@duration,'Update'
                            "; //update query sent to sql server
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec"); //Connection started
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Command with query and connection
                {
                    //Added parameters with values
                    myCommand.Parameters.AddWithValue("@athleteid", athlete_In_Comp.athleteid);
                    myCommand.Parameters.AddWithValue("@competitionid", athlete_In_Comp.competitionid);
                    myCommand.Parameters.AddWithValue("@status", athlete_In_Comp.status);
                    myCommand.Parameters.AddWithValue("@receipt", athlete_In_Comp.receipt);
                    myCommand.Parameters.AddWithValue("@duration", athlete_In_Comp.duration);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }



        /// <summary>
        /// Delete method for athletes in competition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="competition"></param>
        /// <returns>Query result</returns>
        [HttpDelete]
        public ActionResult DeleteAthCompetition(string id, string competition)
        {
            //SQL Query
            string query = @"
                             exec proc_athlete_in_competition @athleteid,@competitionid,'','','','Delete'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) //Created command with query and connection
                {
                    //Adding parameters
                    myCommand.Parameters.AddWithValue("@athleteid", id);
                    myCommand.Parameters.AddWithValue("@competitionid", competition);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close(); //Closed connection
                }
            }
            return Ok(); //Returns acceptance
        }
    }
}
