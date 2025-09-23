using Learnly.Application.Interfaces;
using Learnly.Domain.Entities;
using Learnly.Domain.Entities.Simulados;
using Learnly.Repository.Interfaces;
using Learnly.Services.Interfaces;

namespace Learnly.Application.Applications
{
    public class SimuladoAplicacao : ISimuladoAplicacao
    {
        readonly ISimuladoRepositorio _simuladoRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;
        readonly IIAService _iaService;

        public SimuladoAplicacao(ISimuladoRepositorio simuladoRepositorio, IUsuarioRepositorio usuarioRepositorio, IIAService iaService)
        {
            _simuladoRepositorio = simuladoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _iaService = iaService;
        }

        public async Task<int> GerarSimulado(Simulado simulado, List<string> disciplinas, int totalQuestoes = 25)
        {
            if (simulado == null)
                throw new ArgumentException("Simulado não pode ser nulo.");

            var usuario = await _usuarioRepositorio.Obter(simulado.UsuarioId, true);
            if (usuario == null)
                throw new Exception("Usuário não encontrado!");

            var questoes = await _simuladoRepositorio.GerarQuestoesAsync(disciplinas, totalQuestoes);

            var simuladoQuestoes = questoes.Select(q => new SimuladoQuestao
            {
                QuestaoId = q.QuestaoId
            }).ToList();

            var simuladoId = await _simuladoRepositorio.GerarSimulado(simulado, simuladoQuestoes);

            return simuladoId;
        }

        public async Task<Simulado> ResponderSimulado(Simulado simulado)
        {
            if (simulado == null)
                throw new ArgumentException("Simulado não encontrado!");

            var desempenho = new DesempenhoSimulado();

            foreach (var resposta in simulado.Respostas)
            {
                resposta.Questao = await _simuladoRepositorio.ObterQuestao(resposta.QuestaoId);
                resposta.Alternativa = await _simuladoRepositorio.ObterAlternativa(resposta.AlternativaId);

                if (resposta.Questao == null)
                    throw new Exception($"Questão {resposta.QuestaoId} não encontrada.");

                if (resposta.Alternativa == null)
                    throw new Exception($"Alternativa {resposta.AlternativaId} não encontrada.");

                if (resposta.Questao.AlternativaCorreta == resposta.Alternativa.Letra)
                    resposta.Alternativa.Correta = true;

                desempenho.QuantidadeDeQuestoes++;
            }

            desempenho.QuantidadeDeAcertos = simulado.Respostas.Count(r => r.Alternativa.Correta);
            simulado.Desempenho = desempenho;
            simulado.Desempenho.Feedback = await _iaService.GerarFeedbackAsync(simulado);
            simulado.NotaFinal = (decimal)desempenho.QuantidadeDeAcertos / desempenho.QuantidadeDeQuestoes * 10;

            await _simuladoRepositorio.ResponderSimulado(simulado);
            return await _simuladoRepositorio.Obter(simulado.SimuladoId);
        }

        public async Task<Simulado> Obter(int id)
        {
            var simuladoDominio = await _simuladoRepositorio.Obter(id);

            if (simuladoDominio == null)
                throw new Exception("Simulado não encontrado!");

            return simuladoDominio;
        }
    }
}