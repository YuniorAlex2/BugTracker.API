using System.ComponentModel.DataAnnotations;


namespace BugTracker.API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]

        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string Owner { get; set; } = string.Empty;

        public List<Issue> Issues { get; set; } = new List<Issue>();

    }
}
