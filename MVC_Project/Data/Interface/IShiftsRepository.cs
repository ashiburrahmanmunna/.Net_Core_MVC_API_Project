using MVC_Project.Models;

namespace MVC_Project.Data.Interface
{
    public interface IShiftsRepository:IRepository<Shift>
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Shift>> GetAllAsync(string comId);
    }
}
