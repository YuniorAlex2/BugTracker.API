using System.ComponentModel.DataAnnotations;
using BugTracker.API.Enums;

namespace BugTracker.API.DTOs;

public class CreateIssueDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public IssueStatus Status { get; set; } = IssueStatus.Todo;

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public int ProjectId { get; set; }
}