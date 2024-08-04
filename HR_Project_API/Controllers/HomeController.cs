using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using HR_Project_API.Data;
using HR_Project_API.Models;

using System.Diagnostics;

namespace HR_Project_API.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly ApplicationDbContext db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            //var companyList = await db.Companies.ToListAsync();
            //ViewBag.Companies = new SelectList(db.Companies, "ComId", "ComName");
            return Ok();

        }


        public IActionResult SetCompany(string ComId)
        {
            Response.Cookies.Append("ComId", ComId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}