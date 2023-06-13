using FluentValidation.AspNetCore;
using E_CommerceBackEnd.Persistence;
using E_CommerceBackEnd.Application.Validators.Products;
using E_CommerceBackEnd.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddPersistanceServices();
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

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
