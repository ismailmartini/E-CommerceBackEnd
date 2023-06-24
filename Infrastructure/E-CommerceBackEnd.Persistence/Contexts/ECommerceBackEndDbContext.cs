﻿using E_CommerceBackEnd.Domain.Entities;
using E_CommerceBackEnd.Domain.Entities.Common;
using E_CommerceBackEnd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace E_CommerceBackEnd.Persistence.Contexts
{
    public class ECommerceBackEndDbContext : IdentityDbContext<AppUser,AppRole,string> 
    {
        public ECommerceBackEndDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //ef core table per hierarchy
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

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
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
