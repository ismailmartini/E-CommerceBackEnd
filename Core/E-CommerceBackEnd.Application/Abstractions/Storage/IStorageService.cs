﻿namespace E_CommerceBackEnd.Application.Abstractions.Storage
{
    public interface IStorageService:IStorage
    {
        public string StorageName { get; }
        
    }
}
