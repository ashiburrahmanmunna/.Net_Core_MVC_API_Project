using HR_Project_API.Models;

namespace HR_Project_API.Data.Interface
{
    public interface IDepartmentsRepository:IRepository<Department>
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Department>> GetAllAsync(string comId);
    }
}
