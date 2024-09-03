using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.Models;

namespace PetProject.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<IActionResult> AddAsync([FromBody] CreateCustomerDto model)
        {
            try
            {
                await _customerService.AddAsync(model);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            try
            {
                var customers = await _customerService.GetAllWithPaginationAsync(page, limit);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var customer = await _customerService.GetById(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCustomerDto model)
        {
            try
            {
                await _customerService.UpdateAsync(id, model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Route("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}