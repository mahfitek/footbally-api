using Footbally.Application.DTOs.Auth;
using Footbally.Application.DTOs.User;

namespace Footbally.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
}