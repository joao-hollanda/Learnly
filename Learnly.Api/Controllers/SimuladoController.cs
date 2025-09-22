using System.Threading.Tasks;
using Learnly.Api.Models.Simulados.Request;
using Learnly.Api.Models.Usuarios.Request;
using Learnly.Api.Models.Usuarios.Response;
using Learnly.Application.Interfaces;
using Learnly.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimuladoController : ControllerBase
    {
        private readonly ISimuladoAplicacao _simuladoAplicacao;

        public SimuladoController(ISimuladoAplicacao simuladoAplicacao)
        {
            _simuladoAplicacao = simuladoAplicacao;
        }

        [HttpPost()]
        [Route("/{usuarioId}")]
        public async Task<IActionResult> CriarSimulado([FromRoute] int usuarioId, [FromBody] SimuladoRequest dto)
        {
            try
            {
                var simuladoDominio = new Simulado
                {
                    UsuarioId = usuarioId,
                    Data = DateTime.Now                  
                };

                var simuladoId = await _simuladoAplicacao.GerarSimulado(simuladoDominio, dto.Disciplinas, dto.QuantidadeQuestoes);

                return Ok(simuladoId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [Route("Corrigir/{simuladoId}")]

        public async Task<IActionResult> CorrigirSimulado([FromRoute] int simuladoId)
        {
            
        }
    }
}