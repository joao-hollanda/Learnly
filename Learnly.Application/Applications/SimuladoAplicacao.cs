using Learnly.Application.Interfaces;
using Learnly.Domain.Entities;
using Learnly.Domain.Entities.Simulados;
using Learnly.Repository.Interfaces;

namespace Learnly.Application.Applications
{
    public class SimuladoAplicacao : ISimuladoAplicacao
    {
        readonly ISimuladoRepositorio _simuladoRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;

        public SimuladoAplicacao(ISimuladoRepositorio simuladoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _simuladoRepositorio = simuladoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        public async Task<int> GerarSimulado(Simulado simulado, List<string> disciplinas, int totalQuestoes = 25)
        {
            if (await _usuarioRepositorio.Obter(simulado.UsuarioId, true) == null)
                throw new Exception("Usuario não encontrado!");

            if (simulado == null)
                throw new Exception("Simulado não pode ser vazio!");

            var simuladoId = await _simuladoRepositorio.GerarSimulado(simulado, disciplinas, totalQuestoes);

            var questoes = await _simuladoRepositorio.GerarQuestoesAsync(disciplinas, totalQuestoes);

            var simuladoQuestoes = questoes.Select(q => new SimuladoQuestao
            {
                SimuladoId = simuladoId,
                QuestaoId = q.QuestaoId
            }).ToList();

            await _simuladoRepositorio.AtualizarSimuladoAsync(simuladoQuestoes);

            return simuladoId;
        }

    }
}