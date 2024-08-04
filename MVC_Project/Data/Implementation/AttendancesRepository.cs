using Microsoft.EntityFrameworkCore;

using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Data.Implementation
{
    public class AttendancesRepository : Repository<Department>, IAttendancesRepository
    {
        public AttendancesRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await db.Set<Company>().ToListAsync();
        }
        public async Task<IEnumerable<Attendance>> GetAllAsync(string comId)
        {
            return await db.Attendances
                .Include(d => d.Company)
                .Where(d => d.ComId == comId)
                .ToListAsync();
        }
    }
}
