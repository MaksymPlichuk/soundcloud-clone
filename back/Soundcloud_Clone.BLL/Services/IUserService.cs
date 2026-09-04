using Soundcloud_Clone.BLL.Dtos.Auth;
using Soundcloud_Clone.BLL.Services;

namespace Soundcloud_Clone.BLL.Services;

public interface IUserService
{
    Task<ServiceResponse> GetAllAsync();
}