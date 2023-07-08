using E_CommerceBackEnd.Application.DTOs.User;
using E_CommerceBackEnd.Domain.Entities.Identity;

namespace E_CommerceBackEnd.Application.Abstractions.Services
{
    public interface IUserService
    {

        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    }
}
