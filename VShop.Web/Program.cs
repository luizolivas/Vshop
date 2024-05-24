using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VShop.Web.Services;
using VShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ProductApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ITokenService,TokenService>();
builder.Services.AddScoped<ILogoutService, LogoutService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Adicionar o servi�o de sess�o
builder.Services.AddSession();

// Configurar o servi�o de LoginService
builder.Services.AddScoped<LoginService>(sp =>
    new LoginService(sp.GetRequiredService<IHttpContextAccessor>(),
                            "http://localhost:8080/realms/caelid", // Base URL do Keycloak
                            "caelid-api",       // Seu clientId
                            "https://localhost:7097/", sp.GetRequiredService<ITokenService>(), sp.GetRequiredService<ILogoutService>()));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Configura��o do JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "http://localhost:8080/realms/caelid";
    options.Audience = "caelid-api";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = "http://localhost:8080/realms/caelid",
        ValidAudience = "caelid-api"
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configurar o servi�o de sess�o
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
