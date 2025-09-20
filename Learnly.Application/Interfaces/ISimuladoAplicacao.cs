using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnly.Application.Interfaces
{
    public interface ISimuladoAplicacao
    {
        Task<int> GerarSimulado(Simulado simulado, List<string> disciplinas, int totalQuestoes = 25);
    }
}