using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IPedidoRepository : IDomainRepository<Pedido>
    {
        Task<IEnumerable<Pedido>> GetAllIncludingPedidoItemAsync();
        Task<Pedido> GetByIdIncludingPedidoItemkAsync(int id);
        Task<IEnumerable<Pedido>> GetAllByNumeroDoPedidokAsync(int numeroDoPedido);
        Task<Pedido> GetByNumeroDoPedidokAsync(int numeroDoPedido);
        Task<bool> GetExistByNumeroPedido(int numeroDoPedido);
    }
}