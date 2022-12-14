using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTrackingController : ControllerBase
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
        public void SubscribeProductTracking(SubscribeProductModel model)
        {
            _productTrackingService.SubscribeProduct(model, _httpContextAccessor.HttpContext.Request.Host.Value);
        }

        [HttpGet("UnSubscribe")]
        public string UnSubscribeProductTracking(string email, string token)
        {
            return _productTrackingService.UnSubscribeProduct(email, token);
        }

        [HttpGet("TrackingResult")]
        public List<TrackingResultModel> GetTrackingResult(string productUrl)
        {
            return _productTrackingService.GetTrackingResult(productUrl);
        }
    }
}
