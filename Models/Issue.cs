using BugTracker.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.Models

{
    public class Issue
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public IssueStatus Status { get; set; } = IssueStatus.Todo;

        public IssuePriority  Priority { get; set; } = IssuePriority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}
