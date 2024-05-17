using KeyCloakAuth.Services;
using KeyCloakAuth.Services.Contracts;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICallbackKeycloakService, CallbackKeycloakService>();

builder.Services.AddScoped<ICallbackKeycloakService, CallbackKeycloakService>(sp =>
    new CallbackKeycloakService(
        sp.GetRequiredService<IHttpContextAccessor>(),
        sp.GetRequiredService<IDistributedCache>()
    ));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
