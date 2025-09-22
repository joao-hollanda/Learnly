using System.Text;
using Microsoft.EntityFrameworkCore;
using Learnly.Domain.Entities.Simulados;
using Learnly.Repository.Interfaces;
using Microsoft.VisualBasic;

namespace Learnly.Repository.Repositories
{
    public class SimuladoRepositorio : ISimuladoRepositorio
    {
        private readonly LearnlyContexto _context;

        public SimuladoRepositorio(LearnlyContexto context)
        {
            _context = context;
        }

        public async Task<int> GerarSimulado(Simulado simulado, List<SimuladoQuestao> questoes)
        {

            await using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Simulados.Add(simulado);
            await _context.SaveChangesAsync();

            _context.SimuladoQuestoes.AddRange(questoes);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return simulado.SimuladoId;
        }

        public async Task<List<Questao>> GerarQuestoesAsync(List<string> disciplinas, int totalQuestoes = 25)
        {
            if (disciplinas == null || disciplinas.Count == 0)
                throw new ArgumentException("A lista de disciplinas não pode estar vazia.");

            var resultado = new List<Questao>();

            int n = disciplinas.Count;
            int baseQtd = totalQuestoes / n;
            int restante = totalQuestoes % n;

            for (int i = 0; i < n; i++)
            {
                var disciplina = disciplinas[i];
                int limite = baseQtd + (restante > 0 ? 1 : 0);
                if (restante > 0) restante--;

                var questoes = await _context.Questoes
                                             .FromSqlRaw(
                                                 "SELECT * FROM Questoes WHERE Disciplina = {0} ORDER BY RANDOM() LIMIT {1}",
                                                 disciplina, limite)
                                             .Include(q => q.Alternativas)
                                             .ToListAsync();

                resultado.AddRange(questoes);
            }

            return resultado;
        }

        public async Task<Simulado> ObterSimulado(int simuladoId)
        {
            return await _context.Simulados.FirstOrDefaultAsync(s => s.SimuladoId == simuladoId && s.Respostas == null);
        }

        public async Task<Simulado> ResponderSimulado(int simuladoId, List<RespostaSimulado> respostas)
        {
            var simuladoDominio = await ObterSimulado(simuladoId);

            if (simuladoDominio == null)
                throw new Exception("Simulado não encontrado!");

            if (simuladoDominio.Respostas != null && simuladoDominio.Respostas.Any())
                throw new Exception("Simulado já respondido!");

            int corretas = 0;
            simuladoDominio.Respostas = new List<RespostaSimulado>();

            foreach (var resposta in respostas)
            {
                var questao = simuladoDominio.Questoes
                    .FirstOrDefault(q => q.QuestaoId == resposta.QuestaoId);

                if (questao == null)
                    throw new Exception($"Questão {resposta.QuestaoId} não encontrada no simulado.");

                if (questao.Questao.AlternativaCorreta == resposta.Alternativa.Letra)
                    corretas++;

                simuladoDominio.Respostas.Add(resposta);
            }

            simuladoDominio.NotaFinal = corretas / simuladoDominio.Questoes.Count * 10;
            await _context.SaveChangesAsync();

            return simuladoDominio;
        }



        public async Task AtualizarSimuladoAsync(List<SimuladoQuestao> simuladoQuestoes)
        {
            _context.SimuladoQuestoes.AddRange(simuladoQuestoes);
            await _context.SaveChangesAsync();
        }
    }
}
