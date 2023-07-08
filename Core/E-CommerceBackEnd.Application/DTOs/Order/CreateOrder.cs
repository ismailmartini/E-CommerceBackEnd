﻿using E_CommerceBackEnd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string? BasketId { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
    
    }
}
