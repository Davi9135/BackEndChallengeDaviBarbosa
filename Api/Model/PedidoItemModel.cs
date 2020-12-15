using Domain.Entities;

namespace Api.Model
{
    public class PedidoItemModel 
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public int Qtd { get; set; }


        // Aqui realiza o mapeamento da entidade de dominio "PedidoItem" para a view model.
        // Poderia utilizar o AutoMaper para realizar esse mapeamento.
        public static implicit operator PedidoItemModel(PedidoItem pedidoItemEntity)
        {
            return new PedidoItemModel()
            {
                Id = pedidoItemEntity.Id,
                Descricao = pedidoItemEntity.Descricao,
                PrecoUnitario = pedidoItemEntity.PrecoUnitario,
                Qtd = pedidoItemEntity.Quantidade
            };
        }
    }
}