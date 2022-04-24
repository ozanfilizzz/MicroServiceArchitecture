using AutoMapper;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events.Concrete;
using EventBusRabbitMQ.Producer;
using MicroServiceArchitectureSourcing.DataAccess.Repositories;
using MicroServiceArchitectureSourcing.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MicroServiceArchitectureSourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        private readonly ILogger<AuctionsController> _logger;

        public AuctionsController(IAuctionRepository auctionRepository,
            IBidRepository bidRepository,
            IMapper mapper,
            EventBusRabbitMQProducer eventBusRabbitMQProducer,
            ILogger<AuctionsController> logger)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAll()
        {
            var auctions = await _auctionRepository.GetAll();
            return Ok(auctions);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetById(string id)
        {
            var auction = await _auctionRepository.GetById(id);
            if (auction == null)
            {
                _logger.LogError($"Auction with id : {id}, hasn't been found in database.");
                return NotFound();
            }
            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> Create([FromBody] Auction auction)
        {
            await _auctionRepository.Create(auction);
            return CreatedAtAction("GetById", new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> Update([FromBody] Auction auction)
        {
            return Ok(await _auctionRepository.Update(auction));

        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> Delete(string id)
        {
            return Ok(await _auctionRepository.Delete(id));

        }

        [HttpPost("CompleteAuction")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> CompleteAuction([FromBody]string id)
        {
            Auction auction = await _auctionRepository.GetById(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (auction.Status != (int)Status.Active)
            {
                _logger.LogError("Auction can not be complete");
                return BadRequest();
            }

            Bid bid = await _bidRepository.GetWinnerBid(id);
            if (bid == null)
            {
                return NotFound();
            }

            OrderCreateEvent eventMassage = _mapper.Map<OrderCreateEvent>(bid);
            eventMassage.Quantity = auction.Quantity;
            auction.Status = (int)Status.Closed;
            bool updateResponse = await _auctionRepository.Update(auction);
            if (!updateResponse)
            {
                _logger.LogError("Auction can not be complete");
                return BadRequest();
            }

            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMassage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing integration event: {EventId} from {AppName}", eventMassage.Id, "Sourcing");
            }
            return Accepted();
        }

        [HttpPost("TestEvent")]
        public ActionResult<OrderCreateEvent> TestEvent()
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "dummy1";
            eventMessage.ProductId = "dummy_product_1";
            eventMessage.Price = 10;
            eventMessage.Quantity = 100;
            eventMessage.SellerUserName = "test@test.com";

            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted(eventMessage);
        }
    }
}
