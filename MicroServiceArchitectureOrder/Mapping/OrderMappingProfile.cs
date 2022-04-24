using AutoMapper;
using EventBusRabbitMQ.Events.Concrete;
using OrderingApplication.Commands.OrderCreate;

namespace MicroServiceArchitectureOrder.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
