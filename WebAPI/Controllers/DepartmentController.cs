using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult DeptGet()
        {
            try
            {
                string query = @"
                select DepartmentId, DepartmentName from department
                ";
                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader Reader;
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        Reader = cmd.ExecuteReader();
                        dt.Load(Reader);

                        Reader.Close();
                        con.Close();
                    }
                }

                return new JsonResult(dt);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        [HttpPost]
        public JsonResult DeptPost(Department Dept)
        {
            try
            {
                string query = @"
                Insert into dbo.Department values(
                '" + Dept.DepartmentName + @"'    
                )
                ";
                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader Reader;
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        Reader = cmd.ExecuteReader();
                        dt.Load(Reader);

                        Reader.Close();
                        con.Close();
                    }
                }
                return new JsonResult("Added Successfully");
            }
            catch (Exception ex)
            { 
             return new JsonResult(ex.Message); 
            }
        }


        [HttpPut]
        public JsonResult DeptPut(Department Dept)
        {
            try
            {
                string query = @"
                    Update dbo.Department 
                    set DepartmentName = '" + Dept.DepartmentName+ @"'
                    where DepartmentId = '" + Dept.DepartmentId + @"'
                ";
                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader Reader;
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        Reader = cmd.ExecuteReader();
                        dt.Load(Reader);

                        Reader.Close();
                        con.Close();
                    }
                }
                return new JsonResult("Updated Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        [HttpDelete("{DepartmentId}")]
        public JsonResult DeptDelete(int DepartmentId)
        {
            try
            {
                string query = @"
                    Delete from dbo.Department 
                    where DepartmentId = '" + DepartmentId + @"'
                ";
                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader Reader;
                using (SqlConnection con = new SqlConnection(sqlDataSource))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        Reader = cmd.ExecuteReader();
                        dt.Load(Reader);

                        Reader.Close();
                        con.Close();
                    }
                }
                return new JsonResult("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
