using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Data.Implementation
{
    public class CompaniesRepository : Repository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
