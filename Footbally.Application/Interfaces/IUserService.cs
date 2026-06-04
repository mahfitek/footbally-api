using Footbally.Application.DTOs.User;

namespace Footbally.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto?> GetByEmailAsync(string email);
    Task<UserResponseDto> CreateAsync(RegisterRequestDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<UserResponseDto>> GetAllAsync();
    Task<bool> SetActiveAsync(int id, bool isActive);
    Task<AdminUserDetailDto?> GetDetailAsync(int id);
}