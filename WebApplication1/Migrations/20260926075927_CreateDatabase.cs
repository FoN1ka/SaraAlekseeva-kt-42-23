using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_specialnost",
                columns: table => new
                {
                    specialnost_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи специальности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_specialnost_title = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название специальности"),
                    c_specialnost_code = table.Column<long>(type: "bigint", nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialnost_specialnost_id", x => x.specialnost_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_group_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название группы"),
                    c_group_course = table.Column<int>(type: "integer", nullable: false, comment: "Курс обучения"),
                    specialnost_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.group_id);
                    table.ForeignKey(
                        name: "fk_cd_group_specialnost_id",
                        column: x => x.specialnost_id,
                        principalTable: "cd_specialnost",
                        principalColumn: "specialnost_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_firstName = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastName = table.Column<string>(type: "text", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор группы"),
                    GroupId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.student_id);
                    table.ForeignKey(
                        name: "FK_cd_student_cd_group_GroupId1",
                        column: x => x.GroupId1,
                        principalTable: "cd_group",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.group_id,
                        principalTable: "cd_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи Дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "название дисциплины"),
                    c_discipline_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Признак удаления дисциплины"),
                    StudentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                    table.ForeignKey(
                        name: "FK_Discipline_cd_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "cd_student",
                        principalColumn: "student_id");
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи оценки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор студента"),
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины"),
                    DisciplineId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.grade_id);
                    table.ForeignKey(
                        name: "FK_cd_grade_Discipline_DisciplineId1",
                        column: x => x.DisciplineId1,
                        principalTable: "Discipline",
                        principalColumn: "discipline_id");
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.discipline_id,
                        principalTable: "Discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cd_grade_discipline_id",
                table: "cd_grade",
                column: "discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_cd_grade_DisciplineId1",
                table: "cd_grade",
                column: "DisciplineId1");

            migrationBuilder.CreateIndex(
                name: "IX_cd_grade_student_id",
                table: "cd_grade",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "idk_cd_group_fk_specialnost_id",
                table: "cd_group",
                column: "specialnost_id");

            migrationBuilder.CreateIndex(
                name: "idk_cd_student_fk_group_id",
                table: "cd_student",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_cd_student_GroupId1",
                table: "cd_student",
                column: "GroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_StudentId",
                table: "Discipline",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_specialnost");
        }
    }
}
