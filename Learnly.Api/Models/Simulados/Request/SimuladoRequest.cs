using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnly.Api.Models.Simulados.Request
{
    public class SimuladoRequest
    {
        public List<string> Disciplinas { get; set; }
        public int QuantidadeQuestoes { get; set; }
    }
}