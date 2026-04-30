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
    // record for login
    public record LoginRequest(string Email, string Password);
    private readonly UserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;

    public UserController(UserRepository userRepository, ICompanyRepository companyRepository)
    {   
        _userRepository = userRepository;
        _companyRepository = companyRepository;
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
    public async Task<ActionResult<UserDto>> CreateUser(RegisterUserDto registerUserDto)
    {
        var company = await _companyRepository.GetByCvrAsync(registerUserDto.Cvr);
        Guid companyId;

       if (company == null)
        {
            var newCompanyRequest = new CreateCompanyRequest()
            {
                CompanyName = registerUserDto.CompanyName,
                Cvr = registerUserDto.Cvr,
                CompanyAddress = registerUserDto.CompanyAddress,
                CompanyEmail = registerUserDto.CompanyEmail,
                CompanyPhoneNumber = registerUserDto.CompanyPhoneNumber
            };

            var createdCompanyDto = await _companyRepository.CreateAsync(newCompanyRequest);
            companyId = createdCompanyDto.CompanyId;
        }
        else
        {
            companyId = company.CompanyId;
        }

        var user = registerUserDto.ToEntity();
        user.CompanyId = companyId;
        
        var createdUser = await _userRepository.CreateAsync(user);
        
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
// Når post bliver sendt fra login page, fanges den her og sendes videre til UserRepository hvori logikken findes.
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginRequest loginRequest)
    {
        var user = await _userRepository.LoginAsync(loginRequest.Email, loginRequest.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        else
        {
            return Ok(user);    
        }
        
    }

    [HttpPost("{userId}/favorites/{productId}")]
    public async Task<IActionResult> ToggleFavorite(Guid userId, Guid productId)
    {
        var succes = await _userRepository.ToggleFavoriteAsync(userId, productId);
        if (!succes)
        {
            return BadRequest();
        }
        else
        {
            return Ok();
        }
    }

    [HttpGet("{userId}/favorites")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetFavoriteProducts(Guid userId)
    {
        var favorites = await _userRepository.GetFavoritesAsync(userId);
        return Ok(favorites);
    }
}
