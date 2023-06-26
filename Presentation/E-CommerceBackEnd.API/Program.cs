using FluentValidation.AspNetCore;
using E_CommerceBackEnd.Persistence;
using E_CommerceBackEnd.Application.Validators.Products;
using E_CommerceBackEnd.Infrastructure.Filters;
using E_CommerceBackEnd.Infrastructure;
using E_CommerceBackEnd.Infrastructure.Services.Storage.Local;
using E_CommerceBackEnd.Infrastructure.Services.Storage.Azure;
using E_CommerceBackEnd.Application;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage<AzureStorage>(); // use localStorage or AWS or Amazon

builder.Services.AddStorage<LocalStorage>();

//builder.Services.AddStorage(E_CommerceBackEnd.Infrastructure.Enums.StorageType.Azure); // use localStorage or AWS or Amazon

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin",options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, //oluşturulacak token değerini kimlerin/hangi orjinlerin/sitelerin kullanılacağını belirlediğimiz değerdir. =>www.exapmle.com
                        ValidateIssuer = true, //oluşturulacak token değerini kimin dağıttığını ifade edeceğimzi alan =>www.myapi.com
                        ValidateLifetime = true, //oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                        ValidateIssuerSigningKey = true, //üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key doğrulaması.

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore,expires,securityToken,validationParameters) => expires !=null ? expires >DateTime.UtcNow:false
                    };
                });

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuraiton => configuraiton.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())//bir validator verdiğimzide hepsini alıcak

    .ConfigureApiBehaviorOptions(options=>options.SuppressModelStateInvalidFilter=true); //controller'dan önceki mevcut olan filtreleri çalıştırma
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
