﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Abstractions.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);
    }
}
