namespace DigitalLendingPlatform.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }

        // Navigation Properties
        public User User { get; set; }
    }
}