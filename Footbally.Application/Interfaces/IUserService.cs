using Footbally.Application.DTOs.User;

namespace Footbally.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto?> GetByEmailAsync(string email);
    Task<UserResponseDto> CreateAsync(RegisterRequestDto dto);
    Task<bool> DeleteAsync(int id);
}