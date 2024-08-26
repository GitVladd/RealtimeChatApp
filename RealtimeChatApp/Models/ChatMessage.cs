using System.ComponentModel.DataAnnotations;

namespace RealtimeChatApp.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Sentiment { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
