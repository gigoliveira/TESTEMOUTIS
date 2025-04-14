using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Status)
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.OwnsOne(s => s.Customer, customer =>
            {
                customer.Property(c => c.ExternalCustomerId).HasColumnName("CustomerId");
                customer.Property(c => c.CustomerName).HasMaxLength(100);
            });

            builder.OwnsOne(s => s.Branch, branch =>
            {
                branch.Property(b => b.ExternalBranchId).HasColumnName("BranchId");
                branch.Property(b => b.BranchName).HasMaxLength(100);
            });

            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey(i => i.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
