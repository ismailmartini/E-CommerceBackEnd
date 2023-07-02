using E_CommerceBackEnd.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.SignalR
{
    public static class HubRegistiration
    {
        //bütün hubları merkezi olarak buradan yöneteceğiz
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<ProductHub>("/products-hub");

        }
    }
}
