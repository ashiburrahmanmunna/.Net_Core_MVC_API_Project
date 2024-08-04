using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class DesignationsRepository : Repository<Designation>, IDesignationsRepository
    {
        public DesignationsRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
