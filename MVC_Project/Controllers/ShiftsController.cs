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
    public class ShiftsController : Controller
    {
        private readonly IShiftsRepository shiftsRepository;

        public ShiftsController(IShiftsRepository shiftsRepository)
        {
            this.shiftsRepository = shiftsRepository;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            var comId = Request.Cookies["ComId"];
            if (string.IsNullOrEmpty(comId))
            {
                return Problem("ComId cookie is missing or invalid.");
            }

            var shifts = await shiftsRepository.GetAllAsync(comId);

            return View(shifts);
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || shiftsRepository == null)
            {
                return NotFound();
            }

            var department = await shiftsRepository.Single(x => x.ShiftId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Shifts/Create
        public async Task<IActionResult> Create(string Id)
        {
            var companies = await shiftsRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (string.IsNullOrWhiteSpace(Id))
            {
                return View(new Shift()
                {
                    ComId = string.Empty
                });
            }
            else
            {
                var singleDepartment = await shiftsRepository.Single(x => x.ComId == Id);
                if (singleDepartment == null)
                    return NotFound();
                return View(singleDepartment);
            }

        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,ShiftName,ShiftIn,ShiftOut,ShiftLate")] Shift shift)
        {
                await shiftsRepository.Create(shift);
                return RedirectToAction(nameof(Index));
            }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var companies = await shiftsRepository.GetCompanies();
            ViewData["ComId"] = new SelectList(companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(companies, "ComName", "ComName");
            if (id == null)
            {
                return NotFound();
            }

            var company = await shiftsRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,ShiftId,ShiftName,ShiftIn,ShiftOut,ShiftLate")] Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var updatedCompany = await shiftsRepository.Update(shift, id);
            if (updatedCompany == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
            //}
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await shiftsRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await shiftsRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ShiftExists(string id)
        {
            var company = await shiftsRepository.GetById(id);
            return company != null;
        }
    }
}
