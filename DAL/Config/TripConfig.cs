
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class TripConfig : IEntityTypeConfiguration<Trip>
    {

        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trip");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.StartDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(t => t.UserId)
               .HasColumnType("int");

            builder.Property(t => t.WorkRegionId)
                .HasColumnType("int");

            builder.Property(t => t.CreatedAt)
               .HasColumnType("datetime2");

            builder.Property(t => t.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder. HasOne(t => t.WorkRegion)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Visits)
                .WithOne(v => v.Trip)
                .HasForeignKey(v => v.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.StartDate);
            builder.HasIndex(t => t.UserId);
            builder.HasIndex(t => t.WorkRegionId);
        }
    }
}
