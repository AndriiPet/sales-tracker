
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class TradingPointConfig : IEntityTypeConfiguration<TradingPoint>
    {

        public void Configure(EntityTypeBuilder<TradingPoint> builder)
        {
            builder.ToTable("TradingPoint");

            builder.Property(tp => tp.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(tp => tp.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(tp => tp.Address)
               .HasColumnType("nvarchar(255)")
               .IsRequired();

            builder.Property(tp => tp.Longitude)
               .HasColumnType("float");

            builder.Property(tp => tp.Latitude)
                .HasColumnType("float");

            builder.Property(tp => tp.PhoneNumber)
               .HasColumnType("nvarchar(20)");

            builder.Property(tp => tp.CustomerId)
                .HasColumnType("int");

            builder.Property(tp => tp.UserId)
                .HasColumnType("int");

            builder.Property(tp => tp.WorkRegionId)
                .HasColumnType("int");

            builder.Property(tp => tp.CreatedAt)
               .HasColumnType("datetime2");

            builder.Property(tp => tp.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasOne(tp => tp.Customer)
               .WithMany(c => c.TradingPoints)
               .HasForeignKey(tp => tp.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tp => tp.User)
                .WithMany()
                .HasForeignKey(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tp => tp.WorkRegion)
                .WithMany()
                .HasForeignKey(tp => tp.WorkRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(tp => tp.CustomerId);
            builder.HasIndex(tp => tp.UserId);
            builder.HasIndex(tp => tp.WorkRegionId);

        }
    }
}
