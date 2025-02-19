using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(b => b.Title).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Price).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Category).HasMaxLength(50);
            builder.Property(p => p.Image).HasMaxLength(255);

            builder.OwnsOne(p => p.Rating, rating =>
            {
                rating.Property(r => r.Rate).HasColumnName("Rate").HasPrecision(2, 1);
                rating.Property(r => r.Count).HasColumnName("RateCount");
            });

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