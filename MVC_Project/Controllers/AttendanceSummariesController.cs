using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class AttendanceSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceSummariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceSummaries
        public async Task<IActionResult> Index()
        {
            var comId = Request.Cookies["ComId"];
            if (string.IsNullOrEmpty(comId))
            {
                return Problem("ComId cookie is missing or invalid.");
            }
            var applicationDbContext = _context.AttendanceSummaries.Include(a => a.Company).Include(a => a.Employee).Where(a=>a.ComId==comId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttendanceSummaries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AttendanceSummaries == null)
            {
                return NotFound();
            }

            var attendanceSummary = await _context.AttendanceSummaries
                .Include(a => a.Company)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (attendanceSummary == null)
            {
                return NotFound();
            }

            return View(attendanceSummary);
        }

        // GET: AttendanceSummaries/Create
        public IActionResult Create()
        {
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(_context.Companies, "ComName", "ComName");
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            return View();
        }

        // POST: AttendanceSummaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ComId,EmpId,dtYear,dtMonth,MonthDays,Present,Late,Absent,Holiday")] AttendanceSummary attendanceSummary)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(attendanceSummary);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendanceSummary.ComId);
        //    //ViewData["ComName"] = new SelectList(_context.Companies, "ComName", "ComName", attendanceSummary.Company.ComName);
        //    ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendanceSummary.EmpId);
        //    return View(attendanceSummary);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceSummary attendanceSummary)
        {
            //if (ModelState.IsValid)
            //{
                var comId = attendanceSummary.ComId;
                var dtMonth = attendanceSummary.dtMonth;
                var dtYear = attendanceSummary.dtYear;

                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC InsertIntoTempAndCalculateCounts {comId}, {dtMonth}, {dtYear}");

                return RedirectToAction(nameof(Index));
            //}

            // Populate the select lists and return the view
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendanceSummary.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendanceSummary.EmpId);
            return View(attendanceSummary);
        }

        // GET: AttendanceSummaries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AttendanceSummaries == null)
            {
                return NotFound();
            }

            var attendanceSummary = await _context.AttendanceSummaries.FindAsync(id);
            if (attendanceSummary == null)
            {
                return NotFound();
            }
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendanceSummary.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendanceSummary.EmpId);
            return View(attendanceSummary);
        }

        // POST: AttendanceSummaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,EmpId,dtYear,dtMonth,MonthDays,Present,Late,Absent,Holiday")] AttendanceSummary attendanceSummary)
        {
            if (id != attendanceSummary.ComId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceSummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceSummaryExists(attendanceSummary.ComId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendanceSummary.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendanceSummary.EmpId);
            return View(attendanceSummary);
        }

        // GET: AttendanceSummaries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AttendanceSummaries == null)
            {
                return NotFound();
            }

            var attendanceSummary = await _context.AttendanceSummaries
                .Include(a => a.Company)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (attendanceSummary == null)
            {
                return NotFound();
            }

            return View(attendanceSummary);
        }

        // POST: AttendanceSummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AttendanceSummaries == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttendanceSummaries'  is null.");
            }
            var attendanceSummary = await _context.AttendanceSummaries.FindAsync(id);
            if (attendanceSummary != null)
            {
                _context.AttendanceSummaries.Remove(attendanceSummary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceSummaryExists(string id)
        {
          return (_context.AttendanceSummaries?.Any(e => e.ComId == id)).GetValueOrDefault();
        }
    }
}
