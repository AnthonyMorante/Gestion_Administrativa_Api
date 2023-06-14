using AutoMapper;
using Gestion_Administrativa_Api.Auth;
using Gestion_Administrativa_Api.AutoMapper;
using Gestion_Administrativa_Api.Configuration;
using Gestion_Administrativa_Api.Interfaces;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Repository;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using static Gestion_Administrativa_Api.Repository.IUserRepository;

var builder = WebApplication.CreateBuilder(args);


SigningCredentials CreateSigningCredential()
{
    var credentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.RsaSha256);

    return credentials;
}
RSACryptoServiceProvider GetRSACryptoServiceProvider()
{
    return new RSACryptoServiceProvider(2048);
}
SecurityKey GetSecurityKey()
{
    return new RsaSecurityKey(GetRSACryptoServiceProvider());
}



IConfiguration config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
      .AddEnvironmentVariables()
      .Build();


builder.Services.AddDbContext<_context>(options =>
options.UseNpgsql(config.GetConnectionString("cn")));


// Add services to the container.

var identityResources = config.GetSection("IdentityServer:IdentityResources").Get<List<IdentityResource>>();
var apiScopes = config.GetSection("IdentityServer:ApiScopes").Get<List<ApiScope>>();


builder.Services.AddIdentityServer()

                .AddInMemoryIdentityResources(identityResources)
                .AddInMemoryApiScopes(apiScopes)
                .AddInMemoryClients(config.GetSection("IdentityServer:Clients").Get<List<Client>>())
                .AddProfileService<ProfileService>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddSigningCredential(CreateSigningCredential());


var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ConsumesAttribute("application/json"));
    options.Filters.Add(new ProducesAttribute("application/json"));
});

builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("cors", builder =>
    {
        builder

             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    });
});


builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IClientes, ClientesI>();
builder.Services.AddTransient<ITipoIdentificaciones, TipoIdentificacionesI>();
builder.Services.AddTransient<IProvincias, ProvinciasI>();
builder.Services.AddTransient<ICiudades, CiudadesI>();







builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddIdentityServerAuthentication("Bearer", options =>
{
    options.Authority = config.GetConnectionString("IdentityServerAuthentication");
    options.RequireHttpsMetadata = false;
    options.JwtValidationClockSkew = TimeSpan.Zero;
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.UseCors("cors");
app.Run();
