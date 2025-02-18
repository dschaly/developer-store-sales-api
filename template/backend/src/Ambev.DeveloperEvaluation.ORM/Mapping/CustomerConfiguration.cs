using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Email).IsRequired().HasMaxLength(100);

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(b => b.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UpdatedAt)
                .IsRequired(false)
                .HasDefaultValueSql("NULL");

            builder.Property(b => b.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}