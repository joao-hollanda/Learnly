using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnly.Services.Interfaces
{
    public interface IIAService
    {
        Task<string> GerarFeedbackAsync(Simulado simulado);
    }
}