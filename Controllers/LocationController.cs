using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TopSoSanh.DTO;
using TopSoSanh.Entity;
using TopSoSanh.Services.Interface;

namespace TopSoSanh.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("Create")]
        public IActionResult CreateLocation([FromBody] LocationModel model)
        {
            ErrorModel errors = new ErrorModel();
            if (!ModelState.IsValid)
            {
                AddErrorsFromModelState(ref errors);
                return BadRequest(errors);
            }
            Guid locationId = _locationService.Add(model);
            return Ok(locationId);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateLocation(Guid id, [FromBody] LocationModel model)
        {
            ErrorModel errors = new ErrorModel();
            if (!ModelState.IsValid)
            {
                AddErrorsFromModelState(ref errors);
                return BadRequest(errors);
            }
            _locationService.Update(id, model, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteLocation(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            _locationService.Delete(id, errors);
            return errors.IsEmpty ? Ok() : BadRequest(errors);
        }

        [HttpGet("GetDetail/{id}")]
        public IActionResult GetDetail(Guid id)
        {
            ErrorModel errors = new ErrorModel();
            if (!ModelState.IsValid)
            {
                AddErrorsFromModelState(ref errors);
                return BadRequest(errors);
            }
            var location = _locationService.GetDetail(id, errors);
            return errors.IsEmpty ? Ok(location) : BadRequest(errors);
        }

        [HttpGet("GetAll")]
        public PaginationDataModel<LocationGetAllModel> GetAll(PaginationRequestModel req)
        {
            req.Format();
            var locations = _locationService.GetAll(req);
            return locations;
        }
    }
}
