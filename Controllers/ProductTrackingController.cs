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

        public ProductTrackingController(IProductTrackingService productTrackingService)
        {
            _productTrackingService = productTrackingService;
        }

        [HttpPost("Subscribe")]
        public void SubscribeProductTracking(SubscribeProductModel model)
        {
            _productTrackingService.SubscribeProduct(model);
        }

        [HttpGet("TrackingResult")]
        public List<double> GetTrackingResult(Guid productId)
        {
            return _productTrackingService.GetTrackingResult(productId);
        }
    }
}
