using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class EditDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Specialnosts",
                newName: "cd_specialnost");

            migrationBuilder.RenameTable(
                name: "Disciplines",
                newName: "cd_discipline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "cd_specialnost",
                newName: "Specialnosts");

            migrationBuilder.RenameTable(
                name: "cd_discipline",
                newName: "Disciplines");
        }
    }
}
