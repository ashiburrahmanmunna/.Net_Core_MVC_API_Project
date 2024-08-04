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
    public class CompaniesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly ICompaniesRepository companyRepository;

        //public CompaniesController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public CompaniesController(ICompaniesRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var CompanyList = await companyRepository.GetAllAsync();
            return View(CompanyList);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var singleCompany = await companyRepository.Single(x => x.ComId == id);
            if(singleCompany == null)
            {
                return NotFound();
            }
            return View(singleCompany);
        }

        // GET: Companies/Create
        public async Task<IActionResult> Create(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return View(new Company()
                {
                    ComId = string.Empty
                });
            }
            else
            {
                var singleCompany = await companyRepository.Single(x => x.ComId == Id);
                if (singleCompany == null)
                    return NotFound();
                return View(singleCompany);
            }
            //return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComName,Basic,Hrent,Medical,IsInactive")] Company company)  //
        {
            if (ModelState.IsValid)
            {
                await companyRepository.Create(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ComId,ComName,Basic,Hrent,Medical,IsInactive")] Company company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(company);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(company);
        //}

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await companyRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }




        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.Companies == null)
        //    {
        //        return NotFound();
        //    }

        //    var company = await _context.Companies.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(company);
        //}

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,ComName,Basic,Hrent,Medical,IsInactive")] Company company)
        {
            if (id != company.ComId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedCompany = await companyRepository.Update(company, id);
                if (updatedCompany == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("ComId,ComName,Basic,Hrent,Medical,IsInactive")] Company company)
        //{
        //    if (id != company.ComId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(company);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CompanyExists(company.ComId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(company);
        //}

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await companyRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //if (companyRepository.DbSetIsNull())
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Companies' is null.");
            //}

            await companyRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }


        //private Task<bool> CompanyExists(string id)
        //{
        //  return (_context.Companies?.Any(e => e.ComId == id)).GetValueOrDefault();
        //}
        private async Task<bool> CompanyExists(string id)
        {
            var company = await companyRepository.GetById(id);
            return company != null;
        }

    }
}
