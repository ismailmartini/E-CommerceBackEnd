﻿using E_CommerceBackEnd.Application.Abstractions.Services;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.CreateUser;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.FacebookLogin;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.GoogleLogin;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.LoginUser;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        readonly IMediator _mediator;
        readonly IMailService _mailService;
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response= await  _mediator.Send(createUserCommandRequest);

            return Ok(response);
        }
        
        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {            
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }

    }
}
