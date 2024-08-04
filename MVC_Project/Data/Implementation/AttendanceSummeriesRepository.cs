using MVC_Project.Data.Interface;
using MVC_Project.Models;

namespace MVC_Project.Data.Implementation
{
    public class AttendanceSummariesRepository : Repository<Department>, IAttendanceSummariesRepository
    {
        public AttendanceSummariesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
