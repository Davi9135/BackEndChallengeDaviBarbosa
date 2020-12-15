using System.Collections.Generic;

namespace Api.Model
{
    public class StatusPedidoModel
    {
        public StatusPedidoModel(string pedido, List<string> status)
        {
            Pedido = pedido;
            Status = status;
        }

        public string Pedido { get; set; }
        public List<string> Status { get; set; }
    }
}