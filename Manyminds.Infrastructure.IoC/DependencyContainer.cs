using Manyminds.Application.Interfaces;
using Manyminds.Application.Services;
using Manyminds.Domain.Interfaces;
using Manyminds.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Manyminds.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //application
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRegistroLogsService, RegistroLogsService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IPedidoCompraService, PedidoCompraService>();

            //domain and Infra.data
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IPedidoCompraRepository, PedidoCompraRepository>();
            services.AddScoped<IPedidoCompraItemRepository, PedidoCompraItemRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IRegistroLogsRepository, RegistroLogsRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioControleAcessoRepository, UsuarioControleAcessoRepository>();

        }
    }
}
