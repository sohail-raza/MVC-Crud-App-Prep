using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models
{
    public class EmployeeDTO
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public int EmpCode { get; set; }
        public string OfficeLocation { get; set; }
    }
}
