using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Data.Interface;
using MVC_Project.Models;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MVC_Project.Controllers
{

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsRepository departmentRepository;
        public DepartmentsController(IDepartmentsRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var comId = Request.Cookies["ComId"];
            if (string.IsNullOrEmpty(comId))
            {
                return Problem("ComId cookie is missing or invalid.");
            }

            var departments = await departmentRepository.GetAllAsync(comId);

            return View(departments);

            //var CompanyList = await departmentRepository.GetAllAsync();
            //return View(CompanyList);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || departmentRepository == null)
            {
                return NotFound();
            }

            var department = await departmentRepository.Single(x => x.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
            //var singleCompany = await departmentRepository.Single(x => x.DeptId == id);
            //if (singleCompany == null)
            //{
            //    return NotFound();
            //}
            //return View(singleCompany);
        }

        // GET: Departments/Create
        public async Task<IActionResult> Create(string Id)
        {
            var companies = await departmentRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (string.IsNullOrWhiteSpace(Id))
            {
                return View(new Department()
                {
                    ComId = string.Empty
                });
            }
            else
            {
                var singleDepartment = await departmentRepository.Single(x => x.ComId == Id);
                if (singleDepartment == null)
                    return NotFound();
                return View(singleDepartment);
            }
            
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,DeptName")] Department department)
        {
            //if (ModelState.IsValid)
            //{
                await departmentRepository.Create(department);
                return RedirectToAction(nameof(Index));
            //}
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var companies = await departmentRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (id == null)
            {
                return NotFound();
            }

            var company = await departmentRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,DeptId,DeptName")] Department department)
        {
            if (id != department.DeptId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                var updatedCompany = await departmentRepository.Update(department, id);
                if (updatedCompany == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await departmentRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await departmentRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool>DepartmentExists(string id)
        {
            var company = await departmentRepository.GetById(id);
            return company != null;
        }
    }
}
