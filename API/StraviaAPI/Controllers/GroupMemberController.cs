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
                             select * from dbo.Group_Member
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
            return new JsonResult(table);//Returns table data
        }

        /// <summary>
        /// Method to get a member of a specific group
        /// </summary>
        /// <param name="name"></param>
        /// <param name="member"></param>
        /// <returns>Required group member</returns>
        [HttpGet("{name}/{member}")]
        public JsonResult GetGroupMember(string name, string member)
        {
            //SQL Query
            string query = @"
                             select * from dbo.Group_Member
                             where GroupName = @GroupName and MemberID = @MemberID)
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
                    myCommand.Parameters.AddWithValue("@GroupName", name);
                    myCommand.Parameters.AddWithValue("@MemberID", member);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);//Load data to table
                    myReader.Close();
                    myCon.Close();//Connection
                }
            }
            return new JsonResult(table);//Returns data in table
        }

        /// <summary>
        /// Method to add gruoup members
        /// </summary>
        /// <param name="groupMember"></param>
        /// <returns>Query result</returns>

        [HttpPost]
        public ActionResult PostGroupMember(Group_Member groupMember)
        {

            //Validaciones de Primary Key

            //SQL Query
            string query = @"
                             insert into dbo.Athlete_In_Competition
                             values (@GroupName,@MemberID)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Connection opend
                SqlCommand myCommand = new SqlCommand(query, myCon); //Command with query and connection

                //Added parameters
                myCommand.Parameters.AddWithValue("@GroupName", groupMember.GroupName);
                myCommand.Parameters.AddWithValue("@MemberID", groupMember.MemberID);

                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();//Closed connection

            }

            return Ok();//Returns acceptance

        }

        /// <summary>
        /// Method to delete group members
        /// </summary>
        /// <param name="name"></param>
        /// <param name="member"></param>
        /// <returns>Query result</returns>

        [HttpDelete]
        public ActionResult DeleteGroupMember(string name, string member)
        {
            //SQL Query
            string query = @"
                             delete from dbo.Athlete_In_Challenge
                             where GroupName = @GroupName and  MemberID= @MemberID
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StraviaTec");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open(); //Opened connection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))//Connection with query and connection
                {
                    myCommand.Parameters.AddWithValue("@GroupName", name);
                    myCommand.Parameters.AddWithValue("@MemberID", member);

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
