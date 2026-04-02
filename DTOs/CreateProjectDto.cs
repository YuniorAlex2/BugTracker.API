using System.ComponentModel.DataAnnotations;

namespace BugTracker.API.DTOs;

public class CreateProjectDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Owner { get; set; } = string.Empty;
}