using AutoMapper;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/orders")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderRepository orderRepository,
            IOrderService orderService,
            ILogger<OrderController> logger,
            IMapper mapper)
        {
            _orderRepository = orderRepository ??
                throw new ArgumentNullException(nameof(orderRepository));
            _orderService = orderService ??
                throw new ArgumentNullException(nameof(orderService));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? 
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{orderId}", Name = "GetOrder")]
        public async Task<ActionResult<OrderDto>> GetOrder(
            [FromRoute]int userId, 
            [FromRoute]int orderId,
            [FromQuery]bool includeItems)
        {
            var order = await _orderRepository.GetOrder(orderId, includeItems);

            if (includeItems)
            {
                return Ok(_mapper.Map<OrderDto>(order));
            }
            
            return Ok(_mapper.Map<OrderWithoutItemsDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderWithoutItemsDto>>> GetOrders(
            int userId)
        {
            var orders = await _orderRepository.GetAllOrdersForUserAsync(userId);

            var ordersToReturn = _mapper.Map<IEnumerable<OrderWithoutItemsDto>>(orders);

            return Ok(ordersToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int userId)
        {
            try
            {
                await _orderService.ConvertCartToOrder(userId);
                await _orderRepository.SaveChangesAsync();
            } catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound(
                    new
                    {
                        status = 404,
                        message = e.Message
                    }
                );
            }

            return Ok();
        }
    }
}