
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class PhotoConfig : IEntityTypeConfiguration<Photo>
    {

        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("Photo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FilePath)
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(p => p.VisitId)
                .HasColumnType("int");

            builder.Property(p => p.CreatedAt)
                .HasColumnType("datetime2");

            builder.Property(p => p.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasOne(p => p.Visit)
               .WithMany(v => v.Photos)
               .HasForeignKey(p => p.VisitId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.VisitId);
        }
    }
}
