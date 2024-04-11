using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Response.RegistroLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegistroLogsController : ControllerBase
    {
        private readonly ILogger<RegistroLogsController> _logger;
        private IRegistroLogsService _registroLogsService;

        public RegistroLogsController(ILogger<RegistroLogsController> logger, IRegistroLogsService registroLogsService)
        {
            _logger = logger;
            _registroLogsService = registroLogsService;
        }

        [HttpGet(Name = "RetornarTodosLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaLogsResponse>> RetornarTodosLogs()
        {
            var response = await _registroLogsService.RetornarLista();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
