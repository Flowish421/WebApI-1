using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApI_1.Models;
using WebApI_1.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _userManager.Users
            .Select(u => new UserDto { Id = u.Id, Email = u.Email })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserDto { Id = user.Id, Email = user.Email });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterDto dto)
    {
        var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDto { Id = user.Id, Email = user.Email });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] RegisterDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.Email = dto.Email;
        user.UserName = dto.Email;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors);
    }
}
