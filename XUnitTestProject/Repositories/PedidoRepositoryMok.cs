using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XUnitTestProject.Mok.Repositories
{
    public class PedidoRepositoryMok : IPedidoRepository
    {
        public Task<Pedido> AddAsync(Pedido entity)
        {
            entity.Id = 1;
            var itemId = 1;

            entity.PedidoItems.ForEach(item =>
            {
                item.Id = itemId;
                itemId++;
            });

            return Task.FromResult(entity);
        }

        public Task<int> AddRangeAsync(IEnumerable<Pedido> entities)
        {
            var idItem = 1;

            foreach (var item in entities)
            {
                item.Id = idItem;
                idItem++;
            }

            return Task.FromResult(entities.Count());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pedido>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pedido>> GetAllByNumeroDoPedidokAsync(int numeroDoPedido)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Pedido>> GetAllIncludingPedidoItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> GetByIdIncludingPedidoItemkAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> GetByNumeroDoPedidokAsync(int numeroDoPedido)
        {
            var itemList = new List<PedidoItem>();
            itemList.Add(new PedidoItem(1, 1, 1, "Item A", 10));
            itemList.Add(new PedidoItem(2, 2, 1, "Item B", 5));
            var pedido = new Pedido(1, 123456, itemList);

            return Task.FromResult(pedido);
        }

        public Task<bool> GetExistByNumeroPedido(int numeroDoPedido)
        {
            var ids = new int[10, 20, 21, 15];

            foreach (var item in ids)
            {
                if (item == numeroDoPedido)
                    Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> GetExistRecord(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveRangeAsync(IEnumerable<Pedido> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IEnumerable<Pedido> entities)
        {
            throw new NotImplementedException();
        }
    }
}
