namespace Application.Notifications.Domain
{
    public class PedidoItemNotificationMessages
    {
        public Notification GetExistingRecordValidation(int id)
        {
            return new Notification("PedidoItemNotFound", $"O item do pedido {id} não foi encontrado no sistema!");
        }
    }
}