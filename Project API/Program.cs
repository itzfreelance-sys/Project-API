using Microsoft.EntityFrameworkCore;
using Project_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionstring = $"Data Source=LAPTOP-KPPT6TM3\\SQLEXPRESS;Initial Catalog=Personal;Integrated Security=True";
builder.Services.AddDbContext<PersonalContext>(opt => opt.UseSqlServer(connectionstring));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
