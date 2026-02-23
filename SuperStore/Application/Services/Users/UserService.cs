using Microsoft.EntityFrameworkCore;
using SuperStore.Application.DTOs.Users;
using SuperStore.Domain.Entities;
using SuperStore.Infrastructure.Data;

namespace SuperStore.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

            return (user is null) ? null : ToDto(user);
        }

        public async Task<IReadOnlyList<UserDto>> GetListAsync(
            int page,
            int pageSize,
            string? sortBy,
            string? sortDir,
            string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 20;

            IQueryable<User> query = _db.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();

                query = query.Where(u => u.Name.Contains(term));
            }

            query = ApplySorting(query, sortBy, sortDir);

            var skip = (page - 1) * pageSize;

            var users = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

            return users.Select(ToDto).ToList();
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return ToDto(user);
        }
        
        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return false;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.IsActive = dto.IsActive;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchAsync(int id, PatchUserDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            if (dto.Name != null)
            {
                user.Name = dto.Name;
            }
            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }
            if (dto.IsActive.HasValue)
            {
                user.IsActive = dto.IsActive.Value;
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id
            == id);
            if (user is null) return false;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }


        private static IQueryable<User> ApplySorting(IQueryable<User> query, string? sortBy, string? sortDir)
        {
            var dir = (sortDir ?? "asc").Trim().ToLowerInvariant();
            var descending = dir == "desc";

            var key = (sortBy ?? "createdAt").Trim().ToLowerInvariant();

            // Add only fields you want to expose for sorting
            return key switch
            {
                "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "createdat" => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)
            };
        }
        private static UserDto ToDto(User u)
        {
            return new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            };
        }
    }

}


