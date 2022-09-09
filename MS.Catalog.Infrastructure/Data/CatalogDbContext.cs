﻿using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Data
{
    public class CatalogDbContext : DbContext,IUnitOfWork
    {
        public const string DefaultSchema = "catalog";
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {

        }

        public async Task Compalete()
        {
           await SaveChangesAsync();
        }
    }
}
