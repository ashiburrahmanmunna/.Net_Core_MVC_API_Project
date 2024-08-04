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
    public class DesignationsController : ControllerBase
    {
        private readonly IDesignationsRepository designationsRepository;

        public DesignationsController(IDesignationsRepository designationsRepository)
        {
            this.designationsRepository = designationsRepository;
        }

        // GET: Designations
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var designationList = await designationsRepository.GetAllAsync();
                return Ok(designationList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var singleDesignation = await designationsRepository.Single(x => x.DesigId == id);
                if (singleDesignation == null)
                {
                    return NotFound();
                }
                return Ok(singleDesignation);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Designation model)
        {
            try
            {
                await designationsRepository.Create(model);
                return Ok("Save Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Designation model)
        {
            try
            {
                if (id != model.DesigId)
                {
                    return BadRequest("ID Mismatch");
                }

                var updatedCompany = await designationsRepository.Update(model, id);
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

        // POST: Companies/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await designationsRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        
    }
}
