using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnly.Api.Models.Usuarios.Request
{
    public class Login
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}