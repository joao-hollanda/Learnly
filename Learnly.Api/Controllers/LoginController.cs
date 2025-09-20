using System.Threading.Tasks;
using Learnly.Api.Models.Usuarios.Request;
using Learnly.Api.Models.Usuarios.Response;
using Learnly.Application.Interfaces;
using Learnly.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginAplicacao _loginAplicacao;

        public LoginController(ILoginAplicacao loginAplicacao)
        {
            _loginAplicacao = loginAplicacao;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login dto)
        {
            try
            {
                var teste = await _loginAplicacao.ValidarLogin(dto.Nome, dto.Senha);

                return Ok(teste);
                // return Ok(new { token = "jwt_token_aqui" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("refresh")]
        public IActionResult Refresh(string refreshToken)
        {
            // lógica de refresh token
            return Ok(new { token = "novo_jwt" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // lógica de logout
            return Ok();
        }
    }
}