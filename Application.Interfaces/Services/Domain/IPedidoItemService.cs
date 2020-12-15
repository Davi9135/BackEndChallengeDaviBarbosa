using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IPedidoItemService : IServiceBase<PedidoItem>
    {
        Task<IEnumerable<PedidoItem>> GetAllIncludingPedidoAsync();

        Task<PedidoItem> GetByIdIncludingPedidoAsync(int id);

        
    }
}