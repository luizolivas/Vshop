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
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Adicionar o serviço de sessão
builder.Services.AddSession();

// Configurar o serviço de LoginService
builder.Services.AddScoped<LoginService>(sp =>
    new LoginService(sp.GetRequiredService<IHttpContextAccessor>(),
                            "http://localhost:8080/realms/caelid", // Base URL do Keycloak
                            "caelid-api",       // Seu clientId
                            "https://localhost:7097/"));

var app = builder.Build();

// Configurar o serviço de sessão
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
