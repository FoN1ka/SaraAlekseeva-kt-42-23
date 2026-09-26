using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataBase.Helpers;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configurations
{
    public class SpecialnostConfiguration : IEntityTypeConfiguration<Specialnost>
    {
        private const string TableName = "cd_specialnost";

        public void Configure(EntityTypeBuilder<Specialnost> builder)
        {
            // Имя таблицы
            builder.ToTable(TableName);

            // Первичный ключ
            builder.HasKey(p => p.SpecialnostId)
                .HasName($"pk_{TableName}_specialnost_id");

            builder.Property(p => p.SpecialnostId)
                .ValueGeneratedOnAdd()
                .HasColumnName("specialnost_id")
                .HasComment("Идентификатор записи специальности");

            // Название специальности
            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("c_specialnost_title")
                .HasColumnType(ColumnType.String)
                .HasMaxLength(200)
                .HasComment("Название специальности");

            // Код специальности
            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnName("c_specialnost_code")
                .HasComment("Код специальности");
        }
    }
}