using Microsoft.AspNetCore.Mvc;
using Soundcloud_Clone.BLL.Services;

namespace Soundcloud_Clone.API.Extensions
{
    public static class ControllerBaseExtension
    {
        public static IActionResult GetAction(this ControllerBase controller, ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                return controller.Ok(response);
            }
            else
            {
                return controller.BadRequest(response);
            }
        }
    }
}
