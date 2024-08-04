using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using HR_Project_API.Data.Interface;
using HR_Project_API.Models;

namespace HR_Project_API.Data.Implementation
{
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task CreateEmployee(string comId, string empId, string empCode, string empName, string shiftId, string deptId, string desigId, string gender, decimal gross, string dtJoin)
        {
            var parameters = new[]
            {
        new SqlParameter("@ComId", comId),
        new SqlParameter("@EmpId", empId),
        new SqlParameter("@EmpCode", empCode),
        new SqlParameter("@EmpName", empName),
        new SqlParameter("@ShiftId", shiftId),
        new SqlParameter("@DeptId", deptId),
        new SqlParameter("@DesigId", desigId),
        new SqlParameter("@Gender", gender),
        new SqlParameter("@Gross", gross),
        new SqlParameter("@dtJoin", dtJoin)
    };

            await db.Database.ExecuteSqlRawAsync("EXEC data_entry_in_employee @ComId, @EmpId, @EmpCode, @EmpName, @ShiftId, @DeptId, @DesigId, @Gender, @Gross, @dtJoin", parameters);
        }

        public async Task<IEnumerable<SelectListItem>> GetCompanies()
        {
            var companies = await db.Set<Company>().ToListAsync();
            return companies.Select(c => new SelectListItem
            {
                Value = c.ComId.ToString(),
                Text = c.ComName
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetDepartments()
        {
            var departments = await db.Set<Department>().ToListAsync();
            return departments.Select(d => new SelectListItem
            {
                Value = d.DeptId.ToString(),
                Text = d.DeptName
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetDesignations()
        {
            var designations = await db.Set<Designation>().ToListAsync();
            return designations.Select(d => new SelectListItem
            {
                Value = d.DesigId.ToString(),
                Text = d.DesigName
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetShifts()
        {
            var shifts = await db.Set<Shift>().ToListAsync();
            return shifts.Select(s => new SelectListItem
            {
                Value = s.ShiftId.ToString(),
                Text = s.ShiftName
            });
        }

    }
}
