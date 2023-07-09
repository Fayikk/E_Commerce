using E_CommerceForUdemy_Business.Repository;
using E_CommerceForUdemy_Business.Repository.IRepository;
using ECommerce_ForUdemy_Models;
using ECommerce_ForUdemy_Models.ElasticSearchViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryRepository.GetAll();
            if (result == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Category is not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(result);
        }


        [HttpGet("elasticSearch")]
        public async Task<IActionResult> Search([FromQuery] CategorySearchViewModel model)
        {
            var categoryList = await _categoryRepository.SearchAsync(model);
            if (categoryList.Count == 0)
            {
                return BadRequest(new ErrorModelDTO
                {
                    ErrorMessage = "Eleman Bulunamadı",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(categoryList);
        }
    }
}
