using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataBase.Helpers;
using WebApplication1.Models;

namespace WebApplication1.DataBase.Configuratios
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //Primary key
            builder
                .HasKey(p => p.GroupId)
                .HasName($"pk{TableName}_group_id");

            //For int primary key set autogeneration (k ever new note will add +1)
            builder.Property(p => p.GroupId)
                .ValueGeneratedOnAdd();

            builder.ToTable(TableName);
        }
    }
}

