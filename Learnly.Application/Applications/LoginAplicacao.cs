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
        public async Task<bool> ValidarLogin(string nome, string senha)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterPorNome(nome);

            if (usuarioDominio == null)
                throw new Exception("Usuario não encontrado!");

            if (string.IsNullOrEmpty(senha))
                throw new Exception("Senha é obrigatória!");

            if (string.IsNullOrEmpty(nome))
                throw new Exception("Nome é obrigatório!");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuarioDominio.Senha))
                throw new Exception("Senha incorreta!");

            return true;
        }
    }
}