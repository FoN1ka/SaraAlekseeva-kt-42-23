using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataBase.Helpers;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configurations
{
    public class StudentConfiguration: IEntityTypeConfiguration<Student>
    {
        private const string TableName = "cd_student";
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            //Primary key
            builder
                .HasKey(p => p.StudentId)
                .HasName($"pk_{TableName}_student_id");

            //Автогенерация для первичного ключа
            builder.Property(p => p.StudentId)
                .ValueGeneratedOnAdd();

            //Расписсываем как будут называть колонки в бд, а также их обязательность и тд
            builder.Property(p => p.StudentId)
                .HasColumnName("student_id")
                .HasComment("Идентификатор записи студента");

            //HasComment добавит комментарий
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя студента");

            builder.Property(p => p.LastName)
               .IsRequired()
               .HasColumnName("c_student_lastname")
               .HasColumnType(ColumnType.String).HasMaxLength(100)
               .HasComment("Фамилия студента");

            //Расписсываем как будут называть колонки в бд, а также их обязательность и тд
            builder.Property(p => p.GroupId)
                .HasColumnName("f_group_id")
                .HasComment("Идентификатор группы");

            builder.ToTable(TableName)
                .HasOne(p => p.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(p => p.GroupId)
                .HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Cascade);

            // Индекс по внешнему ключу
            builder.ToTable(TableName).HasIndex(p => p.GroupId, $"idk_{TableName}_fk_group_id");

            builder.Navigation(p => p.Group)
                .AutoInclude();
        }
    }
}
