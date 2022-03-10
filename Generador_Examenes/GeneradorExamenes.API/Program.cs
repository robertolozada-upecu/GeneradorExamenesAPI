using AccesoData;
using AccesoData.Contexto;
using AccesoData.InicializarDB;
using AccessoData.InicializarDB;
using GeneradorExamenes.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Negocio.Repositorio;
using Negocio.Repositorio.IRepositorio;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Generador Examenes API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
});

builder.Services.AddDbContext<AppDbContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Identity 
builder.Services.AddIdentity<Usuario, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

//Configuracion JWT
var seccionJwt = builder.Configuration.GetSection("ConfiguracionJWT");
builder.Services.Configure<ConfiguracionJWT>(seccionJwt);

//Autenticacion
var configuracionJwt = seccionJwt.Get<ConfiguracionJWT>();
var secreto = Encoding.ASCII.GetBytes(configuracionJwt.Secreto);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secreto),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Repositorios
builder.Services.AddScoped<IExamenRepositorio, ExamenRepositorio>();
builder.Services.AddScoped<IPreguntaRepositorio, PreguntaRepositorio>();
builder.Services.AddScoped<IRespuestaRepositorio, RespuestaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddScoped<IInicializadorDB, InicializadorDB>();


var app = builder.Build();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

InicializarDB();


// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



void InicializarDB()
{
    using (var scorpe = app.Services.CreateScope())
    {
        var inicializador = scorpe.ServiceProvider.GetRequiredService<IInicializadorDB>();
        inicializador.InicializarDB();
    }
}
