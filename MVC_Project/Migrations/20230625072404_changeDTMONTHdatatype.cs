using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project.Migrations
{
    /// <inheritdoc />
    public partial class changeDTMONTHdatatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries");

            migrationBuilder.AlterColumn<string>(
                name: "dtMonth",
                table: "AttendanceSummaries",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries",
                columns: new[] { "ComId", "EmpId", "dtYear" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dtMonth",
                table: "AttendanceSummaries",
                type: "datetime2",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendanceSummaries",
                table: "AttendanceSummaries",
                columns: new[] { "ComId", "EmpId", "dtYear", "dtMonth" });
        }
    }
}
