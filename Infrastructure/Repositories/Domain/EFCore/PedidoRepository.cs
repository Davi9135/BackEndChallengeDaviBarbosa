using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class PedidoRepository : DomainRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Pedido>> GetAllIncludingPedidoItemAsync()
        {
            IQueryable<Pedido> query = await Task.FromResult(GenerateQuery(null, null, nameof(Pedido.PedidoItems)));

            return query.AsEnumerable();
        }

        public async Task<Pedido> GetByIdIncludingPedidoItemkAsync(int id)
        {
            IQueryable<Pedido> query = await Task.FromResult(GenerateQuery(pedido => pedido.Id == id, null, nameof(Pedido.PedidoItems)));

            return query.SingleOrDefault();
        }

        public async Task<IEnumerable<Pedido>> GetAllByNumeroDoPedidokAsync(int numeroDoPedido)
        {            
            IQueryable<Pedido> query = await Task.FromResult(GenerateQuery(pedido => pedido.NumeroDoPedido == numeroDoPedido, null, nameof(Pedido.PedidoItems)));
            
            return query.AsEnumerable();
        }

        public async Task<Pedido> GetByNumeroDoPedidokAsync(int numeroDoPedido)
        {
            Pedido query = await Task.FromResult(GenerateQuery(pedido => pedido.NumeroDoPedido == numeroDoPedido, null, nameof(Pedido.PedidoItems)).FirstOrDefault());

            return query;
        }

        public async Task<bool> GetExistByNumeroPedido(int numeroDoPedido)
        {
            bool query = await Task.FromResult(GenerateQuery(pedido => pedido.NumeroDoPedido == numeroDoPedido, null, nameof(Pedido.PedidoItems)).Any());

            return query;
        }
    }
}