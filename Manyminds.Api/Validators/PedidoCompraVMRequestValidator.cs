using FluentValidation;
using Manyminds.Application.ViewModels.Request.PedidoCompra;

namespace Manyminds.Api.Validators
{
    public class PedidoCompraVMRequestValidator : AbstractValidator<PedidoCompraVMRequest>
    {
        public PedidoCompraVMRequestValidator()
        {
            RuleFor(p => p.Items).NotEmpty().WithMessage("Adicione ao menos um produto ao pedido");
        }
    }
}
