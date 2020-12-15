using Api.Model;
using Application.Interfaces.Services.Domain;
using Application.Notifications;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusPedidoService _statusPedidoService;

        public StatusController(IStatusPedidoService statusPedidoService)
        {
            _statusPedidoService = statusPedidoService;
        }

        // POST api/<StatusController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StatusModel statusModel)
        {
            try
            {
                int pedidoId = 0;
                int.TryParse(statusModel.Pedido, out pedidoId);
                var result = new List<string>();

                if (pedidoId <= 0)
                {
                    result.Add("CODIGO_PEDIDO_INVALIDO");
                    return BadRequest(new StatusPedidoModel(statusModel.Pedido, result));
                }

                var statusPedidoEntity = mapStatusModelToStatusPedidoEntity(statusModel);
                var statusList = await _statusPedidoService.GetStatusAsync(statusPedidoEntity);
                var statusPedidoModel = getStatusPedidoModelByListOfStatus(statusList, pedidoId);

                return Ok(statusPedidoModel);
            }
            catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int)HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }
        }

        #region Private Methods 
        private StatusPedido mapStatusModelToStatusPedidoEntity(StatusModel statusModel)
        {
            var statusPedido = new StatusPedido(statusModel.Id,
                                                statusModel.Status, 
                                                statusModel.ItensAprovados, 
                                                statusModel.ValorAprovado,
                                                int.Parse(statusModel.Pedido));

            return statusPedido;
        }

        private StatusPedidoModel getStatusPedidoModelByListOfStatus(List<string> status, int pedidoId)
        {
            if (status == null && status.Count > 0)
                return new StatusPedidoModel(pedidoId.ToString(), new List<string>());

            return new StatusPedidoModel(pedidoId.ToString(), status);
        }
        #endregion
    }
}