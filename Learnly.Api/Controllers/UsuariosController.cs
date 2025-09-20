using System.Threading.Tasks;
using Learnly.Api.Models.Usuarios.Request;
using Learnly.Api.Models.Usuarios.Response;
using Learnly.Application.Interfaces;
using Learnly.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Learnly.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;

        public UsuariosController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> Criar([FromBody] UsuarioCriar usuarioCriar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Nome = usuarioCriar.Nome,
                    Email = usuarioCriar.Email,
                    Senha = usuarioCriar.Senha,
                    Cidade = usuarioCriar.Cidade
                };

                var usuarioId = await _usuarioAplicacao.Criar(usuarioDominio);

                return Ok(usuarioId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Obter/{usuarioId}")]
        public async Task<IActionResult> Obter([FromRoute] int usuarioId)
        {
            try
            {
                var usuarioDominio = await _usuarioAplicacao.Obter(usuarioId);

                var usuarioResposta = new UsuarioResponse()
                {
                    Id = usuarioDominio.Id,
                    Nome = usuarioDominio.Nome,
                    Email = usuarioDominio.Email,
                    Cidade = usuarioDominio.Cidade
                };

                return Ok(usuarioResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar([FromQuery] bool ativo)
        {
            try
            {
                var usuariosDominio = await _usuarioAplicacao.Listar(ativo);

                var usuarios = usuariosDominio.Select(u => new UsuarioResponse()
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Cidade = u.Cidade
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioAtualizar usuario)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Cidade = usuario.Cidade
                };

                await _usuarioAplicacao.Atualizar(usuarioDominio);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha([FromBody] UsuarioAtualizarSenha usuario)
        {
            try
            {
                await _usuarioAplicacao.AlterarSenha(usuario.Id, usuario.SenhaAntiga, usuario.Senha);

                return Ok("Senha alterada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("Reativar/{usuarioId}")]
        public async Task<IActionResult> Reativar([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.Reativar(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Desativar/{usuarioId}")]
        public async Task<IActionResult> Desativar([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.Desativar(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}