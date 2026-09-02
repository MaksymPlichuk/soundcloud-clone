using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Services;
using Soundcloud_Clone.DAL;
using Soundcloud_Clone.DAL.Enitites.Identity;

namespace Soundcloud_Clone.API.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse> GetAllAsync()
    {
        var users = await _context
            .Set<UserEntity>()
            .AsNoTracking()
            .Select(user => new UserForInfoDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Image = user.Image
            })
            .ToListAsync();

        if (users.Count == 0)
        {
            return ServiceResponse.Failure("No users found");
        }

        return ServiceResponse.Success(
            $"Found {users.Count} users",
            users);
    }
}