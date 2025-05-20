using Autofac;
using Autofac.Extensions.DependencyInjection;

using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encyption;
using Core.Utilities.Security.JWT;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// 👇 Autofac'i DI container olarak kullan
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

// 🔐 Token ayarlarını appsettings.json'dan al
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
           // RoleClaimType = ClaimTypes.Role,
    //  ClockSkew = TimeSpan.Zero // ⏱ hassasiyet için önerilir
        };
    });

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpContextAccessor();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});
//ServiceTool.Create(builder.Services); // << BU SATIR ZORUNLU



var app = builder.Build();

// 🔍 Dev ortamında Swagger aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod()
              .AllowAnyHeader());
app.UseAuthentication(); // 🚨 Authentication, Authorization'dan önce olmalı
app.UseAuthorization();
app.MapControllers();
app.Run();
