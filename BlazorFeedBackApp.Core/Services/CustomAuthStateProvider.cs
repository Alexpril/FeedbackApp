using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using BlazorFeedBackApp.Core.Services;
using Microsoft.AspNetCore.Http;

namespace BlazorFeedbackApp.Core.Services
{
    public class CustomAuthStateProvider(
        IUserService userService,
        IHttpContextAccessor httpContextAccessor
        ) : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var objectId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = user.FindFirst("emails")?.Value;
                var name = user.FindFirst(ClaimTypes.Name)?.Value;

                if (!string.IsNullOrEmpty(objectId))
                {
                    await userService.GetOrCreateUserAsync(objectId, email, name);
                    await userService.UpdateLastLoginAsync(objectId);
                }
            }

            return new AuthenticationState(user ?? new ClaimsPrincipal());
        }
    }
}