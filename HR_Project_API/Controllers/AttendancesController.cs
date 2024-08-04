using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HR_Project_API.Data;
using HR_Project_API.Models;
using HR_Project_API.Data.Interface;

namespace HR_Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendancesRepository attendancesRepository;
        public AttendancesController(IAttendancesRepository attendancesRepository)
        {
            this.attendancesRepository = attendancesRepository;
        }

        // GET: Attendances
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var AttendanceList = await attendancesRepository.GetAllAsync();
                return Ok(AttendanceList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        // GET: Attendances/Details/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var singleAttendance = await attendancesRepository.Single(x => x.ComId == id);
                if (singleAttendance == null)
                {
                    return NotFound();
                }
                return Ok(singleAttendance);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: Attendances/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance model)
        {
            try
            {
                await attendancesRepository.Create(model);
                return Ok("Save Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Attendance model)
        {
            try
            {
                if (id != model.ComId)
                {
                    return BadRequest("ID Mismatch");
                }

                var updatedAttendance = await attendancesRepository.Update(model, id);
                if (updatedAttendance == null)
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

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await attendancesRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
    }
}
