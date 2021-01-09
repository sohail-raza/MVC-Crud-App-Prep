using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVCCRUD.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;

        }

        [HttpPost("Department/Add")]
        public IActionResult Add([FromBody] Department department)
        {
            // call into service to add the employee
            // depending on result return a result to the user
            if (!ModelState.IsValid)
            {
                return BadRequest(401);
            }

            var result = _departmentService.AddDepartment(department);
            return Ok(result);
        }

        [HttpDelete("Department/Remove/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _departmentService.DeleteDepartment(id);
            return Ok(result);

        }


        [HttpGet("Department/GetAll")]
        public IActionResult GetAllDepartments()
        {
            var result = _departmentService.GetAllDepartments().ToList();
            if (result != null)
            {

                return Ok(_mapper.Map<IEnumerable<DepartmentDTO>>(result));
            }
            return NotFound();

        }

        [HttpGet("Department/GetDepartment/{id}")]
        public IActionResult GetDepartment(int id)
        {
            var result = _departmentService.GetDepartment(id);
            if (result != null)
            {

                return Ok(_mapper.Map<DepartmentDTO>(result)); // brings in an instance of DTO so that unnecesary data from commandItem isnt shown.
            }
            return NotFound();
        }
    }
}
