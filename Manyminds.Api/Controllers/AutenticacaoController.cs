using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Application.ViewModels.Response.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ILogger<AutenticacaoController> _logger;
        private readonly IUsuarioService _tokenService;

        public AutenticacaoController(ILogger<AutenticacaoController> logger, IUsuarioService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost(Name = "Autenticar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Autenticar(LoginRequest loginRequest)
        {
            var response = await _tokenService.LogarUsuario(loginRequest);
            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
