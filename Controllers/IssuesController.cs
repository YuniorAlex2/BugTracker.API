using BugTracker.API.DTOs;
using BugTracker.API.Models;
using BugTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IssueService _issueService;

    public IssuesController(IssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] IssueQueryParametersDto queryParams)
    {
        var issues = _issueService.GetAll(queryParams)
            .Select(i => new IssueDto
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Status = i.Status,
                Priority = i.Priority,
                CreatedAt = i.CreatedAt,
                ProjectId = i.ProjectId,
                ProjectName = i.Project != null ? i.Project.Name : ""
            })
            .ToList();

        return Ok(issues);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var issue = _issueService.GetById(id);

        if (issue == null)
            return NotFound();

        var result = new IssueDto
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status,
            Priority = issue.Priority,
            CreatedAt = issue.CreatedAt,
            ProjectId = issue.ProjectId,
            ProjectName = issue.Project != null ? issue.Project.Name : ""
        };

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Create(CreateIssueDto dto)
    {
        var issue = new Issue
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            Priority = dto.Priority,
            ProjectId = dto.ProjectId
        };

        var created = _issueService.Create(issue);

        var result = new IssueDto
        {
            Id = created.Id,
            Title = created.Title,
            Description = created.Description,
            Status = created.Status,
            Priority = created.Priority,
            CreatedAt = created.CreatedAt,
            ProjectId = created.ProjectId
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Issue issue)
    {
        var updated = _issueService.Update(id, issue);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _issueService.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}