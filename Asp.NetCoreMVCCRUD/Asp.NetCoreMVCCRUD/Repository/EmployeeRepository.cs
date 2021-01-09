using Asp.NetCoreMVCCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //database related

        private readonly CompanyDatabaseContext _context;
        public EmployeeRepository(CompanyDatabaseContext context)
        {
            _context = context;

        }
        public void AddEmployeeToDatabase(Employee employee)
        {  
            _context.Add(employee);
        }

        public void DeleteEmployeeFromDatabase(int Id)
        {
            var employee = _context.Employees.Find(Id);
            try
            {
                _context.Remove(employee);
            }
            catch
            {
               _context.Employees.ToList();
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployee(int Id)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == Id);
        } 

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        //public void UpdateEmployee(int Id, Employee employee)
        //{
        //    var record = GetEmployee(Id);
        //    record.FullName = employee.FullName;
        //    record.EmpCode = employee.EmpCode;
        //    record.Position = employee.Position;
        //    record.OfficeLocation = employee.OfficeLocation;

        //    _context.SaveChangesAsync();
        //}

        public bool DoesEmployeeExist(int Id)
        {
            try
            {
                return _context.Employees.AsNoTracking()
                    .Any(x => x.Id == Id);
            }

            catch (Exception exception)
            {
                throw;
            }
        }

        public void UpdateEmployee(int Id)
        {
            throw new NotImplementedException();
        }


    }
}
