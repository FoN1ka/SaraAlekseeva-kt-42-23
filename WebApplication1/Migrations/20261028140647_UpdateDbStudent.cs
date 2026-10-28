using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "c_student_is_deleted",
                table: "cd_student",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак удаления Студента");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_student_is_deleted",
                table: "cd_student");
        }
    }
}
