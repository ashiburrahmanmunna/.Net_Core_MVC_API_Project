using Microsoft.AspNetCore.Mvc;
using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesRepository companyRepository;
        public CompaniesController(ICompaniesRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        // GET: Companies
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var CompanyList = await companyRepository.GetAllAsync();
                return Ok(CompanyList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        // GET: Companies/Details/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try 
            {
                var singleCompany = await companyRepository.Single(x => x.ComId == id);
                if (singleCompany == null)
                {
                    return NotFound();
                }
                return Ok(singleCompany);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company) 
        {
            try 
            {
                await companyRepository.Create(company);
                return Ok("Save Successfully");
            }
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
           
        }
        [HttpPut("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,Company company)
        {
           try
            {
                if (id != company.ComId)
                {
                    return BadRequest("ID Mismatch");
                }

                var updatedCompany = await companyRepository.Update(company, id);
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
                await companyRepository.Delete(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

    }
}
