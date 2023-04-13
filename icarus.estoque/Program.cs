using icarus.estoque.AsyncComm;
using icarus.estoque.Data;
using icarus.estoque.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepoEstoque, RepoEstoque>();
builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
builder.Services.AddDbContext<DataContextEstoque>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("docker"), serverVersion));
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
