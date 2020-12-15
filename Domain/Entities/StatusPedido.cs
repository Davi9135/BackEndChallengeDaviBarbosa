namespace Domain.Entities
{
    public class StatusPedido: EntityValidation, IIdentityEntity
    {
        // EF Core utilizar para inserir os dados no banco através do Migration.
        public StatusPedido() { }

        public StatusPedido(int id, string status, int itensAprovados, double valorAprovado, int pedidoId)
        {
            Id = id;
            Status = status;
            ItensAprovados = itensAprovados;
            ValorAprovado = valorAprovado;
            PedidoId = pedidoId;
        }

        public int Id { get; set; }
        public string Status { get; set; }
        public int ItensAprovados { get; set; }
        public double ValorAprovado { get; set; }
        public int PedidoId { get; set; }
    }
}