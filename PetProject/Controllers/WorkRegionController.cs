using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkRegionController : ControllerBase
    {
        private readonly IWorkRegionService _workRegionService;

        public WorkRegionController(IWorkRegionService workRegionService)
        {
            _workRegionService = workRegionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateWorkRegionDto model)
        {
            try
            {
                await _workRegionService.AddAsync(model);
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
                await _workRegionService.DeleteAsync(id);
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
                var workRegions = await _workRegionService.GetAllAsync();
                return Ok(workRegions);
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
                var workRegion = await _workRegionService.GetById(id);
                return Ok(workRegion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<VisitDto>> GetVisitByUserAndDate()
        {
            try
            {
                var workRegions = await _workRegionService.GetAllWithUsers();
                return Ok(workRegions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkRegionDto model)
        {
            try
            {
                await _workRegionService.UpdateAsync(id, model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
