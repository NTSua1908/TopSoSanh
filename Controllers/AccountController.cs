using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.DTO.User;
using TopSoSanh.Entity;
using TopSoSanh.Helper;
using TopSoSanh.Services.Interface;
using static TopSoSanh.Helper.ConstanstHelper;

namespace TopSoSanh.Controllers
{
	[Produces("application/json")]
    [Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly UserResolverService _userResolverService;

        public AccountController(IUserService userService, UserManager<User> userManager, UserResolverService userResolverService)
        {
            _userService = userService;
            _userManager = userManager;
            _userResolverService = userResolverService;
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
                    errors.Add(String.Format(ErrorResource.NotFound, "User"));
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

        [HttpPost("AddFavorite")]
        public IActionResult AddFavorite([FromBody] ProductModel product)
        {
            _userService.AddFavorite(product);
            return Ok();
        }

		[HttpPost("RemoveFavorite")]
		public IActionResult RemoveFavorite([FromBody] string productUrl)
		{
            ErrorModel errors = new ErrorModel();
			_userService.RemoveFavorite(productUrl, errors);
			return errors.IsEmpty ? Ok() : BadRequest(errors);
		}

        [HttpGet("GetFavoriteProductByToken")]
        public PaginationDataModel<ProductModel> GetFavoriteProductByToken(PaginationRequestModel req)
        {
            req.Format();
            return _userService.GetFavoriteProductByToken(req);
        }
	}
}
