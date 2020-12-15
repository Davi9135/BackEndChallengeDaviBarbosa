using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class PedidoItemRepository : DomainRepository<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<PedidoItem>> GetAllIncludingPedidoAsync()
        {
            IQueryable<PedidoItem> query = await Task.FromResult(GenerateQuery(null,
                null,
                nameof(PedidoItem.Pedido)));
            
            return query.AsEnumerable();
        }

        public async Task<PedidoItem> GetByIdIncludingPedidoAsync(int id)
        {
            IQueryable<PedidoItem> query = await Task.FromResult(GenerateQuery(pedidoItem => pedidoItem.Id == id,
                                                                                null, 
                                                                                nameof(PedidoItem.Pedido)));

            return query.SingleOrDefault();
        }
    }
}