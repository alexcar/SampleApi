using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Filters;
using SampleApi.Models;
using SampleApi.Repositories;
using SampleApi.Requests;
using SampleApi.Responses;

namespace SampleApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionAttribute))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // hostname/api/order
        [HttpGet]
        public IActionResult Get()
        {
            var a = 1;
            var b = 0;
            int result = a / b;
            return Ok(Map(_orderRepository.Get()));
        }

        // hostname/api/order/<guid>
        [HttpGet("{id:guid}")]
        [OrderExists]
        public IActionResult GetById(Guid id)
        {
            return Ok(_orderRepository.Get(id));
        }

        // hostname/api/order
        [HttpPost]
        public IActionResult Post(OrderRequest request)
        {
            var order = Map(request);
            _orderRepository.Add(order);

            // É de responsabilidade do POST informar ao cliente onde foi 
            // criado o pedido.
            //return Ok();
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, null);
        }

        // hostname/api/order/<guid>
        [HttpPut("id:guid")]
        [OrderExists]
        public IActionResult Put(Guid id, OrderRequest request)
        {
            /*
             * As ações Put são usadas para substituir um recurso por outro.
             * Não apenas algumas propriedades e sim todas elas.
             */

            if (request.ItemsIds == null)
                return BadRequest();

            var order = _orderRepository.Get(id);            
            
            // substituido pelo action filter OrderExists
            //if (order == null)
            //    return NotFound(new { Message = $"Item with id {id} not exist." });

            order = Map(request, order);

            _orderRepository.Update(id, order);
            return Ok();
        }

        // hostname/api/order/<guid>
        [HttpDelete("{id:guid}")]
        [OrderExists]
        public IActionResult Delete(Guid id)
        {
            var order = _orderRepository.Get(id);

            // substituido pelo action filter OrderExists
            //if (order == null)
            //    return NotFound(new { Message = $"Item with id {id} not exist." });

            _orderRepository.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [OrderExists]
        public IActionResult Patch(Guid id, JsonPatchDocument<Order> requestOp)
        {
            /*
             * dotnet add package Microsoft.AspNetCore.JsonPatch
             * dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
             * Adicona .AddNewtonsoftJson(); na classe StartUp
             */

            var order = _orderRepository.Get(id);

            // substituido pelo action filter OrderExists
            //if (order == null)
            //    return NotFound(new { Message = $"Item with id {id} not exist." });

            requestOp.ApplyTo(order);
            _orderRepository.Update(id, order);

            return Ok();
        }

        private Order Map(OrderRequest request)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                ItemsIds = request.ItemsIds,
                Currency = request.Currency
            };
        }

        private Order Map(OrderRequest request, Order order)
        {
            order.ItemsIds = request.ItemsIds;
            order.Currency = request.Currency;

            return order;
        }

        private IEnumerable<OrderResponse> Map(IEnumerable<Order> orders)
        {
            return orders.Select(Map).ToList();
        }

        private OrderResponse Map(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                ItemsIds = order.ItemsIds,
                Currency = order.Currency
            };
        }
    }
}