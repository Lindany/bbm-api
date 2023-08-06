using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BBMApi.Data;
using BBMApi;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BBMApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BBMApiContext")
    ?? throw new InvalidOperationException("Connection string 'BBMApiContext' not found."),
    builder => builder.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null)));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapChurchEndpoints();

app.MapLeaderEndpoints();

app.MapPersonEndpoints();

app.MapUserEndpoints();

app.MapStatsEndpoints();

app.Run();
