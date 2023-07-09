using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserWithEmail([FromRoute]string email)
        {
            var result =await _userRepository.GetUserByEmail(email);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Böyle bir mail adresi bulunamadı");

        }


    }
}
