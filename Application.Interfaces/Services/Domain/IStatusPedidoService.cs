using Application.Interfaces.Services.Standard;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IStatusPedidoService : IServiceBase<StatusPedido>
    {
        Task<List<string>> GetStatusAsync(StatusPedido entity);
    }
}