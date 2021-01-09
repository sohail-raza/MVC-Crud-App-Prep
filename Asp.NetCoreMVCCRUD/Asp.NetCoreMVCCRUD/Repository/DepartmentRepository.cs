using Asp.NetCoreMVCCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDatabaseContext _context;
        public DepartmentRepository(CompanyDatabaseContext context)
        {
            _context = context;
        }
        public void AddDepartmentToDatabase(Department department)
        {
            _context.Add(department);
        }

        public void DeleteDepartmentFromDatabase(int Id)
        {
            var department = _context.Departments.Find(Id);
            _context.Remove(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartment(int Id)
        {
            return _context.Departments.FirstOrDefault(e => e.DepartmentId == Id);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void UpdateDepartment(int Id, Department Department)
        {
            var record = GetDepartment(Id);


            record.DepartmentId = Department.DepartmentId;
            record.DepartmentName = Department.DepartmentName;




        }
    }
}
