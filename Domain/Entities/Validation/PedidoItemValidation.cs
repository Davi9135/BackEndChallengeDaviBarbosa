using FluentValidation;

namespace Domain.Entities.Validation
{
    public class PedidoItemValidation : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("Deve informar a descrição do item!");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade do item deve ser maior que 1!");

            RuleFor(x => x.PrecoUnitario)
                .GreaterThan(0)
                .WithMessage("O preço unitário do item deve ser maior que 0!");
        }
    }
}