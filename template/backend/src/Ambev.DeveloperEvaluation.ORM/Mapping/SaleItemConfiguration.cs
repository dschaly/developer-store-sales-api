using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Quantity).IsRequired();
            builder.Property(u => u.UnitPrice).IsRequired();
            builder.Property(u => u.Discount);
            builder.Property(u => u.TotalAmount).IsRequired();
            builder.Property(u => u.SaleId).IsRequired();
            builder.Property(u => u.ProductId).IsRequired();

            builder.HasOne(u => u.Sale)
                .WithMany(u => u.SaleItems)
                .HasForeignKey(u => u.SaleId);

            builder.HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId);
        }
    }
}