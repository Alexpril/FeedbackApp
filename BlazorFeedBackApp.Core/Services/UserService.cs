using BlazorFeedbackApp.DAL.Contexts;
using BlazorFeedbackApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorFeedBackApp.Core.Services;

public class UserService(FeedbackAppDbContext context) : IUserService
{
    public async Task<User> GetOrCreateUserAsync(string objectId, string email, string displayName)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ObjectId == objectId);

        if (user != null) return user;
        
        user = new User
        {
            ObjectId = objectId,
            Email = email,
            DisplayName = displayName,
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task UpdateLastLoginAsync(string objectId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ObjectId == objectId);
        if (user != null)
        {
            user.LastLoginAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }

    public async Task<User> GetUserByObjectIdAsync(string objectId)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.ObjectId == objectId);
    }
}