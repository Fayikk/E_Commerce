using E_CommerceForUdemy_Business.Repository.IRepository;
using ECommerce_ForUdemy_Models;
using ECommerce_ForUdemy_Models.ElasticSearchViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var product = await _productRepository.Get(productId.Value);
            if (product == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(product);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productRepository.GetProductByCategoryId(id);
            if (result.Count == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Product is not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(result);
        }
        
        [HttpGet("elasticSearch")]
        public async Task<IActionResult> Search([FromQuery]ProductSearchViewModel model)
        {
            var productList = await _productRepository.SearchAsync(model);
            if (productList.Count == 0)
            {
                return BadRequest(new ErrorModelDTO
                {
                    ErrorMessage = "Eleman Bulunamadı",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(productList); 
        }




    }
}
