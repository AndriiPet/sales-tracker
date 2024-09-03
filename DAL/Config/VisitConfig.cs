
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class VisitConfig : IEntityTypeConfiguration<Visit>
    {

        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visit");

            builder.HasKey(v => v.id);

            builder.Property(v => v.id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(v => v.isVisited)
               .HasColumnType("boolean");

            builder.Property(v => v.isPriority)
                .HasColumnType("boolean");

            builder.Property(v => v.timeStart)
                .HasColumnType("datetime2");

            builder.Property(v => v.timeEnd)
                .HasColumnType("datetime2");

            builder.Property(v => v.visitDate)
                .HasColumnType("datetime2");

            builder.Property(v => v.latitude)
                .HasColumnType("float");

            builder.Property(v => v.longitude)
                .HasColumnType("float");

            builder.Property(v => v.comment)
                .HasColumnType("nvarchar(max)");

            builder.Property(v => v.TradingPointId)
               .HasColumnType("int");

            builder.Property(v => v.TripId)
                .HasColumnType("int");

            builder.HasOne(v => v.tradingPoint)
                .WithMany(tp => tp.Visits)
                .HasForeignKey(v => v.TradingPointId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Trip)
                .WithMany(t => t.Visits)
                .HasForeignKey(v => v.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(v => v.Photos)
                .WithOne()
                .HasForeignKey(p => p.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(v => v.CreatedAt)
                .HasColumnType("datetime2");

            builder.Property(v => v.UpdatedAt)
                .HasColumnType("datetime2");
        }
    }
}
