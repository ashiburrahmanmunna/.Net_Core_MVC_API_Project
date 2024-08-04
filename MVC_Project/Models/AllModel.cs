using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MVC_Project.Models
{

    public class Company
    {
        [Key]
        [StringLength(40)]
        public string ComId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(50)]
        public string ComName { get; set; }
        public int Basic { get; set; }
        public int Hrent { get; set; }
        public int Medical { get; set; }
        public bool IsInactive { get; set; }
    }

    public class Department
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        [Key]
        public string DeptId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(30)]
        public string DeptName { get; set; }
    }

    public class Designation
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        [Key]
        public string DesigId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(30)]
        public string DesigName { get; set; }
    }

    public class Shift
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        [Key]
        public string ShiftId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(50)]
        public string ShiftName { get; set; }
        public DateTime ShiftIn { get; set; }
        public DateTime ShiftOut { get; set; }
        public DateTime ShiftLate { get; set; }
    }

    //[PrimaryKey(nameof(ComId))]
    public class Employee
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        [Key]
        public string EmpId { get; set; } = Guid.NewGuid().ToString();
        [StringLength(15)]
        public string EmpCode { get; set; }
        [StringLength(100)]
        public string EmpName { get; set; }
        [StringLength(40)]
        public string ShiftId { get; set; }
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }
        [StringLength(40)]
        public string DeptId { get; set; }
        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }
        [StringLength(40)]
        public string DesigId { get; set; }
        [ForeignKey("DesigId")]
        public virtual Designation Designation { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        public decimal Gross { get; set; }
        public decimal? Basic { get; set; }
        public decimal? HRent { get; set; }
        public decimal? Medical { get; set; }
        public decimal? Others { get; set; }
        public DateTime dtJoin { get; set; }
    }

    public class Attendance
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        public string EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }

        public DateTime dtDate { get; set; }
        [StringLength(1)]
        public string? AttStatus { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
    }

    public class AttendanceSummary
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        public string EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }
        [StringLength(4)]

        public string dtYear { get; set; }
        [StringLength(3)]

        public string dtMonth { get; set; }
        public int MonthDays { get; set; }
        public int Present { get; set; }
        public int Late { get; set; }
        public int Absent { get; set; }
        public int Holiday { get; set; }
    }

    public class Salary
    {
        [StringLength(40)]
        public string ComId { get; set; }
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
        [StringLength(40)]
        public string EmpId { get; set; }
        [ForeignKey("EmpId")]
        public virtual Employee Employee { get; set; }

        [StringLength(3)]
        public string dtMonth { get; set; }
        [StringLength(4)]
        public string dtYear { get; set; }
        public decimal Gross { get; set; }
        public decimal Basic { get; set; }
        public decimal HRent { get; set; }
        public decimal Medical { get; set; }
        public decimal Others { get; set; }
        public decimal AbsentAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool IsPaid { get; set; }
        public decimal PaidAmount { get; set; }

    }

}
