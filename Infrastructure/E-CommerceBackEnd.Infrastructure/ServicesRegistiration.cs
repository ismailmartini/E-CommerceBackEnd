using E_CommerceBackEnd.Application.Services;
using E_CommerceBackEnd.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Infrastructure
{
    public static class ServicesRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService,FileService>();
        }
         
    }
}
