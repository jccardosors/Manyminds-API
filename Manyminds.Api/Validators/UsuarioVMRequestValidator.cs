using FluentValidation;
using Manyminds.Application.ViewModels.Request.Usuario;

namespace Manyminds.Api.Validators
{
    public class UsuarioVMRequestValidator : AbstractValidator<UsuarioVMRequest>
    {
        public UsuarioVMRequestValidator()
        {
            RuleFor(p => p.Email)
               .NotNull().WithMessage("E-mail é obrigatório")
               .NotEmpty().WithMessage("E-mail é obrigatório")
               .MinimumLength(2).WithMessage("E-mail deve ter no mínimo 2 caracteres")
               .MaximumLength(100).WithMessage("E-mail deve ter no máximo 100 caracteres");


            RuleFor(p => p.Senha)
                .NotNull().WithMessage("Senha é obrigatório")
                .NotEmpty().WithMessage("Senha é obrigatório")
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres")
                .MaximumLength(50).WithMessage("Senha deve ter no máximo 50 caracteres");
        }
    }
}
