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
    public class PedidoItemService : ServiceBase<PedidoItem>, IPedidoItemService
    {
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly NotificationContext _notificationContext;
        private readonly PedidoItemNotificationMessages _pedidoItemNotificationMessages;

        public PedidoItemService(IPedidoItemRepository repository, NotificationContext notificationContext, PedidoItemNotificationMessages pedidoItemNotificationMessages) : base(repository)
        {
            _pedidoItemRepository = repository;
            _notificationContext = notificationContext;
            _pedidoItemNotificationMessages = pedidoItemNotificationMessages;
        }

        public override async Task UpdateRangeAsync(IEnumerable<PedidoItem> entities)
        {
            if (entities == null || entities.Count() <= 0)
                return;

            foreach (var item in entities.ToList())
            {
                if (item.Invalid)
                {
                    _notificationContext.AddNotifications(item.ValidationResult);
                    continue;
                }

                await UpdateAsync(item);
            }
        }

        public async Task<IEnumerable<PedidoItem>> GetAllIncludingPedidoAsync()
        {
            return await _pedidoItemRepository.GetAllIncludingPedidoAsync();
        }

        public async Task<PedidoItem> GetByIdIncludingPedidoAsync(int id)
        {
            return await _pedidoItemRepository.GetByIdIncludingPedidoAsync(id);
        }

        public override async Task UpdateAsync(PedidoItem entity)
        {
            
            var existRecord = await _pedidoItemRepository.GetExistRecord(entity.Id);

            if (!existRecord)
            {
                _notificationContext.AddNotification(_pedidoItemNotificationMessages.GetExistingRecordValidation(entity.Id));
                return;
            }

            await _pedidoItemRepository.UpdateAsync(entity);
        }
    }
}