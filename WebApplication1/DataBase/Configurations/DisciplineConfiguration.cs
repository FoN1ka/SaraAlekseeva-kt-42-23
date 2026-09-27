using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataBase.Helpers;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            //Primary key
            builder
                .HasKey(p => p.DisciplineId)
                .HasName($"pk_{TableName}_discipline_id");

            //For int primary key set autogeneration (k ever new note will add +1)
            builder.Property(p => p.DisciplineId)
                .ValueGeneratedOnAdd();

            //Расписсываем как будут называть колонки в бд, а также их обязательность и тд
            builder.Property(p => p.DisciplineId)
                .HasColumnName("discipline_id")
                .HasComment("Идентификатор записи Дисциплины");

            builder.ToTable(TableName);

            //HasComment will add comment, which seeing in SUBD
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("c_discipline_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("название дисциплины");

            // Флаг удаления
            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName("c_discipline_is_deleted")
                .HasDefaultValue(false)
                .HasComment("Признак удаления дисциплины");

        }
    }
}
