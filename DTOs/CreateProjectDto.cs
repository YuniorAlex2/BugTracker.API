using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.DTOs;

public class CreateProjectDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Owner { get; set; } = string.Empty;
}