using Application.Notifications;
using Application.Services.Domain;
using Domain.Entities;
using Xunit;
using XUnitTestProject.Mok.Repositories;

namespace XUnitTestProject
{
    public class MudancaStatusPedidoTest 
    {
        [Fact]
        public void GetStatusPedidoAprovado()
        {
            var statusPedidoService = new StatusPedidoService(null,
                                        new NotificationContext(),
                                        new PedidoRepositoryMok());
            var statusPedido = new StatusPedido(0, StatusPedidoService.StatusAprovado, 3, 20, 123456); 
            var result = statusPedidoService.GetStatusAsync(statusPedido).Result;

            Assert.True(result.Count == 1 && result.Exists(x => x.Equals(StatusPedidoService.StatusAprovado)), 
                        StatusPedidoService.StatusAprovado);
        }
        
        [Fact]
        public void GetStatusPedidoAprovadoValorAMenor()
        {
            var statusPedidoService = new StatusPedidoService(null,
                                        new NotificationContext(),
                                        new PedidoRepositoryMok());
            var statusPedido = new StatusPedido(0, StatusPedidoService.StatusAprovado, 3, 10, 123456);
            var result = statusPedidoService.GetStatusAsync(statusPedido).Result;

            Assert.True(result.Count == 1 && result.Exists(x => x.Equals(StatusPedidoService.StatusAprovadoValorAMenor)),
                        StatusPedidoService.StatusAprovadoValorAMenor);
        }

        [Fact]
        public void GetStatusPedidoAprovadoValorAMaiorEQuantidadeAMaior()
        {
            var statusPedidoService = new StatusPedidoService(null,
                                        new NotificationContext(),
                                        new PedidoRepositoryMok());
            var statusPedido = new StatusPedido(0, StatusPedidoService.StatusAprovado, 4, 21, 123456);
            var result = statusPedidoService.GetStatusAsync(statusPedido).Result;
            var valorAMAior = result.Exists(x => x.Equals(StatusPedidoService.StatusAprovadoValorAMaior));
            var quantidadeAMAior = result.Exists(x => x.Equals(StatusPedidoService.StatusAprovadoQuantidadeAMaior));
            
            Assert.True(result.Count == 2 && valorAMAior && quantidadeAMAior,
                        StatusPedidoService.StatusAprovadoValorAMenor);
        }

        [Fact]
        public void GetStatusPedidoAprovadoQuantidadeAMenor()
        {
            var statusPedidoService = new StatusPedidoService(null,
                                        new NotificationContext(),
                                        new PedidoRepositoryMok());
            var statusPedido = new StatusPedido(0, StatusPedidoService.StatusAprovado, 2, 20, 123456);
            var result = statusPedidoService.GetStatusAsync(statusPedido).Result;            

            Assert.True(result.Count == 1 && result.Exists(x => x.Equals(StatusPedidoService.StatusAprovadoQuantidadeAMenor)),
                        StatusPedidoService.StatusAprovadoQuantidadeAMenor);
        }

        [Fact]
        public void GetStatusPedidoReprovado()
        {
            var statusPedidoService = new StatusPedidoService(null,
                                        new NotificationContext(),
                                        new PedidoRepositoryMok());
            var statusPedido = new StatusPedido(0, StatusPedidoService.StatusReprovado, 0, 0, 123456);
            var result = statusPedidoService.GetStatusAsync(statusPedido).Result;

            Assert.True(result.Count == 1 && result.Exists(x => x.Equals(StatusPedidoService.StatusReprovado)),
                        StatusPedidoService.StatusReprovado);
        }
    }
}