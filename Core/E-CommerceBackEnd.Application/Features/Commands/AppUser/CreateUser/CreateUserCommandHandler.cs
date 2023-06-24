using E_CommerceBackEnd.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceBackEnd.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager )
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           IdentityResult result= await _userManager.CreateAsync(new()
            {
                Id=Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname=request.NameSurname
            }, request.Password);

            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı Başarıyla Oluşturulmuştur.";
            else
                 foreach (var err in result.Errors)
                {
                    response.Message += $"{err.Code} - {err.Description} ";
                }
                
           return response;
          //  throw new UserCreateFailedException();
                

        }
    }
}
