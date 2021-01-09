using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models.DTOs
{
    public class NewEmployeeDTO 
    {
        
        public string FullName { get; set; }
        public string Position { get; set; }
        public string OfficeLocation { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }

    }
}
