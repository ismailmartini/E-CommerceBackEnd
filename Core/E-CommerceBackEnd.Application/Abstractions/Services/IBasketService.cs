﻿using E_CommerceBackEnd.Application.ViewModels.Baskets;
using E_CommerceBackEnd.Domain.Entities;
 

namespace E_CommerceBackEnd.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();  
        public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);
        public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
    }
}
