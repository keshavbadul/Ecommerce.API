using System;
using AutoMapper;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/cart")]
    [Authorize(Policy = "UserRecordsPolicy")]
    [AuthorizeUserId]
    public class CartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly ILogger<CartController> _logger;
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public CartController(
            IShoppingCartRepository cartRepository,
            ILogger<CartController> logger,
            IMapper mapper,
            IOrderRepository orderRepo)
        {
            _cartRepository = cartRepository ??
                throw new ArgumentNullException(nameof(cartRepository));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _orderRepo = orderRepo ?? throw new ArgumentNullException(nameof(orderRepo));
        }

        [HttpPost("products")]
        public async Task<ActionResult> AddItemToCart(
            int userId, CartItemForCreationDto cartItem)
        {
            if (!await _cartRepository.CheckIfUserHasCartAsync(userId))
            {
                _cartRepository.CreateShoppingCart(userId);
                await _cartRepository.SaveChangesAsync();
            }

            var cart = await _cartRepository.GetUserCart(userId);
            if (cart == null)
            {
                return NotFound();
            }
            cartItem.CartId = cart.Id;
            var cartItemEntity = _mapper.Map<Entities.CartItem>(cartItem);
            _cartRepository.CreateCartItemAsync(cartItemEntity);
            await _cartRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart(int userId)
        {
            // var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            // if (loggedInUserId != userId.ToString())
            // {
            //     return Forbid();
            // }

            var cart = await _cartRepository.GetUserCart(userId);

            var cartToReturn = _mapper.Map<CartDto>(cart);

            return Ok(cartToReturn);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsCart(int userId)
        {
            // var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            // if (loggedInUserId != userId.ToString())
            // {
            //     return Forbid();
            // }
            
            var products = await _cartRepository.GetAllProductsInCartAsync(userId);

            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsToReturn);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult> RemoveProductsFromCart(int userId, int cartItemId)
        {
            // var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            // if (loggedInUserId != userId.ToString())
            // {
            //     return Forbid();
            // }

            var cartItem = await _cartRepository.GetCartItem(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _cartRepository.DeleteCartItem(cartItem);
            await _cartRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{cartItemId}")]
        public async Task<ActionResult> ChangeCartItemQuantity(
            int userId, 
            int cartItemId,
            JsonPatchDocument<CartItemForUpdateDto> patchDocument)
        {
            // var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            // if (loggedInUserId != userId.ToString())
            // {
            //     return Forbid();
            // }

            if (!await _cartRepository.CheckIfUserHasCartAsync(userId))   
            {
                return NotFound();
            }

            var cartItemEntity = await _cartRepository.GetCartItem(cartItemId);

            if (cartItemEntity == null)
            {
                return NotFound();
            }

            var cartItemToPatch = _mapper.Map<CartItemForUpdateDto>(cartItemEntity);

            patchDocument.ApplyTo(cartItemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(cartItemToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(cartItemToPatch, cartItemEntity);

            await _cartRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCart(int userId)
        {
            _cartRepository.ClearCart(userId);
            await _cartRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}