using AutoMapper;
using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Request.Produto;
using Manyminds.Application.ViewModels.Response.Produto;
using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using System.Net;

namespace Manyminds.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistroLogsService _registroLogsService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(ITokenService tokenService, IRegistroLogsService registroLogsService, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _tokenService = tokenService;
            _registroLogsService = registroLogsService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoResponse> Adicionar(ProdutoVMRequest produtoVMRequest)
        {
            var response = new ProdutoResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "Adicionar");

                var produtoMapper = _mapper.Map<Produto>(produtoVMRequest);
                produtoMapper.Ativo = true;

                var produtoSalvo = await _produtoRepository.Adicionar(produtoMapper);
                response.Data = _mapper.Map<ProdutoVM>(produtoMapper);
                response.Message = "Produto cadastrado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<ProdutoResponse> Alterar(ProdutoVMRequest produtoVMRequest)
        {
            var response = new ProdutoResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "Alterar");

                var produto = await _produtoRepository.RetornarItem(produtoVMRequest.Codigo);
                if (produto is null)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Produto não encontrado.";
                    return response;
                }

                if (!produto.Ativo)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Produto desativado não pode ser alterado.";
                    return response;
                }

                produto.Ativo = produtoVMRequest.Ativo;
                produto.Valor = produtoVMRequest.Valor;
                produto.FornecedorCodigo = produtoVMRequest.FornecedorCodigo;
                produto.Nome = produtoVMRequest.Nome;

                var produtoAlterado = await _produtoRepository.Alterar(produto);
                response.Data = _mapper.Map<ProdutoVM>(produtoAlterado);
                response.Message = "Produto alterado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<ProdutoResponse> AtivarDesativar(int codigo)
        {
            var response = new ProdutoResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "AtivarDesativar");

                var produto = await _produtoRepository.AtivarDesativar(codigo);
                response.Data = _mapper.Map<ProdutoVM>(produto);
                response.Message = produto.Ativo ? "Produto ativado com sucesso!" : "Produto desativado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<ProdutoResponse> RetornarItem(int codigo)
        {
            var response = new ProdutoResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "RetornarItem");

                var produto = await _produtoRepository.RetornarItem(codigo);
                response.Data = _mapper.Map<ProdutoVM>(produto);
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<RetornarListaResponse> RetornarLista()
        {
            var response = new RetornarListaResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "RetornarLista");

                var lista = await _produtoRepository.RetornarTodos();
                response.Data = _mapper.Map<IEnumerable<ProdutoVM>>(lista);
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

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
                    _produtoRepository.Dispose();
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
