using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO.User;
using TopSoSanh.DTO;
using TopSoSanh.Helper;
using TopSoSanh.Entity;
using TopSoSanh.Services.Interface;
using Role = TopSoSanh.Helper.Role;
using System.ComponentModel.DataAnnotations;

namespace TopSoSanh.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            ErrorModel errors = new ErrorModel();

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                var account = await _userManager.FindByNameAsync(model.Email);
                if (result.Succeeded)
                {
                    var roleAccount = await _userManager.GetRolesAsync(account);
                    string roleName = roleAccount.Count() == 0 ? "User" : roleAccount.First();
                    int value = (int)Enum.Parse(typeof(Role), roleName);
                    return Ok(new
                    {
                        token = JWTHelper.GenerateJwtToken(account.UserName, account.Id, value),
                    });
                }
                else
                {
                    if (result.IsNotAllowed)
                    {
                        errors.Add(ErrorResource.EmailNotConfirm);
                    }
                    else
                    {
                        errors.Add(ErrorResource.LoginFail);
                    }
                    return BadRequest(errors);
                }
            }
            catch (Exception e)
            {
                errors.Add(e.Message.ToString());
                return BadRequest(errors);
            }
        }

        [HttpDelete("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return NoContent();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            ErrorModel error = await _userService.Register(model, _httpContextAccessor.HttpContext.Request.Host.Value);
            if (!error.IsEmpty)
                return BadRequest(error);
            return Ok();
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            ErrorModel errors = new ErrorModel();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                errors.Add(ErrorResource.UserNotFound);
                return BadRequest(errors);
            }

            var result = await _userManager.ConfirmEmailAsync(user, token.Replace(" ", "+"));
            if (result.Succeeded)
            {
                return Ok("Xác thực email thành công");
            }
            else
            {
                await _userManager.DeleteAsync(user);
                errors.Errors.Add(ErrorResource.TokenExpried);
                return BadRequest(errors);
            }
        }

        [HttpGet("ForgotPassword/{Email}")]
        public async Task<IActionResult> ForgotPassword([EmailAddress] string Email)
        {
            var result = await _userService.ForgotPassword(Email);
            if (!result.IsEmpty)
                return BadRequest(result);
            return Ok();
        }

        
    }
}
