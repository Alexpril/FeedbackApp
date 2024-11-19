using BlazorFeedbackApp.DAL.Contexts;
using BlazorFeedbackApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFeedBackApp.Core.Repository
{
    public class FeedbackRepository(FeedbackAppDbContext context) : IFeedbackRepository
    {
        private readonly FeedbackAppDbContext _context = context;

        public async Task<List<Feedback>> GetFeedbackAsync()
        {
            return await _context.FeedbackEntries.ToListAsync();
        }

        public async Task AddFeedbackAsync(Feedback feedbackEntry)
        {
            _context.FeedbackEntries.Add(feedbackEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedbackEntry)
        {
            _context.FeedbackEntries.Update(feedbackEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.FeedbackEntries.FindAsync(feedbackId);
            if (feedback != null)
            {
                _context.FeedbackEntries.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
