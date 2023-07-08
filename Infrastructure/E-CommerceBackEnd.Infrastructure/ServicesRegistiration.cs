
using E_CommerceBackEnd.Application.Abstractions.Services;
using E_CommerceBackEnd.Application.Abstractions.Storage;
using E_CommerceBackEnd.Application.Abstractions.Token;
using E_CommerceBackEnd.Infrastructure.Enums;
using E_CommerceBackEnd.Infrastructure.Services;
using E_CommerceBackEnd.Infrastructure.Services.Storage;
using E_CommerceBackEnd.Infrastructure.Services.Storage.Azure;
using E_CommerceBackEnd.Infrastructure.Services.Storage.Local;
using E_CommerceBackEnd.Infrastructure.Services.Token;
using ETicaretAPI.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceBackEnd.Infrastructure
{
    public static class ServicesRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {


            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                     serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;


            }

        }


    }
}
