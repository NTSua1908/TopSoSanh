using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductTrackingController : BaseController
    {
        private readonly IProductTrackingService _productTrackingService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductTrackingController(IProductTrackingService productTrackingService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _productTrackingService = productTrackingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Subscribe")]
        [AllowAnonymous]
        public IActionResult SubscribeProductTracking([FromBody] SubscribeProductModel model)
        {
            ErrorModel errors = new ErrorModel();
            _productTrackingService.SubscribeProduct(model, _httpContextAccessor.HttpContext.Request.Host.Value, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpPost("SubscribeCustom")]
        public void SubscribeProductTracking([FromBody] SubscribeProductCustomModel model)
        {
            _productTrackingService.SubscribeProductFromCustomShop(model, _httpContextAccessor.HttpContext.Request.Host.Value);
        }

        [HttpGet("UnSubscribe")]
        [AllowAnonymous]
        public string UnSubscribeProductTracking(string email, string token)
        {
            return _productTrackingService.UnSubscribeProduct(email, token);
        }

        [HttpGet("TrackingResult")]
        public TrackingResultModel GetTrackingResult(string productUrl)
        {
            return _productTrackingService.GetTrackingResult(productUrl);
        }

        [HttpGet("ToggleNotification/notificationId")]
        public IActionResult ToggleActiveNotification(Guid notificationId)
        {
            ErrorModel errors = new ErrorModel();
            _productTrackingService.ToggleNotification(notificationId, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }
    }
}
