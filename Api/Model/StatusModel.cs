namespace Api.Model
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int ItensAprovados { get; set; }
        public double ValorAprovado { get; set; }
        public string Pedido { get; set; }
    }
}