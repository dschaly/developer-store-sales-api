using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(100);

            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            builder.Property(b => b.UpdatedAt)
                .IsRequired(false)
                .HasDefaultValueSql("NULL");
        }
    }
}