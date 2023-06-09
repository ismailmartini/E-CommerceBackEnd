using E_CommerceBackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence
{
    public static class ServiceRegistiration
    {

        public static void AddPersistanceServices(this IServiceCollection services)
        {
         services.AddDbContext<ECommerceBackEndDbContext>(options=>options.UseNpgsql(Configuration.ConnectionString));   
        }


    }
}
