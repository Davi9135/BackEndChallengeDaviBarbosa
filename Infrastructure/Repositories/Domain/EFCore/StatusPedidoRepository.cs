using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class StatusPedidoRepository : DomainRepository<StatusPedido>, IStatusPedidoRepository
    {
        public StatusPedidoRepository(ApplicationContext dbContext) : base(dbContext) { }
    }
}