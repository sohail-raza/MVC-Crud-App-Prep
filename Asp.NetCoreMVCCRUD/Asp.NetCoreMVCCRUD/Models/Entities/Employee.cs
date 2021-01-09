using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string FullName { get; set; }
        [Required]
        public int EmpCode { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string OfficeLocation { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }



    }
}
