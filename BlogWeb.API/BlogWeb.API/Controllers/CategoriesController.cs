using BlogWeb.API.Data;
using BlogWeb.API.Models.Domain;
using BlogWeb.API.Models.DTO;
using BlogWeb.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await CategoryRepository.CreateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await CategoryRepository.GetAllAsync();

            var reponse = new List<CategoryDto>();
            foreach (var category in categories)
            {
                reponse.Add(new CategoryDto {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }

            return Ok(reponse);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingCategory = await CategoryRepository.GetById(id);

            if(existingCategory is null)
            {
                return NotFound();
            }

            var reponse = new CategoryDto
            {
                Id=existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };

            return Ok(reponse);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            category = await CategoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            var reponse = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(reponse);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await CategoryRepository.DeleteAsync(id);

            if(category == null)
            {
                return NotFound();
            }

            var reponse = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };

            return Ok(reponse);
        }
    }
}
