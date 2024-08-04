using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository employeesRepository;

        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        // GET: Employees
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var employeeList = await employeesRepository.GetAllAsync();
                return Ok(employeeList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        // GET: Employees/Details/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var singleEmployee = await employeesRepository.Single(x => x.EmpId == id);
                if (singleEmployee == null)
                {
                    return NotFound();
                }
                return Ok(singleEmployee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: Employees/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            try
            {
                await employeesRepository.Create(model);
                return Ok("Save Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Employee model)
        {
            try
            {
                if (id != model.EmpId)
                {
                    return BadRequest("ID Mismatch");
                }

                var updatedEmployee = await employeesRepository.Update(model, id);
                if (updatedEmployee == null)
                {
                    return NotFound();
                }
                return Ok("Updated Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: Employees/Edit/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await employeesRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
    }
}
