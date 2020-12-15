using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IPedidoItemRepository : IDomainRepository<PedidoItem>
    {
        Task<IEnumerable<PedidoItem>> GetAllIncludingPedidoAsync();
        Task<PedidoItem> GetByIdIncludingPedidoAsync(int id);
    }
}