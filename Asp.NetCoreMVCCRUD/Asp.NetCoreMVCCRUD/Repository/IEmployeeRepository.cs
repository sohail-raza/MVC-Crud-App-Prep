using Asp.NetCoreMVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Repository
{
    public interface IEmployeeRepository
    {
        void AddEmployeeToDatabase(Employee employee);
        Task<int> SaveChangesAsync();   
        int SaveChanges(); //non-async

        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int Id);

        void DeleteEmployeeFromDatabase(int Id);

        void UpdateEmployee(int Id);

        bool DoesEmployeeExist(int Id);


    }
}
