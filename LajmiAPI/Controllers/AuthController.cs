using System.Security.Cryptography;
using DataAcces.Repositories;
using DTO;
using LajmiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LajmiAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public AuthController(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            return Ok();

        var token = GenerateResetToken();

        user.PasswordResetToken = token;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

        await _userRepository.UpdateAsync(user);

        var resetLink = $"https://localhost:7088/ResetPassword?token={Uri.EscapeDataString(token)}";

        await _emailService.SendAsync(
            dto.Email,
            "Reset password",
            $"Klik her for at nulstille dit password: {resetLink}"
        );

        return Ok();
    }
    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userRepository.GetByResetTokenAsync(dto.Token);

        if (user == null ||
            string.IsNullOrWhiteSpace(user.PasswordResetToken) ||
            user.PasswordResetTokenExpiry == null ||
            user.PasswordResetTokenExpiry <= DateTime.UtcNow)
        {
            return BadRequest("Invalid or expired token");
        }

        user.Password = HashPassword(dto.NewPassword); // vigtig!

        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;

        await _userRepository.UpdateAsync(user);

        return Ok();
    }
    
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    private string GenerateResetToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}