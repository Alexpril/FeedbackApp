using System.ComponentModel.DataAnnotations;

namespace BlazorFeedbackApp.DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ObjectId { get; set; } // Azure AD B2C Object ID

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? DisplayName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastLoginAt { get; set; }
    }
}