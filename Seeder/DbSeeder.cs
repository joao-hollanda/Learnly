using Learnly.Domain.Entities.Simulados;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

public static class DbSeeder
{
    public static async Task Inicializar(LearnlyContexto contexto)
    {
        if (contexto.Questoes.Any())
        {
            Console.WriteLine("Banco já contém questões. Seed ignorado.");
            return;
        }

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.enem.dev/v1/");

        for (int ano = 2009; ano <= 2023; ano++)
        {
            Console.WriteLine($"=== Processando questões do ENEM {ano} ===");

            int total = 180;      // Total de questões por ano
            int limit = 50;       // Quantidade por batch (API suporta até 50)
            
            for (int offset = 0; offset < total; offset += limit)
            {
                int currentLimit = Math.Min(limit, total - offset);

                try
                {
                    var url = $"exams/{ano}/questions?limit={currentLimit}&offset={offset}";
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Erro HTTP ano {ano} [{offset + 1}-{offset + currentLimit}]: {response.StatusCode}");
                        await Task.Delay(2000);
                        continue;
                    }

                    var result = await response.Content.ReadFromJsonAsync<QuestionsResponseDTO>();
                    if (result?.questions == null || result.questions.Count == 0)
                    {
                        Console.WriteLine($"Nenhuma questão retornada ano {ano} [{offset + 1}-{offset + currentLimit}]");
                        continue;
                    }

                    foreach (var questaoJson in result.questions)
                    {
                        // Ignorar questões incompletas
                        if (string.IsNullOrWhiteSpace(questaoJson.title) ||
                            string.IsNullOrWhiteSpace(questaoJson.discipline) ||
                            string.IsNullOrWhiteSpace(questaoJson.correctAlternative))
                            continue;

                        // Log de línguas para debug
                        Console.WriteLine($"Questão {questaoJson.index} - Língua: {questaoJson.language}");

                        var questao = new Questao
                        {
                            Titulo = questaoJson.title,
                            Disciplina = questaoJson.discipline,
                            Lingua = string.IsNullOrWhiteSpace(questaoJson.language) ? "pt" : questaoJson.language,
                            Ano = questaoJson.year,
                            Contexto = questaoJson.context,
                            Arquivos = questaoJson.files != null ? string.Join(";", questaoJson.files) : null,
                            IntroducaoAlternativa = questaoJson.alternativesIntroduction,
                            AlternativaCorreta = questaoJson.correctAlternative,
                            Alternativas = new List<Alternativa>()
                        };

                        if (questaoJson.alternatives != null)
                        {
                            foreach (var altJson in questaoJson.alternatives)
                            {
                                if (string.IsNullOrWhiteSpace(altJson.letter) || string.IsNullOrWhiteSpace(altJson.text))
                                    continue;

                                var alternativa = new Alternativa
                                {
                                    Letra = altJson.letter,
                                    Texto = altJson.text,
                                    Arquivo = altJson.file,
                                    Correta = altJson.isCorrect,
                                    Questao = questao
                                };
                                questao.Alternativas.Add(alternativa);
                            }
                        }

                        contexto.Questoes.Add(questao);
                    }

                    await contexto.SaveChangesAsync();
                    Console.WriteLine($"Batch {offset + 1}-{offset + currentLimit} do ano {ano} processado com sucesso.");
                    await Task.Delay(1500); // Delay para respeitar limite da API
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Erro de banco ano {ano} [{offset + 1}-{offset + currentLimit}]: {ex.InnerException?.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado ano {ano} [{offset + 1}-{offset + currentLimit}]: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Seed finalizado com sucesso!");
    }
}

// DTOs
public class QuestionsResponseDTO
{
    public MetadataDTO metadata { get; set; }
    public List<QuestaoJsonDTO> questions { get; set; }
}

public class MetadataDTO
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public bool hasMore { get; set; }
}

public class QuestaoJsonDTO
{
    public string title { get; set; }
    public int index { get; set; }
    public string discipline { get; set; }
    public string language { get; set; }
    public int year { get; set; }
    public string context { get; set; }
    public string[] files { get; set; }
    public string correctAlternative { get; set; }
    public string alternativesIntroduction { get; set; }
    public List<AlternativaJsonDTO> alternatives { get; set; }
}

public class AlternativaJsonDTO
{
    public string letter { get; set; }
    public string text { get; set; }
    public string file { get; set; }
    public bool isCorrect { get; set; }
}
