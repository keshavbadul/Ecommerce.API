using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoriesController(IProductRepository categoryRepository, IMapper mapper)
        {
            _productRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryWithoutProductsDto>>> GetCategories()
        {
            var categories = await _productRepository.GetCategoriesAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryWithoutProductsDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id, bool includeProducts)
        {
            var category = await _productRepository.GetCategoryAsync(id,
                includeProducts);

            if (category == null)
            {
                return NotFound();
            }

            if (includeProducts)
            {
                return Ok(_mapper.Map<CategoryDto>(category));
            }

            return Ok(_mapper.Map<CategoryWithoutProductsDto>(category));
        }
    }
}

