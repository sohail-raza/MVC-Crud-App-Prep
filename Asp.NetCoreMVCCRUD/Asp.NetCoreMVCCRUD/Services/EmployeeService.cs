using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Models.DTOs;
using Asp.NetCoreMVCCRUD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees(); 
        }

        public Employee GetEmployee(int id)
        {

           var results = _employeeRepository.GetEmployee(id);
           
           return results;
          
        }


        public void UpdateEmployee(int Id, Employee Employee) //  need to change this. It doesn't accept Employee employee
        {
            //_employeeRepository.UpdateEmployee(Id,Employee);
            _employeeRepository.SaveChangesAsync();
 
        }


        public async Task<bool> AddEmployee(NewEmployeeDTO newEmployee)
        {

            Random rnd = new Random();
            int newCode = rnd.Next(1, 10000000);
            // Take a employee object
            // call the employee repository passing in the employee object
            // return true or false depending on whether the record was successfully added to the database
            Employee databaseEmployee = new Employee()
            {
                FullName = newEmployee.FullName,
                Position = newEmployee.Position,
                OfficeLocation = newEmployee.OfficeLocation,
                EmpCode = newCode,
                Department = newEmployee.Department,
                DepartmentId = newEmployee.DepartmentId
            };
            _employeeRepository.AddEmployeeToDatabase(databaseEmployee);
            int results = _employeeRepository.SaveChanges();
            if (results == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteEmployee(int Id)
        {

            _employeeRepository.DeleteEmployeeFromDatabase(Id);

            int results = _employeeRepository.SaveChanges();
            if (results == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesEmployeeExist(int Id)
        {
            try
            {
                return _employeeRepository.DoesEmployeeExist(Id);
            }
            catch
            {
                return false;
            }
        }

        public void UpdateEmployee(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
