using CepServiceApp.Data;
using CepServiceApp.Repositories;
using CepServiceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = "Server=127.0.0.1;Database=CepDatabase;Uid=devuser;Pwd=12345678;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

// ⚠️ REMOVA esta linha: builder.Services.AddHttpClient();

// Injeção de Dependência
builder.Services.AddScoped<ICepRepository, CepRepository>();
builder.Services.AddScoped<ICepService, CepService>();

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