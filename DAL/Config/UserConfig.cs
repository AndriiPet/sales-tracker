
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class UserConfig : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(u => u.IsRegistered)
                .HasColumnType("bool");

            builder.Property(u => u.IsManager)
                .HasColumnType("bool");

            builder.Property(u => u.Email)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(u => u.ProfilePicture)
                .HasColumnType("nvarchar(255)");

            builder.Property(u => u.LastLatitude)
                .HasColumnType("float");

            builder.Property(u => u.LastLongitude)
                .HasColumnType("float");

            builder.Property(u => u.IPN)
                .HasColumnType("int");

            builder.Property(u => u.Password)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(u => u.WorkRegionId)
                .HasColumnType("int");

            builder.Property(u => u.RoleId)
                .HasColumnType("int");

            builder.Property(u => u.ManagerId)
                .HasColumnType("int")
                .IsRequired(false);

            builder.Property(u => u.CreatedAt)
                .HasColumnType("datetime2");

            builder.Property(u => u.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasOne(u => u.WorkRegion)
               .WithMany()
               .HasForeignKey(u => u.WorkRegionId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Manager)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<Trip>()
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<TradingPoint>()
               .WithOne(tp => tp.User)
               .HasForeignKey(tp => tp.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.IPN).IsUnique();
        }
    }
}
