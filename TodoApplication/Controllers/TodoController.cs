﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TodoApplication.Controllers
{

    [ApiController]
    public class TodoController : ControllerBase
    {
        private IConfiguration _configuration;
        public TodoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("get_tasks")]
        public JsonResult get_tasks()
        {
            string query = "select * from todo";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("DOTNETProj");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                }
            }

            return new JsonResult(table);
        }

        [HttpPost("add_tasks")]
        public JsonResult add_tasks([FromForm] string task)
        {
            string query = "insert into todo values (@task)";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("DOTNETProj");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@task", task);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                }
            }

            return new JsonResult("Added SuccessFully");
        }

        [HttpPost("delete_tasks")]
        public JsonResult delete_tasks([FromForm] string id)
        {
            string query = "delete from todo where id=@id";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("DOTNETProj");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                }
            }

            return new JsonResult("Deleted SuccessFully");
        }
    }
}
