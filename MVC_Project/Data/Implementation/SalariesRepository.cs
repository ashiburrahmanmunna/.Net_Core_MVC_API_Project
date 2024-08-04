using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Data.Implementation
{
    public class SalariesRepository : Repository<Department>, ISalariesRepository
    {
        public SalariesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
