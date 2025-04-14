using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity);

            builder.OwnsOne(s => s.Product, product =>
            {
                product.Property(p => p.ExternalProductId).HasColumnName("ProductId");
                product.Property(p => p.ProductName).HasMaxLength(100).HasColumnName("ProductName");
                product.Property(p => p.ProductPrice).HasColumnName("ProductPrice");
            });

            builder.Ignore(s => s.TotalValue); // <-- aqui resolve o erro
        }
    }

}
