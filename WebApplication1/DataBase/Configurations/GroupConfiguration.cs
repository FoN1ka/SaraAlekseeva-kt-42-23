using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TableName);

            // Первичный ключ
            builder.HasKey(p => p.GroupId)
                .HasName($"pk_{TableName}_group_id");

            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd()
                .HasColumnName("group_id")
                .HasComment("Идентификатор записи группы");

            // Название группы
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_group_name")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .HasComment("Название группы");

            // Курс
            builder.Property(p => p.Course)
                .IsRequired()
                .HasColumnName("c_group_course")
                .HasComment("Курс обучения");

            // Внешний ключ
            builder.Property(p => p.SpecialnostId)
                .HasColumnName("specialnost_id")
                .HasComment("Идентификатор специальности");

            // Связь с специальностью
            builder.HasOne(p => p.Specialnost)
                .WithMany(s => s.Groups)
                .HasForeignKey(p => p.SpecialnostId)
                .HasConstraintName($"fk_{TableName}_specialnost_id")
                .OnDelete(DeleteBehavior.Cascade);

            // Индекс по внешнему ключу
            builder.HasIndex(p => p.SpecialnostId, $"idk_{TableName}_fk_specialnost_id");

            // Автоподгрузка навигации
            builder.Navigation(p => p.Specialnost)
                .AutoInclude();
        }
    }
}