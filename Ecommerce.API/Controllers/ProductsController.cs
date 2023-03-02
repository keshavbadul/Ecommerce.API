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
	[Route("api/products")]
    // [Authorize(Roles = "admin")]
	public class ProductsController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMapper mapper, IProductRepository productRepository,
            ILogger<ProductsController> logger)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("/api/categories/{categoryId}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInCategory(
            int categoryId)
        {
            var products = await _productRepository.GetAllProductsInCategoryAsync(categoryId);

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("/api/categories/{categoryId}/products")]
        public async Task<ActionResult<ProductDto>> CreateProduct(
            int categoryId, ProductForCreationDto product)
        {
            if (!await _productRepository.CategoryExistsAsync(categoryId))
            {
                return NotFound();
            }

            var productEntity = _mapper.Map<Entities.Product>(product);

            await _productRepository.AddProductToCategory(categoryId, productEntity);
            await _productRepository.SaveChangesAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute(
                "GetProduct",
                new
                {
                    productId = productToReturn.Id
                },
                productToReturn);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            _logger.LogInformation($"user role = {claim}");

            var products = await _productRepository.GetAllProductsAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> GetProduct(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);

            if (product == null)
            {
                _logger.LogInformation("Product not found");
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{productId}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(
            int productId,
            JsonPatchDocument<ProductForUpdateDto> patchDocument)
        {
            var productEntity = await _productRepository.GetProductAsync(productId);
            if (productEntity == null)
            {
                return NotFound();
            }

            var productToPatch = _mapper.Map<ProductForUpdateDto>(productEntity);
            patchDocument.ApplyTo(productToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(productToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(productToPatch, productEntity);

            await _productRepository.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(product);
            await _productRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

