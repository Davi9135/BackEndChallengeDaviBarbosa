using Domain.Entities.Validation;

namespace Domain.Entities
{
    public class PedidoItem : EntityValidation, IIdentityEntity
    {
        // EF Core utilizar para inserir os dados no banco através do Migration.
        public PedidoItem() { }
        public PedidoItem(int id, int quantidade, int pedidoId, string descricao, double precoUnitario)
        {
            Id = id;
            Quantidade = quantidade;
            PedidoId = pedidoId;
            Descricao = descricao;
            PrecoUnitario = precoUnitario;
            Validate(this, new PedidoItemValidation());
        }


        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int PedidoId { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        
        public virtual Pedido Pedido { get; set; }
    }
}