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
/// Groups Controller with CRUD methods
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        //Configuration to get connection string
        private readonly IConfiguration _configuration;

        public GroupsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get method for all groups
        /// </summary>
        /// <returns>List of all existing groups</returns>
        [HttpGet]
        public JsonResult GetGroups()
        {
            //SQL Query sent
            string query = @"
                             exec proc_groups '','','Select'
                            ";
            DataTable table = new DataTable(); //DataTable to store info
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection stablished
            {
                myCon.Open();//Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Loading  data to tabla
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }

        /// <summary>
        /// Method to get a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Required group</returns>
        [HttpGet("Group/{name}")]
        public string GetGroup(string name)
        {
            
            string lbl_name;
            string lbl_adminuser;

            //SQL Query
            string query = @"
                             exec proc_groups @name,'','Select One'
                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@name", name);
                    
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_adminuser = row["AdminUsername"].ToString();
                lbl_name = row["Name"].ToString();

                var data = new JObject(new JProperty("adminUsername", lbl_adminuser), new JProperty("name", lbl_name));
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
        [HttpGet("{athlete}")]
        public JsonResult get_group_byAdmin(string athlete)
        {


            //SQL Query
            string query = @"
                             exec proc_groups '', @username,'GroupAdmin'

                            ";
            DataTable table = new DataTable();//Table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myCommand.Parameters.AddWithValue("@username", athlete);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Closed data
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info
        }



        /// <summary>
        /// Post method to create groups
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Query result</returns>
        [HttpPost]
        public JsonResult PostGroup(Groups group)
        {

            //Validaciones de Primary Key

            //SQL Query
            string query = @"
                             exec proc_groups @name,@adminusername,'Insert'
                            ";
            DataTable table = new DataTable(); 
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Opened connection
                SqlCommand myCommand = new SqlCommand(query, myCon);

                //Parameters added
                myCommand.Parameters.AddWithValue("@name", group.name);
                myCommand.Parameters.AddWithValue("@adminusername", group.adminusername);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table info

        }

        /// <summary>
        /// Method to update group info
        /// </summary>
        /// <param name="group"></param>
        /// <returns>Query result</returns>
        [HttpPut]
        public ActionResult PutGroup(Groups group)
        {
            //SQL Query
            string query = @"
                             exec proc_groups @name,@adminusername,'Update'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//SQL Connection set
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    //Parameters added
                    myCommand.Parameters.AddWithValue("@adminusername", group.adminusername);
                    myCommand.Parameters.AddWithValue("@name", group.name);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();//Closed connection
                }
            }
            return Ok();//Returns acceptance
        }

        /// <summary>
        /// Delete method for groups
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Query result</returns>
        [HttpDelete("{name}")]
        public ActionResult DeleteGroup(string name)
        {
            //SQL Query
            string query = @"
                             exec proc_groups @name,'','Delete'
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection setted
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", name);

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
