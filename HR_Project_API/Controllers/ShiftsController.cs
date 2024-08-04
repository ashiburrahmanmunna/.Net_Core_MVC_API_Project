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
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftsRepository shiftRepository;

        public ShiftsController(IShiftsRepository shiftRepository)
        {
            this.shiftRepository = shiftRepository;
        }

        // GET: Shifts
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var ShiftsList = await shiftRepository.GetAllAsync();
                return Ok(ShiftsList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        // GET: Shifts/Details/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var singleShift = await shiftRepository.Single(x => x.ShiftId == id);
                if (singleShift == null)
                {
                    return NotFound();
                }
                return Ok(singleShift);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shift model)
        {
            try
            {
                await shiftRepository.Create(model);
                return Ok("Save Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Shift model)
        {
            try
            {
                if (id != model.ComId)
                {
                    return BadRequest("ID Mismatch");
                }

                var singleShift = await shiftRepository.Update(model, id);
                if (singleShift == null)
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


        // POST: Companies/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await shiftRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

    }
}
