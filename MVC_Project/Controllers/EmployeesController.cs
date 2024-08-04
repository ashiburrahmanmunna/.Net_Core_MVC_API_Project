using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Data.Implementation;
using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository employeesRepository;

        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            
            var EmployeeList = await employeesRepository.GetAllAsync();
            return View(EmployeeList);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var singleCompany = await employeesRepository.Single(x => x.EmpId == id);
            if (singleCompany == null)
            {
                return NotFound();
            }
            return View(singleCompany);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create(string Id)
        {
            var companies = await employeesRepository.GetCompanies();
            var departments = await employeesRepository.GetDepartments();
            var designations = await employeesRepository.GetDesignations();
            var shifts = await employeesRepository.GetShifts();
            ViewData["ComId"] = new SelectList(companies, "Value", "Text");
            ViewData["ComName"] = new SelectList(companies, "Value", "Text");
            ViewData["DeptId"] = new SelectList(departments, "Value", "Text");
            ViewData["DesigId"] = new SelectList(designations, "Value", "Text");
            ViewData["DeptName"] = new SelectList(departments, "Value", "Text");
            ViewData["DesigName"] = new SelectList(designations, "Value", "Text");
            ViewData["ShiftName"] = new SelectList(shifts, "Value", "Text");
            ViewData["ShiftId"] = new SelectList(shifts, "Value", "Text");

            //var companies = await employeesRepository.GetCompanies();
            //ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            //ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            //ViewData["DeptId"] = new SelectList(companies, "DeptId", "DeptId");
            //ViewData["DesigId"] = new SelectList(companies, "DesigId", "DesigId");
            //ViewData["DeptName"] = new SelectList(companies, "DeptName", "DeptName");
            //ViewData["DesigName"] = new SelectList(companies, "DesigName", "DesigName");
            //ViewData["ShiftName"] = new SelectList(companies, "ShiftName", "ShiftName");
            //ViewData["ShiftId"] = new SelectList(companies, "ShiftId", "ShiftId");
            if (string.IsNullOrWhiteSpace(Id))
            {
                return View(new Employee()
                {
                    ComId = string.Empty
                });
            }
            else
            {
                var singleCompany = await employeesRepository.Single(x => x.ComId == Id);
                if (singleCompany == null)
                    return NotFound();
                return View(singleCompany);
            }
        }


        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,EmpCode,EmpName,ShiftId,DeptId,DesigId,Gender,Gross,Basic,HRent,Medical,Others,dtJoin")] Employee employee)
        {
            string dtJoinString = employee.dtJoin.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            await employeesRepository.CreateEmployee(employee.ComId, employee.EmpId, employee.EmpCode, employee.EmpName, employee.ShiftId, employee.DeptId, employee.DesigId, employee.Gender, employee.Gross, dtJoinString);
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ComId,EmpCode,EmpName,ShiftId,DeptId,DesigId,Gender,Gross,Basic,HRent,Medical,Others,dtJoin")] Employee employee)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    await employeesRepository.Create(employee);
        //    return RedirectToAction(nameof(Index));
        //    //}
        //    //ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", employee.ComId);
        //    //ViewData["DeptId"] = new SelectList(_context.Departments, "DeptId", "DeptId", employee.DeptId);
        //    //ViewData["DesigId"] = new SelectList(_context.Designations, "DesigId", "DesigId", employee.DesigId);
        //    //ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId", employee.ShiftId);
        //    return View(employee);
        //}

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var companies = await employeesRepository.GetCompanies();
            var departments = await employeesRepository.GetDepartments();
            var designations = await employeesRepository.GetDesignations();
            var shifts = await employeesRepository.GetShifts();
            ViewData["ComId"] = new SelectList(companies, "Value", "Text");
            ViewData["ComName"] = new SelectList(companies, "Value", "Text");
            ViewData["DeptId"] = new SelectList(departments, "Value", "Text");
            ViewData["DesigId"] = new SelectList(designations, "Value", "Text");
            ViewData["DeptName"] = new SelectList(departments, "Value", "Text");
            ViewData["DesigName"] = new SelectList(designations, "Value", "Text");
            ViewData["ShiftName"] = new SelectList(shifts, "Value", "Text");
            ViewData["ShiftId"] = new SelectList(shifts, "Value", "Text");
            if (id == null)
            {
                return NotFound();
            }

            var company = await employeesRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,EmpId,EmpCode,EmpName,ShiftId,DeptId,DesigId,Gender,Gross,Basic,HRent,Medical,Others,dtJoin")] Employee employee)
        {
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var updatedCompany = await employeesRepository.Update(employee, id);
            if (updatedCompany == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            //}
            return View(employee);
            ////}
            //ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", employee.ComId);
            //ViewData["DeptId"] = new SelectList(_context.Departments, "DeptId", "DeptId", employee.DeptId);
            //ViewData["DesigId"] = new SelectList(_context.Designations, "DesigId", "DesigId", employee.DesigId);
            //ViewData["ShiftId"] = new SelectList(_context.Shifts, "ShiftId", "ShiftId", employee.ShiftId);
            //return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await employeesRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // Delete related Attendances records
            var attendances = await employeesRepository.Where(a => a.EmpId == id).ToListAsync();
            foreach (var attendance in attendances)
            {
                await employeesRepository.Delete(attendance.EmpId);
            }

            // Delete the employee
            await employeesRepository.Delete(id);

            return RedirectToAction(nameof(Index));
            //await employeesRepository.Delete(id);

            //return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(string id)
        {
            var company = await employeesRepository.GetById(id);
            return company != null;
        }
    }
}
