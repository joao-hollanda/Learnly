using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnly.Application.Interfaces
{
    public interface ILoginAplicacao
    {
        Task<bool> ValidarLogin(string nome, string senha);
    }
}