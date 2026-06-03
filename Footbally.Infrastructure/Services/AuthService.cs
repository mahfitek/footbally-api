using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Footbally.Application.DTOs.Auth;
using Footbally.Application.DTOs.User;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Footbally.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly FootballyDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(FootballyDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email && u.IsActive);

        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return null;

        return GenerateToken(user);
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existingUser != null)
            throw new InvalidOperationException("Bu email zaten kayıtlı.");

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return GenerateToken(user);
    }

    private LoginResponseDto GenerateToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not found.");
        var jwtIssuer = _configuration["Jwt:Issuer"] ?? "Footbally";
        var jwtAudience = _configuration["Jwt:Audience"] ?? "FootballyUsers";
        var expiresAt = DateTime.UtcNow.AddDays(7);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: creds
        );

        return new LoginResponseDto
        {
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            ExpiresAt = expiresAt
        };
    }
}