using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learnly.Domain.Entities.Simulados;

namespace Learnly.Repository.Interfaces
{
    public interface ISimuladoRepositorio
    {
        Task<int> GerarSimulado(Simulado simulado, List<SimuladoQuestao> questoes);
        Task<List<Questao>> GerarQuestoesAsync(List<string> disciplinas, int totalQuestoes = 25);
        Task AtualizarSimuladoAsync(List<SimuladoQuestao> simuladoQuestoes);
        Task ResponderSimulado(Simulado simulado);
        Task<Simulado> Obter(int simuladoId);
        Task<Questao> ObterQuestao(int questaoId);
        Task<Alternativa> ObterAlternativa(int questaoId);
    }
}