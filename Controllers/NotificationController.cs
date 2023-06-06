using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Services.Implement;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationController : BaseController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetDetail/{id}")]
        public IActionResult GetDetail(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            var notification = _notificationService.GetDetail(id, errors);
            return errors.IsEmpty ? Ok(notification) : BadRequest(errors);
        }

        [HttpGet("GetAll")]
        public PaginationDataModel<NotificationModel> GetAll(PaginationRequestModel req)
        {
            req.Format();
            return _notificationService.GetAll(req);
        }

        [HttpPost("Update/{id}")]
        public IActionResult Update(Guid id, [FromBody] NotificationUpdateModel model)
        {
            ErrorModel errors = new ErrorModel();
            if (!ModelState.IsValid)
            {
                AddErrorsFromModelState(ref errors);
                return BadRequest(errors);
            }
            _notificationService.Update(id, model, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpPost("ToggleNotification/{id}")]
        public IActionResult ToggleActiveNotification(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            _notificationService.ToggleNotification(id, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpPost("ToggleAutoOrder/{id}")]
        public IActionResult ToggleAutoOrder(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            _notificationService.ToggleAutoOrder(id, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            _notificationService.Delete(id, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }
    }
}
