using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFeedbackApp.DAL.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(500)]
        public string FeedbackMessage { get; set; }

        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
    }
}