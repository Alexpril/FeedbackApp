using BlazorFeedbackApp.DAL.Models;

namespace BlazorFeedBackApp.Core.Services;

public interface IUserService
{
    Task<User> GetOrCreateUserAsync(string objectId, string email, string displayName);
    Task UpdateLastLoginAsync(string objectId);
    Task<User> GetUserByObjectIdAsync(string objectId);
}
