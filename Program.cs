using BasicAuth.Context;
using BasicAuth.Services.Interfaces;
using BasicAuth.Services.Providers;
using BasicAuth.Storage.Repository.UserRepository.Interfaces;
using BasicAuth.Storage.Repository.UserRepository.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Registering the services and repositories to the container.
builder.Services.AddScoped<IUserEntityRepository, UserEntityRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// Registering the Postgresql
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(config.GetConnectionString("PostgresDb")));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
