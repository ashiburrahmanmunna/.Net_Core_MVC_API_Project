using Microsoft.AspNetCore.Mvc.Rendering;

using MVC_Project.Models;

namespace MVC_Project.Data.Interface
{
    public interface IEmployeesRepository:IRepository<Employee>
    {
        //Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<SelectListItem>> GetCompanies();
        Task<IEnumerable<SelectListItem>> GetDepartments();
        Task<IEnumerable<SelectListItem>> GetDesignations();
        Task<IEnumerable<SelectListItem>> GetShifts();

        Task CreateEmployee(string comId, string empId, string empCode, string empName, string shiftId, string deptId, string desigId, string gender, decimal gross, string dtJoin);
    }
}
