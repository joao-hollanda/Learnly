using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learnly.Domain.Entities;

namespace Learnly.Application.Interfaces
{
    public interface ILoginAplicacao
    {
        Task<Usuario> ValidarLogin(string nome, string senha);
    }
}