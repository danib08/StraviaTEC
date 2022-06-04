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
/// Group Member 
/// </summary>

namespace StraviaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMemberController : ControllerBase
    {
        /// <summary>
        /// Configuration to get connection string
        /// </summary>
        private readonly IConfiguration _configuration;

        public GroupMemberController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to get all groups members
        /// </summary>
        /// <returns>List of all members in all gruops</returns>
        [HttpGet]
        public JsonResult GetGroupMembers()
        {
            //SQL Query
            string query = @"
                             exec get_all_groupMembers
                            ";
            DataTable table = new DataTable(); //Create table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))//Connection created
            {
                myCon.Open();//Connection opened
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Command with query and connection
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Loads data to table
                    myReader.Close();
                    myCon.Close();//Close connection
                }
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = ti.ToLower(column.ColumnName);
            }

            return new JsonResult(table);//Returns table data
        }

        /// <summary>
        /// Method to get a member of a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <param name="member"></param>
        /// <returns>Required group member</returns>
        [HttpGet("{name}/{member}")]
        public string GetGroupMember(string name, string member)
        {
            string lbl_groupname;
            string lbl_memberid;

            //SQL Query
            string query = @"
                             exec get_groupMember @groupname,@memberid
                            ";
            DataTable table = new DataTable(); //Create table to store data
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();//Open connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//SQL Command with query and connection
                {
                    //Added parameters
                    myCommand.Parameters.AddWithValue("@groupname", name);
                    myCommand.Parameters.AddWithValue("@memberid", member);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Connection
                }
            }

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                lbl_groupname = row["GroupName"].ToString();
                lbl_memberid = row["MemberID"].ToString();

                var data = new JObject(new JProperty("groupName", lbl_groupname), new JProperty("memberID", lbl_memberid));

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
        [HttpGet("Group/{name}")]
        public JsonResult get_One_groupMembers(string name)
        {

            //SQL Query
            string query = @"
                             exec get_One_groupMembers @name
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
        [HttpGet("Athlete/{username}")]
        public JsonResult get_groups_OneMembers(string username)
        {

            //SQL Query
            string query = @"
                             exec get_groups_OneMembers @username
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
        [HttpGet("NotInscribed/{username}")]
        public JsonResult get_not_inscribed_Groups(string username)
        {

            //SQL Query
            string query = @"
                             exec get_not_inscribed_Groups @username
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
        /// Method to add gruoup members
        /// </summary>
        /// <param name="groupMember"></param>
        /// <returns>Query result</returns>

        [HttpPost]
        public JsonResult PostGroupMember(Group_Member groupMember)
        {

            //Validaciones de Primary Key

            //SQL Query
            string query = @"
                             exec post_groupMember @groupname,@memberid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Connection opend
                SqlCommand myCommand = new SqlCommand(query, myCon); //Command with query and connection

                //Added parameters
                myCommand.Parameters.AddWithValue("@groupname", groupMember.groupname);
                myCommand.Parameters.AddWithValue("@memberid", groupMember.memberid);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return new JsonResult(table);//Returns table data

        }

        /// <summary>
        /// Method to delete group members
        /// </summary>
        /// <param name="name"></param>
        /// <param name="member"></param>
        /// <returns>Query result</returns>

        [HttpDelete("{name}/{member}")]
        public ActionResult DeleteGroupMember(string name, string member)
        {
            //SQL Query
            string query = @"
                             exec delete_groupMember @groupname,@memberid
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Connection with query and connection
                {
                    myCommand.Parameters.AddWithValue("@groupname", name);
                    myCommand.Parameters.AddWithValue("@memberid", member);

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
