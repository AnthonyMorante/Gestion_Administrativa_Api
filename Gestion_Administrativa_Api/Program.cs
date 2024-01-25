using AutoMapper;
using Gestion_Administrativa_Api.Auth;
using Gestion_Administrativa_Api.AutoMapper;
using Gestion_Administrativa_Api.Interfaces.Interfaz;
using Gestion_Administrativa_Api.Interfaces.Sri;
using Gestion_Administrativa_Api.Interfaces.Utilidades;
using Gestion_Administrativa_Api.Models;
using Gestion_Administrativa_Api.Repository;
using Gestion_Administrativa_Api.Utilities;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddDbContextPool<_context>(options => {
    options.UseSqlServer(config.GetConnectionString("cn"));
    options.EnableSensitiveDataLogging();
    });

//builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(config.GetConnectionString("cn")));

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

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add(new ConsumesAttribute("application/json"));
//    options.Filters.Add(new ProducesAttribute("application/json"));
//});

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

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IClientes, ClientesI>();
builder.Services.AddScoped<ITipoIdentificaciones, TipoIdentificacionesI>();
builder.Services.AddScoped<IProvincias, ProvinciasI>();
builder.Services.AddScoped<ICiudades, CiudadesI>();
builder.Services.AddScoped<IProveedores, ProveedoresI>();
builder.Services.AddScoped<IEmpleados, EmpleadosI>();
builder.Services.AddScoped<IProductos, ProductosI>();
builder.Services.AddScoped<IIvas, IvasI>();
builder.Services.AddScoped<IDatosContribuyentes, DatosContribuyentesI>();
builder.Services.AddScoped<IEstablecimientos, EstablecimientosI>();
builder.Services.AddScoped<IPuntoEmisiones, PuntoEmisionesI>();
builder.Services.AddScoped<IDocumentosEmitir, DocumentosEmitirI>();
builder.Services.AddScoped<IDetallePrecioProductos, DetallePrecioProductosI>();
builder.Services.AddScoped<ISecuenciales, SecuencialesI>();
builder.Services.AddScoped<IFormaPagos, FormaPagosI>();
builder.Services.AddScoped<ITiempoFormaPagos, TiempoFormaPagosI>();
builder.Services.AddScoped<IFacturas, FacturasI>();
builder.Services.AddScoped<IUtilidades, UtilidadesI>();
builder.Services.AddScoped<IRetenciones, RetencionesI>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWkhtmltopdf();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
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
string wwwroot = app.Environment.WebRootPath;
RotativaConfiguration.Setup(wwwroot, "Rotativa/Windows");
app.Run();