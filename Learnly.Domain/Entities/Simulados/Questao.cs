using Learnly.Domain.Entities.Simulados;
using Learnly.Domain.Enums;

public class Questao
{
    public int QuestaoId { get; set; }
    public string Titulo { get; set; }
    public string Disciplina { get; set; }
    public int Ano { get; set; }
    public string Lingua { get; set; }
    public string Contexto { get; set; }
    public string Arquivos { get; set; }
    public string IntroducaoAlternativa { get; set; }

    public string AlternativaCorreta { get; set; }

    public ICollection<Alternativa> Alternativas { get; set; } = new List<Alternativa>();
    public List<SimuladoQuestao> Simulados { get; set; } = new();
    public List<RespostaSimulado> Respostas { get; set; } = new();
}
