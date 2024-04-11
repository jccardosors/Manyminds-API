using AutoMapper;
using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Response.RegistroLogs;
using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using System.Net;

namespace Manyminds.Application.Services
{
    public class RegistroLogsService : IRegistroLogsService
    {
        private readonly IRegistroLogsRepository _registroLogsRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public RegistroLogsService(IRegistroLogsRepository registroLogsRepository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _registroLogsRepository = registroLogsRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<RetornarListaLogsResponse> RetornarLista()
        {
            var response = new RetornarListaLogsResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                var lista = await _registroLogsRepository.RetornarTodos();
                response.Data = _mapper.Map<IEnumerable<RegistroLogsVM>>(lista);
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task RegistrarLogs(string email, string classe, string metodo)
        {
            try
            {
                var usuario = await _usuarioRepository.RetornarItem(email.Trim());
                if (usuario is not null)
                {
                    var registroLog = new RegistroLogs
                    {
                        Data = DateTime.Now,
                        Descricao = $"Usuário {email}, metodo {metodo}, classe {classe}",
                        UsuarioCodigo = usuario.Codigo
                    };

                    var registroResponse = await _registroLogsRepository.Adicionar(registroLog);
                }
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        #region Disposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Dispose();
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
