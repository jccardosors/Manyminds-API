using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Request.Produto;
using Manyminds.Application.ViewModels.Response.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private IProdutoService _produtoService;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpGet(Name = "RetornarTodosProdutos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaResponse>> RetornarTodosProdutos()
        {
            var response = await _produtoService.RetornarLista();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(Name = "RetornarProduto, {codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaResponse>> RetornarProduto(int codigo)
        {
            var response = await _produtoService.RetornarItem(codigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost(Name = "AdicionarProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaResponse>> AdicionarProduto(ProdutoVMRequest produto)
        {
            var response = await _produtoService.Adicionar(produto);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut(Name = "AlterarProduto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaResponse>> AlterarProduto(ProdutoVMRequest produto)
        {
            var response = await _produtoService.Alterar(produto);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(Name = "AtivarDesativarProduto, {codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaResponse>> AtivarDesativarProduto(int codigo)
        {
            var response = await _produtoService.AtivarDesativar(codigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
