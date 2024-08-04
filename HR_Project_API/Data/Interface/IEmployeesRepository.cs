using Microsoft.AspNetCore.Mvc.Rendering;

using HR_Project_API.Models;

namespace HR_Project_API.Data.Interface
{
    public interface IEmployeesRepository:IRepository<Employee>
    {
        //Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<SelectListItem>> GetCompanies();
        Task<IEnumerable<SelectListItem>> GetDepartments();
        Task<IEnumerable<SelectListItem>> GetDesignations();
        Task<IEnumerable<SelectListItem>> GetShifts();

        Task CreateEmployee(string comId, string empId, string empCode, string empName, string shiftId, string deptId, string desigId, string gender, decimal gross, string dtJoin);
        //Task Update(Employee model, string id);
    }
}
