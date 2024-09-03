using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpPost]
        [Route("AddTrip")]
        public async Task<IActionResult> AddAsync([FromBody] CreateTripWithVisitsDto model)
        {
            try
            {
                await _tripService.AddAsync(model);
                return StatusCode(StatusCodes.Status201Created);
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
                var trip = await _tripService.GetById(id);
                return Ok(trip);
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
                var trips = await _tripService.GetAllAsync();
                return Ok(trips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("current")]
        public async Task<ActionResult<TripDto>> GetCurrentTrip([FromQuery] int userId, [FromQuery] DateTime date)
        {
            try
            {
                var trip = await _tripService.getCurrentTripForUser(userId, date);
                return Ok(trip);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("workregion/{workRegionId}")]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetTripsByWorkRegion(int workRegionId)
        {
            try
            {
                var trips = await _tripService.getTripByWorkRegion(workRegionId);
                return Ok(trips);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("User/{user_id}")]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetTripsByUser(int userId)
        {
            try
            {
                var trips = await _tripService.getTripForUser(userId);
                return Ok(trips);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTripDto model)
        {
            try
            {
                await _tripService.UpdateAsync(id, model);
                return Ok();
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
                await _tripService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
