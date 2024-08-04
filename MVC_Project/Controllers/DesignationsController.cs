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
    public class DesignationsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDesignationsRepository designationRepository;
        public DesignationsController(IDesignationsRepository designationRepository)
        {
            this.designationRepository = designationRepository;
        }

        // GET: Designations
        public async Task<IActionResult> Index()
        {
            var comId = Request.Cookies["ComId"];
            if (string.IsNullOrEmpty(comId))
            {
                return Problem("ComId cookie is missing or invalid.");
            }

            var departments = await designationRepository.GetAllAsync(comId);

            return View(departments);

            //var CompanyList = await departmentRepository.GetAllAsync();
            //return View(CompanyList);
        }

        // GET: Designations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || designationRepository == null)
            {
                return NotFound();
            }

            var department = await designationRepository.Single(x => x.DesigId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Designations/Create
        public async Task<IActionResult> Create(string Id)
        {
            //ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId");
            //ViewData["ComName"] = new SelectList(_context.Companies, "ComName", "ComName");
            //return View();

            var companies = await designationRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (string.IsNullOrWhiteSpace(Id))
            {
                return View(new Designation()
                {
                    ComId = string.Empty
                });
            }
            else
            {
                var singleDepartment = await designationRepository.Single(x => x.ComId == Id);
                if (singleDepartment == null)
                    return NotFound();
                return View(singleDepartment);
            }

        }

        // POST: Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,DesigName")] Designation designation)
        {
            //if (ModelState.IsValid)
            //{
            await designationRepository.Create(designation);
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", designation.ComId);
            return View(designation);
        }

        // GET: Designations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var companies = await designationRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (id == null)
            {
                return NotFound();
            }

            var company = await designationRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,DesigId,DesigName")] Designation designation)
        {
            if (id != designation.DesigId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var updatedCompany = await designationRepository.Update(designation, id);
            if (updatedCompany == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            //}
            return View(designation);
        }

        // GET: Designations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await designationRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await designationRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DesignationExists(string id)
        {
            var company = await designationRepository.GetById(id);
            return company != null;
        }
    }
}
