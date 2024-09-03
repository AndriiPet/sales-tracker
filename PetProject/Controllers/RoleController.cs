using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetProject.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        public readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateRoleDto model)
        {
            try
            {
                await _roleService.AddAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Add successful");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateRoleDto model)
        {
            try
            {
                await _roleService.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Add successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Add successful");
        }
    }
}
