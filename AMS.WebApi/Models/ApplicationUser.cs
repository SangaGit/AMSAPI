using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.WebApi.Models
{
    public class ApplicationUser
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public int ManagerID { get; set; }
        public int SupervisorID { get; set; }
        public int CellLeadID { get; set; }
        public int GradeID { get; set; }
        public int DepartmentID { get; set; }
        public int[] RoleList { get; set; }
    }
}
