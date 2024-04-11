using FluentValidation;
using Manyminds.Application.ViewModels.Request.Produto;

namespace Manyminds.Api.Validators
{
    public class ProdutoVMRequestValidator : AbstractValidator<ProdutoVMRequest>
    {
        public ProdutoVMRequestValidator()
        {
            RuleFor(p => p.Nome)
                .NotNull().WithMessage("Nome é obrigatório")
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(5).WithMessage("Nome deve ter no mínimo 5 caracteres")
                .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres");


            RuleFor(p => p.Valor)
                .NotNull().WithMessage("Valor é obrigatório")
                .NotEmpty().WithMessage("Valor é obrigatório")
                .GreaterThan(0).WithMessage("O produto deve ter um valor maior que 0.");

        }
    }
}
