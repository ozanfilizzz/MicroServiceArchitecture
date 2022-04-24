using AutoMapper;
using EventBusRabbitMQ.Events.Concrete;
using MicroServiceArchitectureSourcing.Entities;

namespace MicroServiceArchitectureSourcing.Mapping
{
    public class SourcingMappingProfile : Profile
    {
        public SourcingMappingProfile()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
