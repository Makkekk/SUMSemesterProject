using DataAcces.Mappers;
using DataAcces.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
    {

        var userEnity = userDto.ToEntity();
        
        var createdUser = await _userRepository.CreateAsync(userEnity);
        
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.UserId) return BadRequest();
        
        var success = await _userRepository.UpdateAsync(user);
        if (!success) return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var success = await _userRepository.DeleteAsync(id);
        if (!success) return NotFound();
        
        return NoContent();
    }
}
