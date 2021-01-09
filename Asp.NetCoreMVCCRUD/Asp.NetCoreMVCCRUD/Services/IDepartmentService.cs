using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.NetCoreMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVCCRUD.Services
{
    public interface IDepartmentService
    {
        bool AddDepartment(Department department);
        bool DeleteDepartment(int Id);
        void UpdateDepartment(int Id, Department department);
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartment(int id);


    }
}
