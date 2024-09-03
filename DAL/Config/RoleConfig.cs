
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasMany(x => x.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => r.Name)
                .IsUnique();

        }
    }
}
