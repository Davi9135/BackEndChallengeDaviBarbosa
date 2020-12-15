using Api.Model;
using Application.Interfaces.Services.Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Net;
using Application.Notifications;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoItemService _pedidoItemService;
        private readonly string _uriPedidoHasBeenCreated = "api/pedido/{0}";

        public PedidoController(IPedidoService pedidoService, IPedidoItemService pedidoItemService)
        {
            _pedidoService = pedidoService;
            _pedidoItemService = pedidoItemService;
        }

        // GET: api/pedido
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try { 
                var pedidoEntity = await _pedidoService.GetAllIncludingPedidoItemAsync();
                var result = mapPedidoEntitysToPedidoModels(pedidoEntity);

                return Ok(result);            
            } catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int) HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }
        }

        // GET api/pedido/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetByIdIncludingPedidoItemAsync(id);

                return Ok((PedidoModel)pedido);
            }
            catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int)HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }
        }

        // POST api/pedido
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoModel pedido)
        {
            try
            {
                var pedidoEntity = mapPedidoModelToPedidoEntity(pedido);

                if (pedidoEntity.Invalid)
                    return BadRequest(Notification.GetBadRequestNotification());

                var result = await _pedidoService.AddAsync(pedidoEntity);

                if (result == null || result.Id == 0)
                    return BadRequest(Notification.GetBadRequestNotification());

                return Created(string.Format(_uriPedidoHasBeenCreated, pedido.Id), (PedidoModel)result);
            } catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int)HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }
        }

        // PUT api/pedido/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PedidoModel pedido)
        {
            try
            {
                var pedidoEntity = mapPedidoModelToPedidoEntity(pedido);

                if (pedidoEntity.Invalid)
                    return BadRequest(Notification.GetBadRequestNotification());

                await _pedidoService.UpdateAsync(pedidoEntity);
                // Criar outro controlador para atualizar os itens do pedido em  /api/pedido/item e adicionar os verbos HTTP GET, POST, PUT e DELETE.
                await _pedidoItemService.UpdateRangeAsync(pedidoEntity.PedidoItems);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int)HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }            
        }

        // DELETE api/pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(Notification.GetBadRequestNotification());

                // Deletar também os itens do pedido.
                var result = await _pedidoService.RemoveAsync(id);

                if (result)
                    return NoContent();

                return BadRequest(Notification.GetBadRequestNotification());
            } catch (Exception ex)
            {
                // Gravar o erro 'ex' no log aqui
                return StatusCode((int)HttpStatusCode.InternalServerError, Notification.GetInternalServerErrorNotification());
            }
        }

        #region Private Methods 
        // Substituir pelo AutoMaper.
        private List<PedidoModel> mapPedidoEntitysToPedidoModels(IEnumerable<Pedido> pedidoEntitys)
        {
            var pedidoModels = new List<PedidoModel>();

            if (pedidoEntitys == null || pedidoEntitys.Count() <= 0)
                return new List<PedidoModel>();

            foreach (var item in pedidoEntitys)
            {
                pedidoModels.Add(item);
            }
            
            return pedidoModels;
        }

        // Substituir pelo AutoMaper.
        private Pedido mapPedidoModelToPedidoEntity(PedidoModel pedidoModel)
        {
            var pedidoItemEntitys = new List<PedidoItem>();

            if (pedidoModel.Itens != null && pedidoModel.Itens.Count > 0)
            {
                foreach (var item in pedidoModel.Itens)
                {
                    var pedidoItemEntity = new PedidoItem(item.Id, item.Qtd, pedidoModel.Id, item.Descricao, item.PrecoUnitario);
                    pedidoItemEntitys.Add(pedidoItemEntity);
                }
            }

            var pedidoEntity = new Pedido(pedidoModel.Id, pedidoModel.Pedido, pedidoItemEntitys);

            return pedidoEntity;
        }
        #endregion
    }
}