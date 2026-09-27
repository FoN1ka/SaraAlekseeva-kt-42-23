using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class EditStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_discipline_cd_student_StudentId",
                table: "cd_discipline");

            migrationBuilder.DropIndex(
                name: "IX_cd_discipline_StudentId",
                table: "cd_discipline");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "cd_discipline");

            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "cd_grade",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cd_grade_StudentId1",
                table: "cd_grade",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_grade_cd_student_StudentId1",
                table: "cd_grade",
                column: "StudentId1",
                principalTable: "cd_student",
                principalColumn: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cd_grade_cd_student_StudentId1",
                table: "cd_grade");

            migrationBuilder.DropIndex(
                name: "IX_cd_grade_StudentId1",
                table: "cd_grade");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "cd_grade");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "cd_discipline",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cd_discipline_StudentId",
                table: "cd_discipline",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_cd_discipline_cd_student_StudentId",
                table: "cd_discipline",
                column: "StudentId",
                principalTable: "cd_student",
                principalColumn: "student_id");
        }
    }
}
