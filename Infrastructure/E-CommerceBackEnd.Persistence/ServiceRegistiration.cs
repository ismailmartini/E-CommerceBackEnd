﻿using E_CommerceBackEnd.Application.Abstractions.Services;
using E_CommerceBackEnd.Application.Abstractions.Services.Authentications;
using E_CommerceBackEnd.Application.Repositories;
using E_CommerceBackEnd.Domain.Entities.Identity;
using E_CommerceBackEnd.Persistence.Contexts;
using E_CommerceBackEnd.Persistence.Repositories;
using E_CommerceBackEnd.Persistence.Services;
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
         services.AddIdentity<AppUser, AppRole>(options =>
         {
             options.Password.RequiredLength = 6;
             options.Password.RequireNonAlphanumeric=false;
             options.Password.RequireDigit=false;
             options.Password.RequireLowercase=false;
             options.Password.RequireUppercase=false;
              
         }).AddEntityFrameworkStores<ECommerceBackEndDbContext>();
         services.AddScoped<ICostumerReadRepository,CustomerReadRepository>(); 
         services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>(); 
         services.AddScoped<IOrderReadRepository,OrderReadRepository>(); 
         services.AddScoped<IOrderWriteRepository,OrderWriteRepository>(); 
         services.AddScoped<IProductReadRepository,ProductReadRepository>(); 
         services.AddScoped<IProductWriteRepository,ProductWriteRepository>(); 
         services.AddScoped<IFileReadRepository,FileReadRepository>();
         services.AddScoped<IFileWriteRepository,FileWriteRepository>();
         services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
         services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();           
         services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
         services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
         services.AddScoped<IUserService, UserService>();
         services.AddScoped<IAuthService, AuthService>();
         services.AddScoped<IExternalAuthentication, AuthService>();
         services.AddScoped<IInternalAuthentication, AuthService>();



        }


    }
}
