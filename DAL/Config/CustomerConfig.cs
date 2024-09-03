
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configs
{
    internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("varchar(20)")
                .IsRequired(false);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            builder.HasIndex(c => c.PhoneNumber)
                .IsUnique();

            builder.HasMany(c => c.TradingPoints)
               .WithOne(tp => tp.Customer)
               .HasForeignKey(tp => tp.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
