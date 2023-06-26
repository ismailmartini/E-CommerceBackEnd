using E_CommerceBackEnd.Application.Abstractions.Token;
using E_CommerceBackEnd.Application.DTOs;
using E_CommerceBackEnd.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using E_CommerceBackEnd.Application.Abstractions.Services;

namespace E_CommerceBackEnd.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken, 15);
            return new()
            {
                Token = token
            }; 
        }
    }
}
