namespace Learnly.Api.Models.Usuarios.Request
{
    public class UsuarioAtualizarSenha
    {
        public int Id { get; set; }
        public string SenhaAntiga { get; set; }
        public string Senha { get; set; }
    }
}