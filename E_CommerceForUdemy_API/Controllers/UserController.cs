using E_CommerceForUdemy_API.MailService;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailHelperForgotPassword _mailHelper;
        
        public UserController(IUserRepository userRepository,
                              IMailHelperForgotPassword mailHelper)
        {
            _userRepository = userRepository;
            _mailHelper = mailHelper;
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

        [HttpPost("SendEmail")]
        public IActionResult PostEmailForChangePassword(ChangePasswordModel model)
        {
            var result = _mailHelper.SendEmailForResEmail(model.Subject,model.Mail, model.Body);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
