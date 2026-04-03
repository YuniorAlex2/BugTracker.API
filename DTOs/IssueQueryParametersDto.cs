namespace BugTracker.API.DTOs;

public class IssueQueryParametersDto
{
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public int? ProjectId { get; set; }
    public string? Search { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}