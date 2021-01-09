using Asp.NetCoreMVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Repository
{
    public interface IDepartmentRepository
    {
        Task<int> SaveChangesAsync();
        int SaveChanges(); //non-async

        void AddDepartmentToDatabase(Department department);

        void DeleteDepartmentFromDatabase(int id);

        void UpdateDepartment(int Id, Department Department);

        Department GetDepartment(int id);

        IEnumerable<Department> GetAllDepartments();

        //get all, get employee, add, delete, modify
    }
}
