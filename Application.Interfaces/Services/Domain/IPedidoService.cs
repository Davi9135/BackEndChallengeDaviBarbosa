using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IPedidoService : IServiceBase<Pedido>
    {
        Task<IEnumerable<Pedido>> GetAllIncludingPedidoItemAsync();

        Task<Pedido> GetByIdIncludingPedidoItemAsync(int id);
    }
}