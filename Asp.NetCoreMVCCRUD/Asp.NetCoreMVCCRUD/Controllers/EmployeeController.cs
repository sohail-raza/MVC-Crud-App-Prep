using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Services;
using Asp.NetCoreMVCCRUD.Repository;
using Asp.NetCoreMVCCRUD.Models.DTOs;
using AutoMapper;

namespace Asp.NetCoreMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;


        // don't want to inject the repository in here at all
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost("Employee/Add")]
        public async Task<IActionResult> Add([FromBody] NewEmployeeDTO newEmployee)
        {
            // call into service to add the employee
            // depending on result return a result to the user
            if (!ModelState.IsValid)
            {
                return BadRequest(401);
            }

            var result = await _employeeService.AddEmployee(newEmployee);
            return Ok(result);

        }

        [HttpGet("Employee/GetAll")]
        public IActionResult GetAllEmployees()
        {
            var result = _employeeService.GetAllEmployees().ToList();
            if (result != null)
            {

                return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(result)); // brings in an instance of DTO so that unnecesary data from commandItem isnt shown.
            }
            return NotFound();
        }

        [HttpGet("Employee/GetEmployee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var result = _employeeService.GetEmployee(id);
            if (result != null)
            {

                return Ok(_mapper.Map<EmployeeDTO>(result)); // brings in an instance of DTO so that unnecesary data from commandItem isnt shown.
            }
            return NotFound();
        }

        //[HttpPut("UpdateEmployee/{id}")]
        //public IActionResult UpdateEmployee(int id, Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(401);
        //    }

        //    _employeeService.UpdateEmployee(id, employee);
        //    return Ok();

        //}


        [HttpDelete("Employee/Remove/{id}")]
        public IActionResult DeleteEmployee(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(401);
            }

            if (!_employeeService.DoesEmployeeExist(Id))
            {
                return NotFound();
            }

            var result = _employeeService.DeleteEmployee(Id);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return Ok("Failed");
            }
        }
    }
}