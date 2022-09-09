using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Catalog.Domain.ProductsAggregate;

namespace MS.Catalog.Infrastructure.Data.EntityConfigurations
{
    internal class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products",CatalogDbContext.DefaultSchema);
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(id => id, v => v.Value)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("varchar(500)");

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.AvailableStock);
            builder.Ignore(x=>x.DomainEvents);
                
        }
    }
}
