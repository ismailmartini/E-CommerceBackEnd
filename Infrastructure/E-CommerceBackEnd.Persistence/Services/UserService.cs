using E_CommerceBackEnd.Application.Abstractions.Services;
using E_CommerceBackEnd.Application.DTOs.User;
using E_CommerceBackEnd.Application.Exceptions;
using E_CommerceBackEnd.Application.Features.Commands.AppUser.CreateUser;
using E_CommerceBackEnd.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı Başarıyla Oluşturulmuştur.";
            else
                foreach (var err in result.Errors)
                {
                    response.Message += $"{err.Code} - {err.Description} ";
                }

            return response;

        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate,int addOnAccesTokenDate)
        {
           
            if(user != null) {
            
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accesTokenDate.AddSeconds(addOnAccesTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
            throw new NotFoundUserException();
            
        }
    }
}
