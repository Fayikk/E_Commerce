﻿using E_CommerceForUdemy_API.Helper;
using E_CommerceForUdemy_Common;
using E_CommerceForUdemy_DataAccess;
using ECommerce_ForUdemy_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceForUdemy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _aPISettings;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _aPISettings = options.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO requestDTO)
        {
            if (!ModelState.IsValid || requestDTO == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Something went wrong",
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }
            ApplicationUser user = new()
            {
                UserName = requestDTO.Email,
                Email = requestDTO.Email,
                Name = requestDTO.Name,
                PhoneNumber = requestDTO.PhoneNumber,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, requestDTO.Password);

            if (!result.Succeeded)
            {
                return  BadRequest(new ErrorModelDTO()
                {
                    Errors = result.Errors.Select(u => u.Description),
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }
            var role = await _userManager.AddToRoleAsync(user, Keys.Role_Customer);
            if (!role.Succeeded)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Something went wrong",
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }
            return StatusCode(204);

        }


        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new LoginResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //everything is valid and we need to login 
                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new LoginResponseDTO()
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });

            }
            else
            {
                return Unauthorized(new LoginResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }
            return StatusCode(201);
        }


        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
