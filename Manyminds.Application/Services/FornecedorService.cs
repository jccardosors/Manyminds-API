using AutoMapper;
using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Response.Fornecedor;
using Manyminds.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manyminds.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistroLogsService _registroLogsService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorService(ITokenService tokenService, IRegistroLogsService registroLogsService, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _tokenService = tokenService;
            _registroLogsService = registroLogsService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<RetornarLista_Response> RetornarLista()
        {
            var response = new RetornarLista_Response
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "FornecedorService", "RetornarLista");

                var lista = await _fornecedorRepository.RetornarTodos();
                response.Data = _mapper.Map<IEnumerable<FornecedorVM>>(lista);
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
                    _fornecedorRepository.Dispose();
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
