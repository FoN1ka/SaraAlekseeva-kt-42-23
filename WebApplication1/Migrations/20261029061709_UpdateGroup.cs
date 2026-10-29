using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "c_group_is_deleted",
                table: "cd_group",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак удаления Группы");

            migrationBuilder.AlterColumn<string>(
                name: "c_discipline_name",
                table: "cd_discipline",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                comment: "направление дисциплины",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldComment: "название дисциплины");

            migrationBuilder.AddColumn<string>(
                name: "c_discipline_direction",
                table: "cd_discipline",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "название дисциплины");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_group_is_deleted",
                table: "cd_group");

            migrationBuilder.DropColumn(
                name: "c_discipline_direction",
                table: "cd_discipline");

            migrationBuilder.AlterColumn<string>(
                name: "c_discipline_name",
                table: "cd_discipline",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                comment: "название дисциплины",
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100,
                oldComment: "направление дисциплины");
        }
    }
}
