using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingPointController : ControllerBase
    {
        private readonly ITradingPointService _tradingPointService;

        public TradingPointController(ITradingPointService tradingPointService)
        {
            _tradingPointService = tradingPointService;
        }

        [HttpPost]
        [Route("AddTradingPoint")]
        public async Task<IActionResult> AddAsync([FromBody] CreateTradingPointDto model)
        {
            try
            {
                await _tradingPointService.AddAsync(model);
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
                var tradingPoint = await _tradingPointService.GetById(id);
                return Ok(tradingPoint);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var tradingPoints = await _tradingPointService.GetAllWithPaginationAsync(page, limit);
                return Ok(tradingPoints);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserIdAsync(int id)
        {
            try
            {
                var tradingPoint = await _tradingPointService.GetByUserIdAsync(id);
                return Ok(tradingPoint);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            try
            {
                var tradingPoint = await _tradingPointService.GetByNameAsync(name);
                return Ok(tradingPoint);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("workregion/{workRegionId}")]
        public async Task<IActionResult> GetByWorkRegionAsync(int workRegionId)
        {
            try
            {
                var tradingPoints = await _tradingPointService.GetByWorkRegionAsync(workRegionId);
                return Ok(tradingPoints);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTradingPointDto model)
        {
            try
            {
                await _tradingPointService.UpdateAsync(id, model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _tradingPointService.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
