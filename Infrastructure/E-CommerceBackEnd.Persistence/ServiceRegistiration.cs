using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Persistence.Contexts;
using E_CommerceBackEnd.Persistence.Repositories;
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
        //add ioc container

        public static void AddPersistanceServices(this IServiceCollection services)
        {
         services.AddDbContext<ECommerceBackEndDbContext>(options=>options.UseNpgsql(Configuration.ConnectionString));
         services.AddScoped<ICostumerReadRepository,CustomerReadRepository>(); 
         services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>(); 
         services.AddScoped<IOrderReadRepository,OrderReadRepository>(); 
         services.AddScoped<IOrderWriteRepository,OrderWriteRepository>(); 
         services.AddScoped<IProductReadRepository,ProductReadRepository>(); 
         services.AddScoped<IProductWriteRepository,ProductWriteRepository>(); 
            

        }


    }
}
