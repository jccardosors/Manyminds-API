using AutoMapper;
using Manyminds.Application.ViewModels;
using Manyminds.Application.ViewModels.Request.PedidoCompra;
using Manyminds.Application.ViewModels.Request.Produto;
using Manyminds.Application.ViewModels.Request.Usuario;
using Manyminds.Domain.Entities;

namespace Manyminds.Application.AutoMappers
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Fornecedor, FornecedorVM>().ReverseMap();
            CreateMap<PedidoCompra, PedidoCompraVM>().ReverseMap();
            CreateMap<PedidoCompraItem, PedidoCompraItemVM>().ReverseMap();
            CreateMap<Produto, ProdutoVM>().ReverseMap();
            CreateMap<RegistroLogs, RegistroLogsVM>().ReverseMap();
            CreateMap<Usuario, UsuarioVM>().ReverseMap();
            CreateMap<UsuarioControleAcesso, UsuarioControleAcessoVM>().ReverseMap();

            CreateMap<Usuario, LoginRequest>().ReverseMap();
            CreateMap<PedidoCompraVM, PedidoCompraVMRequest>().ReverseMap();
            CreateMap<Produto, ProdutoVMRequest>().ReverseMap();
            CreateMap<Usuario, UsuarioVMRequest>().ReverseMap();
        }
    }
}
