using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateVisitDto model)
        {
            try
            {
                await _visitService.AddAsync(model);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _visitService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var visits = await _visitService.GetAllAsync();
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var visit = await _visitService.GetById(id);
                return Ok(visit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("trip/{tripId}")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisitsByTrip(int tripId)
        {
            try
            {
                var visits = await _visitService.GetVisitsByTripId(tripId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<VisitDto>> GetVisitByUserAndDate([FromQuery] int userId, [FromQuery] DateTime date)
        {
            try
            {
                var visit = await _visitService.GetVisitByUserAndDate(userId, date);
                return Ok(visit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVisitDto model)
        {
            try
            {
                await _visitService.UpdateAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/photo")]
        public async Task<IActionResult> UpdateWithPhoto(int id, [FromForm] UpdateVisitWithPhotosDto model, [FromForm] List<IFormFile> photos)
        {
            try
            {
                if (photos == null || !photos.Any())
                {
                    return BadRequest("No photos provided");
                }

                await _visitService.UpdateWithPhotoAsync(id, model, photos);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
