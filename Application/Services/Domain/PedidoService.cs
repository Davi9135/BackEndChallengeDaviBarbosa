using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Application.Notifications;
using Application.Notifications.Domain;

namespace Application.Services.Domain
{
    public class PedidoService : ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly NotificationContext _notificationContext;
        private readonly PedidoNotificationMessages _pedidoNotificationMessages;
        public PedidoService(IPedidoRepository pedidoRepository, NotificationContext notificationContext, PedidoNotificationMessages pedidoNotificationMessages) : base(pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _notificationContext = notificationContext;            
            _pedidoNotificationMessages = pedidoNotificationMessages;
        }

        public override async Task<Pedido> AddAsync(Pedido entity)
        {
            var validPedido = await ValidAddPedido(entity);

            if (validPedido)
                return await _pedidoRepository.AddAsync(entity);

            return entity;
        }
                
        public override async Task UpdateAsync(Pedido entity)
        {
            var validPedido = await ValidUpdatePedido(entity);

            if (validPedido)
                await _pedidoRepository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<Pedido>> GetAllIncludingPedidoItemAsync()
        {
            return await _pedidoRepository.GetAllIncludingPedidoItemAsync();
        }

        public async Task<Pedido> GetByIdIncludingPedidoItemAsync(int id)
        {
            if(await _pedidoRepository.GetExistRecord(id))
                return await _pedidoRepository.GetByIdIncludingPedidoItemkAsync(id);

            _notificationContext.AddNotification(_pedidoNotificationMessages.GetPedidoDoesNotExistValidation());

            return null;
        }
        
        public async Task<bool> ValidAddPedido(Pedido entity)
        {
            if (entity.Invalid)
                _notificationContext.AddNotifications(entity.ValidationResult);

            var existingPedidoByNumeroDoPedido = await _pedidoRepository.GetExistByNumeroPedido(entity.NumeroDoPedido);

            if (existingPedidoByNumeroDoPedido)
                _notificationContext.AddNotification(_pedidoNotificationMessages.GetExistingRecordValidation());

            return entity.Valid && !existingPedidoByNumeroDoPedido;
        }

        public async Task<bool> ValidUpdatePedido(Pedido entity)
        {
            var existingPedidoByNumeroDoPedido = await _pedidoRepository.GetExistByNumeroPedido(entity.NumeroDoPedido);
            var pedidoChangedNumero = await PedidoChangedNumero(entity);

            return !existingPedidoByNumeroDoPedido && !pedidoChangedNumero;
        }

        public async Task<bool> PedidoChangedNumero(Pedido entity)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(entity.Id);

            if (pedido.NumeroDoPedido == entity.NumeroDoPedido)
                return false;

            _notificationContext.AddNotification(_pedidoNotificationMessages.GetPedidoChangedNumeroValidation());

            return false;
        }
    }
}