using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class AttendanceSummariesRepository : Repository<Department>, IAttendanceSummariesRepository
    {
        public AttendanceSummariesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
