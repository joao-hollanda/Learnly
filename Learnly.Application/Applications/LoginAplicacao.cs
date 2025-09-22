using Learnly.Application.Interfaces;
using Learnly.Repository.Interfaces;

namespace Learnly.Application.Applications
{
    public class LoginAplicacao : ILoginAplicacao
    {
        readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public async Task<bool> ValidarLogin(string email, string senha)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorEmail(email);
            
            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado!");

            if (string.IsNullOrEmpty(senha))
                throw new Exception("Senha é obrigatória!");

            if (string.IsNullOrEmpty(email))
                throw new Exception("Email é obrigatório!");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuarioDominio.Senha))
                throw new Exception("Senha incorreta!");

            return true;
        }
    }
}