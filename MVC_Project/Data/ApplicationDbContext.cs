using Microsoft.EntityFrameworkCore;

using MVC_Project.Models;

namespace MVC_Project.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //modelBuilder.Entity<Department>()
                //    .HasKey(a => new { a.ComId, a.DeptId });

                //modelBuilder.Entity<Department>()
                //    .HasOne(a => a.Company)
                //    .WithMany()
                //    .HasForeignKey(a => a.ComId)
                //    .OnDelete(DeleteBehavior.NoAction);

                //modelBuilder.Entity<Designation>()
                //    .HasKey(a => new { a.ComId, a.DesigId });

                //modelBuilder.Entity<Designation>()
                //    .HasOne(a => a.Company)
                //    .WithMany()
                //    .HasForeignKey(a => a.ComId)
                //    .OnDelete(DeleteBehavior.NoAction);

                //modelBuilder.Entity<Employee>()
                //    .HasKey(a => new { a.ComId, a.EmpId });

                //modelBuilder.Entity<Employee>()
                //    .HasOne(a => a.Company)
                //    .WithMany()
                //    .HasForeignKey(a => a.ComId)
                //    .OnDelete(DeleteBehavior.NoAction);

                //modelBuilder.Entity<Employee>()
                //.HasOne(a => a.Designation)
                //.WithMany()
                //.HasForeignKey(a => a.DesigId)
                //.OnDelete(DeleteBehavior.NoAction);

                //modelBuilder.Entity<Employee>()
                //    .HasOne(a => a.Department)
                //    .WithMany()
                //    .HasForeignKey(a => a.DeptId)
                //    .OnDelete(DeleteBehavior.NoAction);



                modelBuilder.Entity<Attendance>()
                    .HasKey(a => new { a.ComId, a.EmpId, a.dtDate });

                modelBuilder.Entity<Attendance>()
                    .HasOne(a => a.Company)
                    .WithMany()
                    .HasForeignKey(a => a.ComId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Attendance>()
                    .HasOne(a => a.Employee)
                    .WithMany()
                    .HasForeignKey(a => a.EmpId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<AttendanceSummary>()
                    .HasKey(a => new { a.ComId, a.EmpId, a.dtYear, a.dtMonth }); //, a.dtMonth 

            modelBuilder.Entity<AttendanceSummary>()
                    .HasOne(a => a.Company)
                    .WithMany()
                    .HasForeignKey(a => a.ComId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<AttendanceSummary>()
                    .HasOne(a => a.Employee)
                    .WithMany()
                    .HasForeignKey(a => a.EmpId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Salary>()
                   .HasKey(a => new { a.ComId, a.EmpId, a.dtYear, a.dtMonth });

                modelBuilder.Entity<Salary>()
                    .HasOne(a => a.Company)
                    .WithMany()
                    .HasForeignKey(a => a.ComId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Salary>()
                    .HasOne(a => a.Employee)
                    .WithMany()
                .HasForeignKey(a => a.EmpId)
                    .OnDelete(DeleteBehavior.NoAction);
            }

            public DbSet<Company> Companies { get; set; }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Designation> Designations { get; set; }
            public DbSet<Shift> Shifts { get; set; }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<Attendance> Attendances { get; set; }
            public DbSet<AttendanceSummary> AttendanceSummaries { get; set; }
            public DbSet<Salary> Salaries { get; set; }
        }
    }

