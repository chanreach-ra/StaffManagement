using Microsoft.AspNetCore.Mvc;
using StaffManagementAPI.DTOs;
using StaffManagementAPI.Models;
using StaffManagementAPI.Services;

namespace StaffManagementAPI.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : BaseController<IStaffService>
{
    private readonly ILogger<StaffController> _logger;

    public StaffController(IStaffService baseService, ILogger<StaffController> logger) : base(baseService)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostStaffAsync([FromBody] StaffDTO staff)
    {
        try
        {
            var response = await _baseService.PostStaffAsync(staff);
            return Ok(response);
        }
        catch
        {
            throw;
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStaffAsync([FromRoute] int id, [FromBody] StaffDTO staff)
    {
        try
        {
            var response = await _baseService.PutStaffAsync(id, staff);
            return Ok(response);
        }
        catch
        {
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStaffByIdAsync([FromRoute] int id)
    {
        try
        {
            var response = await _baseService.GetStaffByIdAsync(id);
            return Ok(response);
        }
        catch
        {
            throw;
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStaffAsync([FromRoute] int id)
    {
        try
        {
            var response = await _baseService.DeleteStaffAsync(id);
            return Ok(response);
        }
        catch
        {
            throw;
        }
    }
    [HttpPost("by-criteria")]
    public async Task<IActionResult> GetStaffAsync([FromBody] SearchStaffDTO search)
    {
        try
        {
            var response = await _baseService.GetStaffAsync(search);
            return Ok(response);
        }
        catch
        {
            throw;
        }
    }
}
