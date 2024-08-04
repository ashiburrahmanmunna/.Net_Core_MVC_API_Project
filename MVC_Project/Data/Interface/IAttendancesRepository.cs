using MVC_Project.Models;

namespace MVC_Project.Data.Interface
{
    public interface IAttendancesRepository:IRepository<Department>
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<IEnumerable<Attendance>> GetAllAsync(string comId);
    }
}
