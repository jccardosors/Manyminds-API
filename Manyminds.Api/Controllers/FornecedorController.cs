using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Response.Fornecedor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FornecedorController : ControllerBase
    {
        private readonly ILogger<FornecedorController> _logger;
        private IFornecedorService _fornecedorService;

        public FornecedorController(ILogger<FornecedorController> logger, IFornecedorService fornecedorService)
        {
            _logger = logger;
            _fornecedorService = fornecedorService;
        }

        [HttpGet(Name = "RetornarTodosFornecedores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarLista_Response>> RetornarTodosFornecedores()
        {
            var response = await _fornecedorService.RetornarLista();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
