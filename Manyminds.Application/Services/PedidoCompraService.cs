using AutoMapper;
using Manyminds.Application.Interfaces;
using Manyminds.Application.Utils;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Request.PedidoCompra;
using Manyminds.Application.ViewModels.Response.PedidoCompra;
using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using System.Net;

namespace Manyminds.Application.Services
{
    public class PedidoCompraService : IPedidoCompraService
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistroLogsService _registroLogsService;
        private readonly IPedidoCompraRepository _pedidoCompraRepository;
        private readonly IPedidoCompraItemRepository _pedidoCompraItemRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public PedidoCompraService(ITokenService tokenService, IRegistroLogsService registroLogsService, IPedidoCompraRepository pedidoCompraRepository, IPedidoCompraItemRepository pedidoCompraItemRepository, IMapper mapper, IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository)
        {
            _tokenService = tokenService;
            _registroLogsService = registroLogsService;
            _pedidoCompraRepository = pedidoCompraRepository;
            _pedidoCompraItemRepository = pedidoCompraItemRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<PedidoCompraResponse> Adicionar(PedidoCompraVMRequest pedidoCompraVMRequest)
        {
            var response = new PedidoCompraResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "Adicionar");

                var pedidoCompraMapper = _mapper.Map<PedidoCompra>(pedidoCompraVMRequest.Pedido);
                pedidoCompraMapper.Data = DateTime.Now;
                pedidoCompraMapper.Status = (int)PedidoStatusEnum.Ativo;

                var pedidoCompra = await _pedidoCompraRepository.Adicionar(pedidoCompraMapper);
                if (pedidoCompra.Codigo > 0)
                {
                    var pedidoCompraItens = _mapper.Map<IEnumerable<PedidoCompraItem>>(pedidoCompraVMRequest.Items);

                    //inclui os itens que estao chegando do front end
                    response = await AdicionarItens(pedidoCompraItens, pedidoCompra, response);
                    if (response.Success)
                    {
                        response.Message = "Pedido de compra incluído com sucesso.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<PedidoCompraResponse> Alterar(PedidoCompraVMRequest pedidoCompraVMRequest)
        {
            var response = new PedidoCompraResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "Alterar");

                var pedidoCompra = await _pedidoCompraRepository.RetornarItem(pedidoCompraVMRequest.Pedido.Codigo);
                if (pedidoCompra.Status == (int)PedidoStatusEnum.Finalizado)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Pedido não pode ser modificado, pois esta com o status 'Finalizado'.";

                    return response;
                }

                pedidoCompra.Status = pedidoCompraVMRequest.Pedido.Status;
                pedidoCompra.Data = DateTime.Now;
                pedidoCompra.Observacao = pedidoCompraVMRequest.Pedido.Observacao;

                var pedidoCompraItens = _mapper.Map<IEnumerable<PedidoCompraItem>>(pedidoCompraVMRequest.Items);
                var pedidoCompraItensExclusaoList = await _pedidoCompraItemRepository.RetornarItensPorCodigoPedidoCompra(pedidoCompra.Codigo);

                //exclui os itens existentes
                foreach (var item in pedidoCompraItensExclusaoList)
                {
                    await _pedidoCompraItemRepository.Excluir(item.Codigo);
                }

                //inclui os itens que estao chegando do front end
                response = await AdicionarItens(pedidoCompraItens, pedidoCompra, response);

            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<PedidoCompraResponse> Excluir(int codigo)
        {
            var response = new PedidoCompraResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "Excluir");

                var pedidoCompra = await _pedidoCompraRepository.RetornarItem(codigo);
                if (pedidoCompra.Status == (int)PedidoStatusEnum.Finalizado)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Pedido não pode ser excluído, pois esta com o status 'Finalizado'.";
                }

                var pedidoComprasItens = await _pedidoCompraItemRepository.RetornarItensPorCodigoPedidoCompra(codigo);
                foreach (var item in pedidoComprasItens)
                {
                    await _pedidoCompraItemRepository.Excluir(item.Codigo);
                }

                await _pedidoCompraRepository.Excluir(codigo);
                response.Message = "Pedido excluído com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<PedidoCompraResponse> RetornarItem(int codigo)
        {
            var response = new PedidoCompraResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "RetornarItem");

                var pedidoCompra = await _pedidoCompraRepository.RetornarItem(codigo);
                if (pedidoCompra is null)
                {
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Message = "Pedido não encontrado.";

                    return response;
                }

                var pedidoCompraMapper = _mapper.Map<PedidoCompraVM>(pedidoCompra);

                var fornecedor = await _fornecedorRepository.RetornarItem(pedidoCompraMapper.FornecedorCodigo);
                pedidoCompraMapper.FornecedorNome = fornecedor.Nome;
                response.Pedido = pedidoCompraMapper;

                var pedidoCompraItens = await _pedidoCompraItemRepository.RetornarItensPorCodigoPedidoCompra(codigo);
                var pedidoCompraItensMapper = _mapper.Map<IEnumerable<PedidoCompraItemVM>>(pedidoCompraItens);

                foreach (var item in pedidoCompraItensMapper)
                {
                    var produto = await _produtoRepository.RetornarItem(item.ProdutoCodigo);
                    item.NomeProduto = produto.Nome;
                    item.ValorProduto = produto.Valor;
                }

                response.Items = pedidoCompraItensMapper;
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<RetornarListaPedidoCompraResponse> RetornarLista()
        {
            var response = new RetornarListaPedidoCompraResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "RetornarLista");

                var itens = new List<RetornarListaPedidoCompraItemResponse>();
                var pedidoComprasList = await _pedidoCompraRepository.RetornarTodos();

                foreach (var item in pedidoComprasList)
                {
                    var pedidoCompraMapper = _mapper.Map<PedidoCompraVM>(item);
                    var fornecedor = await _fornecedorRepository.RetornarItem(pedidoCompraMapper.FornecedorCodigo);
                    pedidoCompraMapper.FornecedorNome = fornecedor.Nome;

                    var pedidoCompraItens = await _pedidoCompraItemRepository.RetornarItensPorCodigoPedidoCompra(item.Codigo);
                    var pedidoCompraItensMapper = _mapper.Map<IEnumerable<PedidoCompraItemVM>>(pedidoCompraItens);

                    itens.Add(new RetornarListaPedidoCompraItemResponse
                    {
                        Pedido = pedidoCompraMapper,
                        Items = pedidoCompraItensMapper
                    });
                }

                foreach (var pedido in itens)
                {
                    foreach (var item in pedido.Items)
                    {
                        var produto = await _produtoRepository.RetornarItem(item.ProdutoCodigo);
                        item.NomeProduto = produto.Nome;
                        item.ValorProduto = produto.Valor;
                    }
                }

                response.Data = itens;
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<RetornarListaPedidosResponse> RetornarPedidosLista()
        {
            var response = new RetornarListaPedidosResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "PedidoCompraService", "RetornarLista");

                var pedidoComprasList = await _pedidoCompraRepository.RetornarTodos();
                var listaMapper = _mapper.Map<IEnumerable<PedidoCompraVM>>(pedidoComprasList);
                foreach (var item in listaMapper)
                {
                    item.Observacao = item.Observacao.Length > 12 ? $"{item.Observacao.Substring(0, 11)}..." : item.Observacao;
                    var fornecedorNome = await _fornecedorRepository.RetornarItem(item.FornecedorCodigo);
                    item.FornecedorNome = fornecedorNome.Nome.Length > 10 ? $"{fornecedorNome.Nome.Substring(0, 9)}..." : fornecedorNome.Nome;
                }

                response.Data = listaMapper;
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }


        private async Task<PedidoCompraResponse> AdicionarItens(IEnumerable<PedidoCompraItem> pedidoCompraItens, PedidoCompra pedidoCompra, PedidoCompraResponse response)
        {
            decimal total = 0;
            var items = new List<PedidoCompraItemVM>();

            foreach (var item in pedidoCompraItens)
            {
                item.PedidoCompraCodigo = pedidoCompra.Codigo;

                var pedidoCompraItemMapper = _mapper.Map<PedidoCompraItem>(item);
                var pedidoCompraItem = await _pedidoCompraItemRepository.Adicionar(pedidoCompraItemMapper);

                items.Add(_mapper.Map<PedidoCompraItemVM>(pedidoCompraItem));

                var produto = await _produtoRepository.RetornarItem(pedidoCompraItem.ProdutoCodigo);
                total += item.Quantidade * produto.Valor;
            }

            pedidoCompra.ValorTotal = total;
            await _pedidoCompraRepository.Alterar(pedidoCompra);

            response.Pedido = _mapper.Map<PedidoCompraVM>(pedidoCompra);
            response.Items = items;

            return response;
        }

        #region Disposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _pedidoCompraRepository.Dispose();
                    _pedidoCompraItemRepository.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
