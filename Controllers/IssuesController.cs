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
    public IActionResult GetAll()
    {
        return Ok(_issueService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var issue = _issueService.GetAll().FirstOrDefault(i => i.Id == id);

        if (issue == null)
            return NotFound();

        return Ok(issue);
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