using FluentValidation;

namespace Domain.Entities.Validation
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(pedido => pedido.PedidoItems)
                .Must(item => item != null && item.Count > 0)
                .WithMessage("O item do pedido deve conter pelomenos 1 item!");

            RuleFor(x => x.NumeroDoPedido)
                .NotNull()
                .WithMessage("Número do pedido não pode ser vazio!")
                .GreaterThan(0)
                .WithMessage("Número do pedido deve ser maior que 0!");
        }
    }
}