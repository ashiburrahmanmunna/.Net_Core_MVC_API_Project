using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class SalariesRepository : Repository<Department>, ISalariesRepository
    {
        public SalariesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
