using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
   public interface IEmployeeService
    {
        Task<bool> AddEmployee(NewEmployeeDTO employee);
        IEnumerable<Employee> GetAllEmployees();
        void UpdateEmployee(int Id);
        bool DeleteEmployee(int Id);
        Employee GetEmployee(int id);
        bool DoesEmployeeExist(int Id);








    }
}
