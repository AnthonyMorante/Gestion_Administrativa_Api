using AutoMapper;
using Gestion_Administrativa_Api.Auth;
using Gestion_Administrativa_Api.AutoMapper;
using Gestion_Administrativa_Api.Configuration;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Sri;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Repository;
using Gestion_Administrativa_Api.Utilities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Rotativa.AspNetCore;
using System.Data;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Wkhtmltopdf.NetCore;
using static Gestion_Administrativa_Api.Interfaces.Interfaz.IpuntoEmisiones;
using static Gestion_Administrativa_Api.Repository.IUserRepository;

var builder = WebApplication.CreateBuilder(args);

Tools.Initialize(builder.Configuration, builder.Environment);
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


builder.Services.AddTransient<IDbConnection>(db => new NpgsqlConnection(
config.GetConnectionString("cn")));


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

    options.AddPolicy("cors", builder =>
    {
        builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    });
});



builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddIdentityServerAuthentication("Bearer", options =>
{
    options.Authority = config.GetConnectionString("IdentityServerAuthentication");
    options.RequireHttpsMetadata = false;
    options.JwtValidationClockSkew = TimeSpan.FromHours(5);
});


builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IClientes, ClientesI>();
builder.Services.AddTransient<ITipoIdentificaciones, TipoIdentificacionesI>();
builder.Services.AddTransient<IProvincias, ProvinciasI>();
builder.Services.AddTransient<ICiudades, CiudadesI>();
builder.Services.AddTransient<IProveedores, ProveedoresI>();
builder.Services.AddTransient<IEmpleados, EmpleadosI>();
builder.Services.AddTransient<IProductos, ProductosI>();
builder.Services.AddTransient<IIvas, IvasI>();
builder.Services.AddTransient<IDatosContribuyentes, DatosContribuyentesI>();
builder.Services.AddTransient<IEstablecimientos, EstablecimientosI>();
builder.Services.AddTransient<IPuntoEmisiones, PuntoEmisionesI>();
builder.Services.AddTransient<IDocumentosEmitir, DocumentosEmitirI>();
builder.Services.AddTransient<IDetallePrecioProductos, DetallePrecioProductosI>();
builder.Services.AddTransient<ISecuenciales, SecuencialesI>();
builder.Services.AddTransient<IFormaPagos, FormaPagosI>();
builder.Services.AddTransient<ITiempoFormaPagos, TiempoFormaPagosI>();
builder.Services.AddTransient<IFacturas, FacturasI>();
builder.Services.AddTransient<IUtilidades, UtilidadesI>();




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

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIdentityServer();
app.MapControllers();
app.UseCors("cors");
RotativaConfiguration.Setup(app.Environment.WebRootPath);
app.Run();
