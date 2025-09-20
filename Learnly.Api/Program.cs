using Learnly.Application.Applications;
using Learnly.Application.Interfaces;
using Learnly.Repository;
using Learnly.Repository.Interfaces;
using Learnly.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar aplicações

builder.Services.AddScoped<ILoginAplicacao, LoginAplicacao>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<ISimuladoAplicacao, SimuladoAplicacao>();


// Adicionar Serviços

// builder.Services.AddScoped<Interface, Aplicacao>();

// Adicionar repositorios

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISimuladoRepositorio, SimuladoRepositorio>();


builder.Services.AddControllers();

// Adicionar o serviço de banco de Dados

builder.Services.AddDbContext<LearnlyContexto>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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