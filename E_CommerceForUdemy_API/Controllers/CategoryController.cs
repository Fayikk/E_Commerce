using E_CommerceForUdemy_Business.Repository.IRepository;
using ECommerce_ForUdemy_Models;
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
    }
}
