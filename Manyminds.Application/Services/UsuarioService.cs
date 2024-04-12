using AutoMapper;
using Manyminds.Application.Interfaces;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Application.ViewModels.Response;
using Manyminds.Application.ViewModels.Response.Usuario;
using Manyminds.Domain.Entities;
using Manyminds.Domain.Interfaces;
using System.Net;

namespace Manyminds.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IRegistroLogsService _registroLogsService;
        private readonly IUsuarioControleAcessoRepository _usuarioControleAcessoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokenService, IRegistroLogsService registroLogsService, IUsuarioControleAcessoRepository usuarioControleAcessoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _registroLogsService = registroLogsService;
            _usuarioControleAcessoRepository = usuarioControleAcessoRepository;
        }

        public async Task<LoginResponse> LogarUsuario(LoginRequest loginRequest)
        {
            var response = new LoginResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(loginRequest.Email, "UsuarioService", "LogarUsuario");

                var usuario = await _usuarioRepository.RetornarItem(loginRequest.Email.Trim(), loginRequest.Senha.Trim());
                if (usuario == null)
                {
                    await this.RegistrarTentativaAcesso(loginRequest.Email);
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuário não autenticado.";

                    return response;
                }

                var usuarioDesbloqueado = await _usuarioRepository.RetornarUsuarioDesbloqueado(usuario.Email.Trim());
                if (!usuarioDesbloqueado)
                {
                    usuarioDesbloqueado = await this.VerificarBloqueioTemporario(usuario);
                    if (usuarioDesbloqueado)
                    {
                        response.Status = (int)HttpStatusCode.NotFound;
                        response.Message = "Usuário bloqueado, tente novamente após 5 minutos.";
                        return response;
                    }
                }

                var token = await _tokenService.GerarToken(usuario.Email);
                response.Data = new LoginDataVM
                {
                    Email = usuario.Email,
                    UsuarioId = usuario.Codigo,
                    Token = token
                };

            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<RetornarTodosResponse> RetornarTodos()
        {
            var response = new RetornarTodosResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "UsuarioService", "RetornarTodosUsuarios");

                var responseRepo = await _usuarioRepository.RetornarLista();
                var responseMapper = _mapper.Map<IEnumerable<UsuarioVM>>(responseRepo);
                response.Data = responseMapper;
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<UsuarioResponse> Adicionar(UsuarioVMRequest usuarioVMRequest)
        {
            var response = new UsuarioResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(usuarioVMRequest.Email, "UsuarioService", "Adicionar");

                var usuario = await _usuarioRepository.RetornarItem(usuarioVMRequest.Email);
                if (usuario is not null)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Já existe usuário com o mesmo e-amil.";
                    return response;
                }

                var usuarioMapper = _mapper.Map<Usuario>(usuarioVMRequest);
                var usuarioSalvo = await _usuarioRepository.Adicionar(usuarioMapper);
                response.Data = _mapper.Map<UsuarioVM>(usuarioSalvo);
                response.Message = "Usuário salvo com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<LoginResponse> Alterar(UsuarioVMRequest usuarioVMRequest)
        {
            var response = new LoginResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                var emailUsuarioAutenticado = await _tokenService.RetornarEmailTokenClaims();
                await _registroLogsService.RegistrarLogs(emailUsuarioAutenticado, "UsuarioService", "Alterar");

                var usuario = await _usuarioRepository.RetornarItem(usuarioVMRequest.Codigo);
                if (usuario is null)
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Usuário não encontrado.";
                    return response;
                }

                if (!emailUsuarioAutenticado.Trim().Equals(usuario.Email.Trim()))
                {
                    response.Status = (int)HttpStatusCode.NotModified;
                    response.Message = "Não permitido alterar e-mail de outros usuarios.";
                    return response;
                }

                usuario.Email = usuarioVMRequest.Email;
                usuario.Senha = usuarioVMRequest.Senha;

                var usuarioAlterado = await _usuarioRepository.Alterar(usuario);    
                var token = await _tokenService.GerarToken(usuario.Email);

                response.Data = new LoginDataVM
                {
                    Email = usuarioAlterado.Email,
                    UsuarioId = usuarioAlterado.Codigo,
                    Token = token
                };
                response.Message = "Usuário alterado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<UsuarioResponse> RetornarItem(int codigo)
        {
            var response = new UsuarioResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "UsuarioService", "RetornarItem");

                var usuario = await _usuarioRepository.RetornarItem(codigo);
                response.Data = _mapper.Map<UsuarioVM>(usuario);
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        public async Task<UsuarioResponse> Excluir(int codigo)
        {
            var response = new UsuarioResponse
            {
                Status = (int)HttpStatusCode.OK
            };

            try
            {
                await _registroLogsService.RegistrarLogs(await _tokenService.RetornarEmailTokenClaims(), "ProdutoService", "Excluir");

                await _usuarioRepository.Excluir(codigo);
                response.Message = "Usuário excluido com sucesso.";
            }
            catch (Exception ex)
            {
                response.Status = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.GetBaseException().Message;
            }

            return response;
        }

        private async Task RegistrarTentativaAcesso(string email)
        {
            var usuarioControle = await _usuarioControleAcessoRepository.RetornarItem(email);
            if (usuarioControle is null)
            {
                await _usuarioControleAcessoRepository.Adicionar(new UsuarioControleAcesso
                {
                    Bloqueado = false,
                    NumeroTentativa = 1,
                    UltimoAcesso = DateTime.Now,
                    UsuarioEmail = email
                });
            }
            else if (!usuarioControle.Bloqueado)
            {
                usuarioControle.NumeroTentativa++;
                usuarioControle.Bloqueado = usuarioControle.NumeroTentativa == 3 ? true : false;

                await _usuarioControleAcessoRepository.Alterar(usuarioControle);
            }
        }

        private async Task<bool> VerificarBloqueioTemporario(Usuario usuario)
        {
            var usuarioControle = await _usuarioControleAcessoRepository.RetornarItem(usuario.Email);
            var tempoDecorrido = DateTime.Now.Minute - usuarioControle.UltimoAcesso.Minute;
            if (tempoDecorrido > 5)
            {
                usuarioControle.NumeroTentativa = 0;
                usuarioControle.UltimoAcesso = DateTime.Now;
                usuarioControle.Bloqueado = false;

                await _usuarioControleAcessoRepository.Alterar(usuarioControle);

                return false;
            }

            return true;
        }

        #region Disposable

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_usuarioRepository.Dispose();
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
