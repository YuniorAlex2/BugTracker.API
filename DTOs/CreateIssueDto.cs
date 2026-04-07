using BugTracker.API.Enums;
using BugTracker.API.Models;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.DTOs;

public class CreateIssueDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public IssueStatus Status { get; set; }

    [Required]
    public IssuePriority Priority { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ProjectId must be greater than 0.")]
    public int ProjectId { get; set; }
}