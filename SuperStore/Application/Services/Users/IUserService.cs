using SuperStore.Application.DTOs.Users;

namespace Application.Services.Users
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<IReadOnlyList<UserDto>> GetListAsync(int page, int pageSize, string? sortBy, string? sortDir, string? search);
        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> PatchAsync(int id, PatchUserDto dto);
        Task<bool> DeleteAsync(int id);

    }
}