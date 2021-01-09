using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        public bool AddDepartment(Department department)
        {
            _departmentRepository.AddDepartmentToDatabase(department);
            int results = _departmentRepository.SaveChanges();
            if (results == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDepartment(int Id)
        {

            _departmentRepository.DeleteDepartmentFromDatabase(Id);
            int results = _departmentRepository.SaveChanges();
            if (results == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments();
        }

        public Department GetDepartment(int Id)
        {
          return _departmentRepository.GetDepartment(Id);     
        }

        public void UpdateDepartment(int Id, Department Department)
        {

            _departmentRepository.UpdateDepartment(Id, Department);
            _departmentRepository.SaveChangesAsync();

        }
    }
}