using Domain.Entities.Validation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Domain.Entities
{
    public class Pedido : EntityValidation, IIdentityEntity
    {
        // EF Core utilizar para inserir os dados no banco através do Migration.
        public Pedido() { }
        public Pedido(int id, int numeroDoPedido, List<PedidoItem> pedidoItems)
        {
            PedidoItems = new List<PedidoItem>();
            Id = id;
            NumeroDoPedido = numeroDoPedido;
            
            if(pedidoItems != null && pedidoItems.Count > 0)
            {
                foreach (var item in pedidoItems)
                {
                    PedidoItems.Add(item);
                }
            }
            
            Validate(this, new PedidoValidation());
        }

        public int Id { get; set; }
        public int NumeroDoPedido { get; set; }        
        public List<PedidoItem> PedidoItems { get; set; }

        public int QuantidadeTotalItensPedido()
        {
            if (PedidoItems == null || PedidoItems.Count <= 0)
                return 0;

            return PedidoItems.Sum(x => x.Quantidade);
        }

        public double ValorTotalItensPedido()
        {
            if (PedidoItems == null || PedidoItems.Count <= 0)
                return 0;

            return PedidoItems.Sum(x => x.PrecoUnitario * x.Quantidade);
        }
    }
}