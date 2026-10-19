using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataBase.Helpers;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            //Primary key
            builder
                .HasKey(p => p.GradeId)
                .HasName($"pk_{TableName}_grade_id");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd();

            //Расписсываем как будут называть колонки в бд, а также их обязательность и тд
            builder.Property(p => p.GradeId)
                .HasColumnName("grade_id")
                .HasComment("Идентификатор записи оценки");

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("c_grade_value");

            builder.Property(p => p.StudentId)
                .HasColumnName("f_student_id")
                .HasComment("Идентификатор студента");

            builder.Property(p => p.DisciplineId)
                .HasColumnName("f_discipline_id")
                .HasComment("Идентификатор дисциплины");

            //HasComment 
            builder.HasOne(p => p.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Discipline)
                .WithMany(d => d.Grades)
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);

            // Индекс по внешнему ключу
            builder.ToTable(TableName).HasIndex(p => p.StudentId, $"idk_{TableName}_fk_student_id");

            // Автоподгрузка навигации
            builder.Navigation(p => p.Student)
                .AutoInclude();

            // Индекс по внешнему ключу
            builder.ToTable(TableName).HasIndex(p => p.DisciplineId, $"idk_{TableName}_fk_discipline_id");

            // Автоподгрузка навигации
            builder.Navigation(p => p.Discipline)
                .AutoInclude();
        }
    }
}
