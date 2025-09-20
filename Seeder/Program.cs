using Learnly.Repository;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        // Caminho do banco SQLite local
        var dbPath = "Learnly.db";

        // Configura opções do DbContext para SQLite
        var options = new DbContextOptionsBuilder<LearnlyContexto>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;

        using var contexto = new LearnlyContexto(options);

        // Cria o banco se não existir
        await contexto.Database.EnsureCreatedAsync();

        Console.WriteLine("Banco criado ou já existente.");

        // Executa o Seeder
        Console.WriteLine("Iniciando seed das questões...");
        await DbSeeder.Inicializar(contexto);

        Console.WriteLine("Seed finalizado com sucesso!");
    }
}
