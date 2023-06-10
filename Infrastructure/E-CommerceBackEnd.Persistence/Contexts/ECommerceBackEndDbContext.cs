﻿using E_CommerceBackEnd.Domain.Entities;
using E_CommerceBackEnd.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence.Contexts
{
    public class ECommerceBackEndDbContext : DbContext
    {
        public ECommerceBackEndDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //add interceptor insert and update
            // ChangeTracker : Entityler üzerinden yapılan değişikliklerib ya da yeni ekleen verilerib yakalanmasını sağlayan property.
            // Update operasyonlarında Track edilen veriileri yakalayıp elde etmemizi sağlar
            var datas =ChangeTracker.Entries<BaseEntities>();
            
            foreach (var data in datas)
            {
               _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
