using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using VShop.ProductAPI.Context;
using VShop.ProductAPI.Repositories;
using VShop.ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions( x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=DESKTOP-1NDP0IE; Initial Catalog=VShopDB;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;Trusted_Connection=True"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/caelid"; // O issuer
        options.Audience = "caelid-api"; // O audience
        options.RequireHttpsMetadata = false; // Desabilitar verificação de HTTPS temporariamente para desenvolvimento
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero // opcional, para remover a margem de segurança
        };
    });


builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    var request = context.Request;

    Console.WriteLine($"Recebida solicitação: {request.Method} {request.Path}");

    var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();

    if (!string.IsNullOrEmpty(authorizationHeader))
    {
        var token = authorizationHeader.Split(" ").Last();
        Console.WriteLine($"Token JWT recebido: {token}");
    }
    else
    {
        Console.WriteLine("Nenhum cabeçalho de autorização encontrado na solicitação.");
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
