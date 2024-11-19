using BlazorFeedbackApp.DAL.Models;

namespace BlazorFeedBackApp.Core.Repository
{
    public interface IFeedbackRepository
    {
        public Task<List<Feedback>> GetFeedbackAsync();
        public Task AddFeedbackAsync(Feedback feedbackEntry);
        public Task UpdateFeedbackAsync(Feedback feedbackEntry);
        public Task DeleteFeedbackAsync(int feedbackId);
    }
}
