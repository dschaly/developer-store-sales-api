using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(b => b.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(b => b.TotalAmount).IsRequired();
            builder.Property(b => b.IsCancelled).IsRequired();
            builder.Property(b => b.CustomerId).IsRequired();
            builder.Property(b => b.BranchId).IsRequired();

            // FK Statements
            builder.HasMany(b => b.SaleItems)
                .WithOne()
                .HasForeignKey(b => b.SaleId);

            builder.HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId);

            builder.HasOne(b => b.Branch)
                .WithMany()
                .HasForeignKey(b => b.BranchId);

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