using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class ShiftsRepository : Repository<Shift>, IShiftsRepository
    {
        public ShiftsRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
