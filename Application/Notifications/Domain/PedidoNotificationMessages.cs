namespace Application.Notifications.Domain
{
    public class PedidoNotificationMessages
    {
        public Notification GetExistingRecordValidation()
        {
            return new Notification("ExistingRecordValidation", $"O pedido já existe!");
        }

        public Notification GetPedidoDoesNotExistValidation()
        {
            return new Notification("PedidoDoesNotExistValidation", $"O pedido não existe!");
        }

        public Notification GetPedidoChangedNumeroValidation()
        {
            return new Notification("PedidoChangedNumeroValidation", $"Não é possivel mudar o número do pedido!");
        }
    }
}