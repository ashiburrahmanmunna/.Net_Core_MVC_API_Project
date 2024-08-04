using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Project_API.Migrations
{
    /// <inheritdoc />
    public partial class addModelsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Companies_ComId",
                table: "Designations");

            migrationBuilder.AlterColumn<string>(
                name: "ComId",
                table: "Designations",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Companies_ComId",
                table: "Designations",
                column: "ComId",
                principalTable: "Companies",
                principalColumn: "ComId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Companies_ComId",
                table: "Designations");

            migrationBuilder.AlterColumn<string>(
                name: "ComId",
                table: "Designations",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Companies_ComId",
                table: "Designations",
                column: "ComId",
                principalTable: "Companies",
                principalColumn: "ComId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
