using Learnly.Application.Applications;
using Learnly.Application.Interfaces;
using Learnly.Repository;
using Learnly.Repository.Interfaces;
using Learnly.Repository.Repositories;
using Learnly.Services.IAService;
using Learnly.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar aplicações

builder.Services.AddScoped<ILoginAplicacao, LoginAplicacao>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();
builder.Services.AddScoped<ISimuladoAplicacao, SimuladoAplicacao>();


// Adicionar Serviços

builder.Services.AddScoped<IIAService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiKey = config["ApiKeys:GroqIA"];
    return new IAService(apiKey);
});

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