using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Application.ViewModels.Response;
using Manyminds.Application.ViewModels.Response.PedidoCompra;
using Manyminds.Application.ViewModels.Response.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet(Name = "RetornarTodosUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarTodosResponse>> RetornarTodosUsuarios()
        {
            var response = await _usuarioService.RetornarTodos();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(Name = "RetornarUsuario, {codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponse>> RetornarUsuario(int codigo)
        {
            var response = await _usuarioService.RetornarItem(codigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost(Name = "AdicionarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioResponse>> AdicionarUsuario(UsuarioVMRequest usuario)
        {
            var response = await _usuarioService.Adicionar(usuario);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut(Name = "AlterarUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> AlterarUsuario(UsuarioVMRequest usuario)
        {
            var response = await _usuarioService.Alterar(usuario);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete(Name = "ExcluirUsuario, {codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoCompraResponse>> ExcluirUsuario(int codigo)
        {
            var response = await _usuarioService.Excluir(codigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
