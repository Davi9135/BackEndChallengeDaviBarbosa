using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Api.Model
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public int Pedido { get; set; }

        public List<PedidoItemModel> Itens { get; set; }

        // Aqui realiza o mapeamento da entidade de dominio "Pedido" para a view model.
        // Poderia utilizar o AutoMaper para realizar esse mapeamento.
        public static implicit operator PedidoModel(Pedido pedidoEntity)
        {
            var pedidoModel = new PedidoModel();

            if (pedidoEntity == null)
                return pedidoModel;

            pedidoModel.Id = pedidoEntity.Id;
            pedidoModel.Pedido = pedidoEntity.NumeroDoPedido;
            pedidoModel.Itens = new List<PedidoItemModel>();

            pedidoEntity.PedidoItems.ToList().ForEach(item =>
            {
                pedidoModel.Itens.Add(item);
            });

            return pedidoModel;
        }
    }
}
