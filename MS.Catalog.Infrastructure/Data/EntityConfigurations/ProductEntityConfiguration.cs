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
                .HasConversion(id => id.Value, v => v)
                .ValueGeneratedNever()
                .HasColumnType("varchar(36)");


            builder.Property(x => x.Name)
                .HasColumnType("varchar(50)")
                .IsRequired()
                .HasConversion(n => n.Value, v => v);




            builder.Property(x => x.Description)
                .HasColumnType("varchar(500)")
                .HasConversion(d => d.Value, v => v);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .HasConversion(d => d.Value, v => v);

            builder.Property(x => x.AvailableStock)
                .HasConversion(a=>a.Value,v=>v);

            builder.Ignore(x=>x.DomainEvents);
                
        }
    }
}
