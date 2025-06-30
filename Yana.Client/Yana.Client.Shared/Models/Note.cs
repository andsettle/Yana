namespace Yana.Client.Shared.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }
    }
}
