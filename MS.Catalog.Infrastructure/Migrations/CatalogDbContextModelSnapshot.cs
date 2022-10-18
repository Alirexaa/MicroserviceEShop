﻿// <auto-generated />
using MS.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MS.Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MS.Catalog.Domain.ProductsAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("varchar(36)")
                        .HasColumnName("id");

                    b.Property<int>("AvailableStock")
                        .HasColumnType("integer")
                        .HasColumnName("available_stock");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("price");

                    b.Property<long>("Version")
                        .HasColumnType("bigint")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("Id")
                        .HasDatabaseName("ix_products_id");

                    b.ToTable("products", "catalog");
                });
#pragma warning restore 612, 618
        }
    }
}
