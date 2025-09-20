using System.Text;
using Microsoft.EntityFrameworkCore;
using Learnly.Domain.Entities.Simulados;
using Learnly.Repository.Interfaces;

namespace Learnly.Repository.Repositories
{
    public class SimuladoRepositorio : ISimuladoRepositorio
    {
        private readonly LearnlyContexto _context;

        public SimuladoRepositorio(LearnlyContexto context)
        {
            _context = context;
        }

        public async Task<int> GerarSimulado(Simulado simulado, List<string> disciplinas, int totalQuestoes = 25)
        {
            if (simulado == null)
                throw new ArgumentException("Simulado não pode ser nulo.");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            _context.Simulados.Add(simulado);
            await _context.SaveChangesAsync();

            var questoes = await GerarQuestoesAsync(disciplinas, totalQuestoes);

            var simuladoQuestoes = questoes.Select(q => new SimuladoQuestao
            {
                SimuladoId = simulado.SimuladoId,
                QuestaoId = q.QuestaoId
            }).ToList();

            _context.SimuladoQuestoes.AddRange(simuladoQuestoes);
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


        public async Task AtualizarSimuladoAsync(List<SimuladoQuestao> simuladoQuestoes)
        {
            _context.SimuladoQuestoes.AddRange(simuladoQuestoes);
            await _context.SaveChangesAsync();
        }
    }
}
