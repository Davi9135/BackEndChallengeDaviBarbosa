using Application.Notifications;
using Application.Notifications.Domain;
using Application.Services.Domain;
using Domain.Entities;
using System.Collections.Generic;
using Xunit;
using XUnitTestProject.Mok.Repositories;
using System.Linq;

namespace XUnitTestProject
{
    public class PedidoTest
    {
        [Fact]
        public void AddPedidoSemNenhumItem()
        {
            var pedidoService = new PedidoService(new PedidoRepositoryMok(),
                                        new NotificationContext(),
                                        new PedidoNotificationMessages());
            var pedido = new Pedido(0, 1414, null);
            var result = pedidoService.AddAsync(pedido).Result;

            Assert.True(result.Id == 0, "Pedido não inserido");
        }

        [Fact]
        public void AddPedidoComUmItem()
        {
            var pedidoService = new PedidoService(new PedidoRepositoryMok(),
                                        new NotificationContext(),
                                        new PedidoNotificationMessages());            
            var pedidoItem = new PedidoItem();
            pedidoItem.Descricao = "Item 4";
            pedidoItem.PedidoId = 0;
            pedidoItem.PrecoUnitario = 12;
            pedidoItem.Quantidade = 3;
            var pedido = new Pedido(0, 8547, new List<PedidoItem>() { pedidoItem });
            var result = pedidoService.AddAsync(pedido).Result;

            Assert.Equal(1, result.Id);
            Assert.Equal(1, pedido.PedidoItems.FirstOrDefault().Id);
        }

    }
}