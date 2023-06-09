
using E_CommerceBackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceBackEndDbContext>
    {
        //for power shell cli with migration

        
        public ECommerceBackEndDbContext CreateDbContext(string[] args)
        {
             

            DbContextOptionsBuilder<ECommerceBackEndDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new ECommerceBackEndDbContext(dbContextOptionsBuilder.Options);  
        }
    }
}
