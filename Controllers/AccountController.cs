using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO.User;
using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using Role = TopSoSanh.Helper.Role;
using static TopSoSanh.Helper.ConstanstHelper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TopSoSanh.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly UserResolverService _userResolverService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, UserResolverService userResolverService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userResolverService = userResolverService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Create")]
        [Authorize(Roles = RoleConstant.Admin)]
        public IActionResult CreateUser()
        {
            return Ok();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            IActionResult actionResult;
            ErrorModel errors = new ErrorModel();
            if (!ModelState.IsValid)
            {
                AddErrorsFromModelState(ref errors);
                actionResult = BadRequest(errors);
            }
            else
            {
                var account = await _userManager.FindByIdAsync(_userResolverService.GetUser());
                if (account == null)
                {
                    errors.Add(ErrorResource.UserNotFound);
                    actionResult = NotFound(errors);
                }
                else
                {
                    var resetResults = await _userManager.ChangePasswordAsync(account, model.OldPassword, model.NewPassword);
                    if (resetResults.Succeeded)
                    {
                        actionResult = Ok();
                    }
                    else
                    {
                        AddErrorsFromResult(resetResults, ref errors);
                        actionResult = BadRequest(errors);
                    }
                }
            }
            return actionResult;
        }
    }
}
