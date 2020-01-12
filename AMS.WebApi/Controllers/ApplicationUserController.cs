using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AMS.WebApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ApplicationUserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: api/ApplicationUser
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApplicationUser/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApplicationUser
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] ApplicationUser user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("AMSConnectionString")))
                {
                    var parameter = new
                    {
                        EmployeeID = user.EmployeeID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Active = user.Active,
                        ManagerID = user.ManagerID,
                        SupervisorID= user.SupervisorID,
                        CellLeadID = user.CellLeadID,
                        GradeID = user.GradeID,
                        DepartmentID = user.DepartmentID,
                        EmployeeRoles = string.Join(',',user.RoleList)
                    };
                    var result = connection.Execute("usp_Employee_Update", parameter, commandType: CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        // POST: api/ApplicationUser
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("AMSConnectionString")))
                {
                    var parameter = new
                    {
                        Email = user.Email
                    };
                    var result = connection.Query<UserRole>("usp_EmployeeRolesByEmail_Get", parameter, commandType: CommandType.StoredProcedure);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/ApplicationUser/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
