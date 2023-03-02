using AutoMapper;
using Ecommerce.API.Models;
using Ecommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

        // [HttpGet("{id}", Name = "GetCategory")]
        // public async Task<IActionResult> GetCategory(int id, [FromQuery]bool includeProducts)
        // {
        //     var category = await _productRepository.GetCategoryAsync(id,
        //         includeProducts);

        //     if (category == null)
        //     {
        //         return NotFound();
        //     }

        //     if (includeProducts)
        //     {
        //         return Ok(_mapper.Map<CategoryDto>(category));
        //     }

        //     return Ok(_mapper.Map<CategoryWithoutProductsDto>(category));
        // }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Entities.Category>(category);

            await _productRepository.CreateCategoryAsync(categoryEntity);
            await _productRepository.SaveChangesAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            // return CreatedAtRoute("GetCategory",
            //     new
            //     {
            //         id = categoryEntity.Id,
            //         includeProducts = false
            //     },
            //     categoryToReturn);
            
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(
            int categoryId,
            JsonPatchDocument<CategoryForUpdateDto> patchDocument)
        {
            var categoryEntity = await _productRepository.GetCategoryAsync(categoryId, false);

            if (categoryEntity == null)
            {
                return NotFound();
            }

            var categoryToPatch = _mapper.Map<CategoryForUpdateDto>(categoryEntity);

            patchDocument.ApplyTo(categoryToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(categoryToPatch))
            {
                return BadRequest();
            }

            _mapper.Map(categoryToPatch, categoryEntity);

            await _productRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}

