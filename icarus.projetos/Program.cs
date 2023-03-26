using icarus.projetos.data;
using icarus.projetos.Repository;
using Microsoft.EntityFrameworkCore;
using MySql.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("docker"), serverVersion));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


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
