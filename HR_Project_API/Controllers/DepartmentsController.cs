using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Project_API.Data;
using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HR_Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsRepository departmentRepository;
        public DepartmentsController(IDepartmentsRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        // GET: Departme
        // nts
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            //var comId = Request.Cookies["ComId"];
            //if (string.IsNullOrEmpty(comId))
            //{
            //    return Problem("ComId cookie is missing or invalid.");
            //}
            try
            {
                var departments = await departmentRepository.GetAllAsync();

                return Ok(departments);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }


            //var CompanyList = await departmentRepository.GetAllAsync();
            //return View(CompanyList);
        }

        // GET: Departments/Details/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                //if (id == null || departmentRepository == null)
                //{
                //    return NotFound();
                //}

                var singleDepartment = await departmentRepository.Single(x => x.DeptId == id);
                if (singleDepartment == null)
                {
                    return NotFound();
                }

                return Ok(singleDepartment);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: Departments/Create
        //public async Task<IActionResult> Create(string Id)
        //{
        //    var companies = await departmentRepository.GetCompanies();
        //    //ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
        //    //ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
        //    if (string.IsNullOrWhiteSpace(Id))
        //    {
        //        return Ok(new Department()
        //        {
        //            ComId = string.Empty
        //        });
        //    }
        //    else
        //    {
        //        var singleDepartment = await departmentRepository.Single(x => x.ComId == Id);
        //        if (singleDepartment == null)
        //            return NotFound();
        //        return Ok(singleDepartment);
        //    }
            
        //}

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Department department)
        {
            try
            {
                await departmentRepository.Create(department);
                return Ok("Saved Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: Departments/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    var companies = await departmentRepository.GetCompanies();
        //    //ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
        //    //ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var company = await departmentRepository.GetById(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(company);
        //}

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id,Department department)
        {
            try
            {
                if (id != department.DeptId)
                {
                    return NotFound();
                }
                var updatedCompany = await departmentRepository.Update(department, id);
                if (updatedCompany == null)
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

        // GET: Departments/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var company = await departmentRepository.GetById(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(company);
        //}

        // POST: Departments/Delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await departmentRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
        

        //private async Task<bool>DepartmentExists(string id)
        //{
        //    var company = await departmentRepository.GetById(id);
        //    return company != null;
        //}
    }
}
