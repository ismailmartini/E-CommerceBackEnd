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
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using Serilog.Context;
using E_CommerceBackEnd.API.Configurations.ColumnWriters;
using Microsoft.AspNetCore.HttpLogging;

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
                        LifetimeValidator = (notBefore,expires,securityToken,validationParameters) => expires !=null ? expires >DateTime.UtcNow:false,

                        NameClaimType=ClaimTypes.Name //JWT üzerinde name claimne karşışıl gelen değeri User.Identity name propersitini elde etmeye yarar

                    };
                });


Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",needAutoCreateTable:true,
    columnOptions:new Dictionary<string, ColumnWriterBase>
    {
        {"message",new RenderedMessageColumnWriter() },
        {"message_template",new MessageTemplateColumnWriter()},
        {"level",new LevelColumnWriter()},
        {"time_stamp",new TimestampColumnWriter()},
        {"exception",new ExceptionColumnWriter()},
        {"log_event",new LogEventSerializedColumnWriter()},
        {"user_name", new UserNameColumnWriter()}
    })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log); //seri log

//microsoft http fll log
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
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


app.UseSerilogRequestLogging(); //kod blogu altındaki middle warelerın loglanmasını sağlayacak
app.UseHttpLogging();//microsoft full http logları
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//seri log için middleware yazıp jwt token claimden username propertysini elde edip usernamecolumnwrite push ediliyor
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated!=null || true ? context.User.Identity.Name:null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.Run();
