using Microsoft.EntityFrameworkCore;

using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class DepartmentsRepository : Repository<Department>, IDepartmentsRepository
    {
        public DepartmentsRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await db.Set<Company>().ToListAsync();
        }
        public async Task<IEnumerable<Department>> GetAllAsync(string comId)
        {
            return await db.Departments
                .Include(d => d.Company)
                .Where(d => d.ComId == comId)
                .ToListAsync();
        }

    }
}
