using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project.Migrations
{
    /// <inheritdoc />
    public partial class changeDTMONTHdatatypeNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries",
                columns: new[] { "ComId", "EmpId", "dtYear", "dtMonth" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries",
                columns: new[] { "ComId", "EmpId", "dtYear" });
        }
    }
}
