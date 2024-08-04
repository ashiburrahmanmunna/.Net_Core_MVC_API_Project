using Microsoft.EntityFrameworkCore;

using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Data.Implementation
{
    public class ShiftsRepository : Repository<Shift>, IShiftsRepository
    {
        public ShiftsRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await db.Set<Company>().ToListAsync();
        }
        public async Task<IEnumerable<Shift>> GetAllAsync(string comId)
        {
            return await db.Shifts
                .Include(d => d.Company)
                .Where(d => d.ComId == comId)
                .ToListAsync();
        }
    }
}
