using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels.Request.PedidoCompra;
using Manyminds.Application.ViewModels.Response.PedidoCompra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manyminds.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PedidoCompraController : ControllerBase
    {
        private readonly ILogger<PedidoCompraController> _logger;
        private IPedidoCompraService _pedidoCompraService;

        public PedidoCompraController(ILogger<PedidoCompraController> logger, IPedidoCompraService pedidoCompraService)
        {
            _logger = logger;
            _pedidoCompraService = pedidoCompraService;
        }

        [HttpGet(Name = "RetornarTodosPedidosCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaPedidoCompraResponse>> RetornarTodosPedidosCompra()
        {
            var response = await _pedidoCompraService.RetornarLista();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(Name = "RetornarPedidosLista")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RetornarListaPedidosResponse>> RetornarPedidosLista()
        {
            var response = await _pedidoCompraService.RetornarPedidosLista();

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet(Name = "RetornarPedidoCompra, {codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoCompraResponse>> RetornarPedidoCompra(int codigo)
        {
            var response = await _pedidoCompraService.RetornarItem(codigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost(Name = "AdicionarPedidoCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoCompraResponse>> AdicionarPedidoCompra(PedidoCompraVMRequest pedidoCompra)
        {
            var response = await _pedidoCompraService.Adicionar(pedidoCompra);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut(Name = "AlterarPedidoCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoCompraResponse>> AlterarPedidoCompra(PedidoCompraVMRequest pedidoCompra)
        {
            var response = await _pedidoCompraService.Alterar(pedidoCompra);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete(Name = "ExcluirPedidoCompra, {pedidoCompracodigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoCompraResponse>> ExcluirPedidoCompra(int pedidoCompracodigo)
        {
            var response = await _pedidoCompraService.Excluir(pedidoCompracodigo);

            if (!response.Success)
            {
                _logger.Log(LogLevel.Error, response.Message);
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
