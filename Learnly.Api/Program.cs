using Learnly.Application.Applications;
using Learnly.Application.Interfaces;
using Learnly.Repository;
using Learnly.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar aplicações

builder.Services.AddScoped<ILoginAplicacao, LoginAplicacao>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();


// Adicionar Serviços

// builder.Services.AddScoped<Interface, Aplicacao>();

// Adicionar repositorios

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();


builder.Services.AddControllers();

// Adicionar o serviço de banco de Dados

builder.Services.AddDbContext<LearnlyContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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