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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
             var comId = Request.Cookies["ComId"];
            if (string.IsNullOrEmpty(comId))
            {
                return Problem("ComId cookie is missing or invalid.");
            }
            var applicationDbContext = _context.Attendances.Include(a => a.Company).Include(a => a.Employee).Where(a => a.ComId == comId); ;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Company)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId");
            ViewData["ComName"] = new SelectList(_context.Companies, "ComName", "ComName");
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            ViewData["EmpName"] = new SelectList(_context.Employees, "EmpName", "EmpName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,EmpId,dtDate,AttStatus,InTime,OutTime")] Attendance attendance)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendance.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendance.EmpId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FirstOrDefaultAsync(a=>a.EmpId == id);  //FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendance.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendance.EmpId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComId,EmpId,AttStatus,dtDate,InTime,OutTime")] Attendance attendance)
        {
            if (id != attendance.EmpId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!AttendanceExists(attendance.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
        //}
        ViewData["ComId"] = new SelectList(_context.Companies, "ComId", "ComId", attendance.ComId);
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", attendance.EmpId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Company)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(string id)
        {
          return (_context.Attendances?.Any(e => e.ComId == id)).GetValueOrDefault();
        }
    }
}
