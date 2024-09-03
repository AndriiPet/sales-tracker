using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto model)
        {
            try
            {
                await _userService.AddAsync(model);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUSers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllManagers()
        {
            try
            {
                var managers = await _userService.GetAllManagers();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<UserDto>> GetUserByName(string name)
        {
            try
            {
                var user = await _userService.GetByName(name);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{userId}/subordinates")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllSubordinates(int userId)
        {
            try
            {
                var subordinates = await _userService.GetAllSubordinates(userId);
                return Ok(subordinates);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("AllWithPagination")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllWithPagination([FromQuery] int page, [FromQuery] int limit)
        {
            try
            {
                var users = await _userService.GetAllWithPagination(page, limit);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{userId}/subordinates-by-region")]
        public async Task<ActionResult<IEnumerable<IGrouping<WorkRegionDto, UserDto>>>> GetSubordinatesByRegion(int userId)
        {
            try
            {
                var subordinatesByRegion = await _userService.GetSubordinatesByRegion(userId);
                return Ok(subordinatesByRegion);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("subordinates-in-region")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetSubordinatesInRegion([FromQuery] int regionId, [FromQuery] int managerId)
        {
            try
            {
                var subordinates = await _userService.GetSubordinatesInRegion(regionId, managerId);
                return Ok(subordinates);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/location")]
        public async Task<ActionResult<WorkRegionDto>> GetUserLocation(int id)
        {
            try
            {
                var location = await _userService.GetUserLocation(id);
                return Ok(location);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("without-region")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersWithoutRegion()
        {
            try
            {
                var users = await _userService.GetUsersWithoutRegion();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{userId}/visits")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisitsForUser(int userId)
        {
            try
            {
                var visits = await _userService.GetVisitsForUser(userId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{userId}/visits-stats")]
        public async Task<ActionResult<(int CompletedVisits, int TotalVisits)>> GetVisitsStats(int userId, [FromQuery] DateTime currentDate)
        {
            try
            {
                var stats = await _userService.GetVisitsStats(userId, currentDate);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto model)
        {
            try
            {
                await _userService.UpdateAsync(id, model);
                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
