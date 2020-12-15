using Application.Interfaces.Services.Domain;
using Application.Notifications;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class StatusPedidoService : ServiceBase<StatusPedido>, IStatusPedidoService
    {
        private readonly IStatusPedidoRepository _statusPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly NotificationContext _notificationContext;

        public StatusPedidoService(IStatusPedidoRepository statusPedidoRepository, NotificationContext notificationContext, IPedidoRepository pedidoRepository) : base(statusPedidoRepository)
        {
            _statusPedidoRepository = statusPedidoRepository;
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
        }

        public static readonly string StatusReprovado = "REPROVADO";
        public static readonly string StatusAprovado = "APROVADO";
        public static readonly string StatusAprovadoValorAMenor = "APROVADO_VALOR_A_MENOR";
        public static readonly string StatusCodigoPedidoInvalido = "CODIGO_PEDIDO_INVALIDO";
        public static readonly string StatusAprovadoQuantidadeAMenor = "APROVADO_QTD_A_MENOR";
        public static readonly string StatusAprovadoValorAMaior = "APROVADO_VALOR_A_MAIOR";
        public static readonly string StatusAprovadoQuantidadeAMaior = "APROVADO_QTD_A_MAIOR";

        public async Task<List<string>> GetStatusAsync(StatusPedido statusPedido)
        {
            var status = new List<string>();
            var isPedidoExists = await PedidoExistsByNumeroDoPedido(statusPedido.PedidoId);
            var pedido = await _pedidoRepository.GetByNumeroDoPedidokAsync(statusPedido.PedidoId);

            if (!isPedidoExists)
                status.Add(StatusCodigoPedidoInvalido);

            var isPedidoReprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusReprovado);
            if (isPedidoReprovado)
                status.Add(StatusReprovado);

            var pedidoAprovado = await PedidoAprovado(statusPedido, pedido);
            if (pedidoAprovado)
                status.Add(StatusAprovado);

            var pedidoAprovadoValorMenor = await PedidoAprovadoValorMenor(statusPedido, pedido);
            if (pedidoAprovadoValorMenor)
                status.Add(StatusAprovadoValorAMenor);

            var pedidoAprovadoQuantidadeMenor = await PedidoAprovadoQuantidadeMenor(statusPedido, pedido);
            if (pedidoAprovadoQuantidadeMenor)
                status.Add(StatusAprovadoQuantidadeAMenor);

            var pedidoAprovadoValorMaior = await PedidoAprovadoValorMaior(statusPedido, pedido);
            if (pedidoAprovadoValorMaior)
                status.Add(StatusAprovadoValorAMaior);

            var pedidoAprovadoQuantidadeMaior = await PedidoAprovadoQuantidadeMaior(statusPedido, pedido);
            if (pedidoAprovadoQuantidadeMaior)
                status.Add(StatusAprovadoQuantidadeAMaior);

            return status;
        }

        public async Task<bool> PedidoExistsByNumeroDoPedido(int numeroDoPedido)
        {
            var pedido = await _pedidoRepository.GetByNumeroDoPedidokAsync(numeroDoPedido);

            return pedido != null && pedido.NumeroDoPedido == numeroDoPedido;
        }

        public async Task<bool> PedidoAprovado(StatusPedido statusPedido, Pedido pedido)
        {
            var pedidoExists = await PedidoExistsByNumeroDoPedido(pedido.NumeroDoPedido);
            var itensAprovadosIgualQuantidadeItensPedido = ItensAprovadosIgualQuantidadeItensPedido(statusPedido.ItensAprovados, pedido.QuantidadeTotalItensPedido());
            var valorAprovadoIgualValorTotalPedido = ValorAprovadoIgualValorTotalPedido(statusPedido.ValorAprovado, pedido.ValorTotalItensPedido());
            var statusEqualsAprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusAprovado);

            return pedidoExists && itensAprovadosIgualQuantidadeItensPedido && valorAprovadoIgualValorTotalPedido && statusEqualsAprovado;
        }

        public async Task<bool> PedidoAprovadoValorMenor(StatusPedido statusPedido, Pedido pedido)
        {
            var pedidoExists = await PedidoExistsByNumeroDoPedido(pedido.NumeroDoPedido);
            var valorAprovadoMenorValorTotalPedido = ValorAprovadoMenorValorTotalPedido(statusPedido.ValorAprovado, pedido.ValorTotalItensPedido());
            var statusEqualsAprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusAprovado);

            return pedidoExists && valorAprovadoMenorValorTotalPedido && statusEqualsAprovado;
        }

        public async Task<bool> PedidoAprovadoQuantidadeMenor(StatusPedido statusPedido, Pedido pedido)
        {
            var pedidoExists = await PedidoExistsByNumeroDoPedido(pedido.NumeroDoPedido);
            var itensAprovadosMenorQuantidadeItensPedido = ItensAprovadosMenorQuantidadeItensPedido(statusPedido.ItensAprovados, pedido.QuantidadeTotalItensPedido());
            var statusEqualsAprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusAprovado);

            return pedidoExists && itensAprovadosMenorQuantidadeItensPedido && statusEqualsAprovado;
        }

        public async Task<bool> PedidoAprovadoValorMaior(StatusPedido statusPedido, Pedido pedido)
        {
            var pedidoExists = await PedidoExistsByNumeroDoPedido(pedido.NumeroDoPedido);
            var valorAprovadoMaiorValorTotalPedido = ValorAprovadoMaiorValorTotalPedido(statusPedido.ValorAprovado, pedido.ValorTotalItensPedido());
            var statusEqualsAprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusAprovado);

            return pedidoExists && valorAprovadoMaiorValorTotalPedido && statusEqualsAprovado;
        }

        public async Task<bool> PedidoAprovadoQuantidadeMaior(StatusPedido statusPedido, Pedido pedido)
        {
            var pedidoExists = await PedidoExistsByNumeroDoPedido(pedido.NumeroDoPedido);
            var itensAprovadosMaiorQuantidadeItensPedido = ItensAprovadosMaiorQuantidadeItensPedido(statusPedido.ItensAprovados, pedido.QuantidadeTotalItensPedido());
            var statusEqualsAprovado = StatusPedidoEqualsTo(statusPedido.Status, StatusAprovado);

            return pedidoExists && itensAprovadosMaiorQuantidadeItensPedido && statusEqualsAprovado;
        }

        public bool ItensAprovadosIgualQuantidadeItensPedido(int quantidadeItensAprovados, int quantidadeTotalItensPedido) => quantidadeTotalItensPedido == quantidadeItensAprovados;
        public bool ItensAprovadosMenorQuantidadeItensPedido(int quantidadeItensAprovados, int quantidadeTotalItensPedido) => quantidadeItensAprovados < quantidadeTotalItensPedido;
        public bool ItensAprovadosMaiorQuantidadeItensPedido(int quantidadeItensAprovados, int quantidadeTotalItensPedido) => quantidadeItensAprovados > quantidadeTotalItensPedido;
        public bool ValorAprovadoIgualValorTotalPedido(double valorAprovado, double valorTotalItensPedido) => valorAprovado == valorTotalItensPedido;
        public bool ValorAprovadoMenorValorTotalPedido(double valorAprovado, double valorTotalItensPedido) => valorAprovado < valorTotalItensPedido;
        public bool ValorAprovadoMaiorValorTotalPedido(double valorAprovado, double valorTotalItensPedido) => valorAprovado > valorTotalItensPedido;
        public bool StatusPedidoEqualsTo(string status, string statusEqualTo) => status.Equals(statusEqualTo);
    }
}