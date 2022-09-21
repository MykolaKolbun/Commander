using Commander.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// new way to access appssettings.json file, also to environment variables
// var connectionstring = builder.Configuration.GetConnectionString("CommanderConnection");

//Adding DBContext service
builder.Services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("CommanderConnection")
));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Adding AutoMapper extesion to services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding repository interface and it's implementation to sevice to use dependency injection in controller class constructor. 
// Scoped - one new service for request, Singletone - same for every request, Transient - new instance creats every time


//builder.Services.AddScoped<ICommanderRepo, MockCommanderRepo>();  // Dependency injection for Mock-based repository
builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();     // Dependency injection for Sql-based repository

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
