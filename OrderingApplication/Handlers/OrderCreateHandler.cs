using AutoMapper;
using MediatR;
using OrderingApplication.Commands.OrderCreate;
using OrderingApplication.Responses;
using OrderingDomain.DataAccess.Repositories;
using OrderingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingApplication.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            if (orderEntity == null)
            {
                throw new ApplicationException("Entity could not be mapped");
            }
            var order = await _orderRepository.AddAsync(orderEntity);
            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }
    }
}
