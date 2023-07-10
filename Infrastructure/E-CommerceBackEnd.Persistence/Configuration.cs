using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get {

                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/E-CommerceBackEnd.API"));
                try
                {
                    configurationManager.AddJsonFile("appsettings.json");
                
                }
                catch  
                {
                    //release mode in azure                  
                     configurationManager.AddJsonFile("appsettings.production.json");
                   
                }

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
