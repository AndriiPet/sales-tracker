
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class WorkRegionConfig : IEntityTypeConfiguration<WorkRegion>
    {

        public void Configure(EntityTypeBuilder<WorkRegion> builder)
        {
            builder.ToTable("WorkRegion");

            builder.HasKey(wr => wr.id);

            builder.Property(wr => wr.id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(wr => wr.name)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(wr => wr.latitude)
                .HasColumnType("float");

            builder.Property(wr => wr.longitude)
                .HasColumnType("float");

            builder.HasMany(wr => wr.Users)
                .WithOne(u => u.WorkRegion)
                .HasForeignKey(u => u.WorkRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(wr => wr.Trips)
                .WithOne(t => t.WorkRegion)
                .HasForeignKey(t => t.WorkRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(wr => wr.TradingPoints)
                .WithOne(tp => tp.WorkRegion)
                .HasForeignKey(tp => tp.WorkRegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(wr => wr.CreatedAt)
                .HasColumnType("datetime2");

            builder.Property(wr => wr.UpdatedAt)
                .HasColumnType("datetime2");
        }
    }
}
