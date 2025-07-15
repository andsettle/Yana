using System.ComponentModel.DataAnnotations;

namespace Yana.Client.Shared.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Content is required")]
        [StringLength(5000, ErrorMessage = "Content cannot exceed 5000 characters")]
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }
    }
}
