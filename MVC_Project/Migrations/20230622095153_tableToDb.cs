using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Project.Migrations
{
    public partial class tableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ComName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Basic = table.Column<int>(type: "int", nullable: false),
                    Hrent = table.Column<int>(type: "int", nullable: false),
                    Medical = table.Column<int>(type: "int", nullable: false),
                    IsInactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ComId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeptName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesigId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DesigName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesigId);
                    table.ForeignKey(
                        name: "FK_Designations_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ShiftName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShiftIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftLate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmpCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeptId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DesigId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HRent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    dtJoin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesigId",
                        column: x => x.DesigId,
                        principalTable: "Designations",
                        principalColumn: "DesigId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    dtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    InTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OutTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.ComId, x.EmpId, x.dtDate });
                    table.ForeignKey(
                        name: "FK_Attendances_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSummaries",
                columns: table => new
                {
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    dtYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    dtMonth = table.Column<DateTime>(type: "datetime2", maxLength: 3, nullable: false),
                    MonthDays = table.Column<int>(type: "int", nullable: false),
                    Present = table.Column<int>(type: "int", nullable: false),
                    Late = table.Column<int>(type: "int", nullable: false),
                    Absent = table.Column<int>(type: "int", nullable: false),
                    Holiday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummaries", x => new { x.ComId, x.EmpId, x.dtYear, x.dtMonth });
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId");
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    ComId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    dtMonth = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    dtYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AbsentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => new { x.ComId, x.EmpId, x.dtYear, x.dtMonth });
                    table.ForeignKey(
                        name: "FK_Salaries_Companies_ComId",
                        column: x => x.ComId,
                        principalTable: "Companies",
                        principalColumn: "ComId");
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmpId",
                table: "Attendances",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummaries_EmpId",
                table: "AttendanceSummaries",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ComId",
                table: "Departments",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ComId",
                table: "Designations",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ComId",
                table: "Employees",
                column: "ComId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptId",
                table: "Employees",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesigId",
                table: "Employees",
                column: "DesigId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftId",
                table: "Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmpId",
                table: "Salaries",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ComId",
                table: "Shifts",
                column: "ComId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "AttendanceSummaries");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
