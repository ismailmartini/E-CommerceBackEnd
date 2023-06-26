using E_CommerceBackEnd.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
