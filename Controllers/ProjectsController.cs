using BugTracker.API.DTOs;
using BugTracker.API.Models;
using BugTracker.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers;

    
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{   
    private readonly ProjectService _projectService;

    private readonly IssueService _issueService;

    public ProjectsController(ProjectService projectService, IssueService issueService)
    {
        _projectService = projectService;
        _issueService = issueService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var projects = _projectService.GetAll()
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Owner = p.Owner,
                CreatedAt = p.CreatedAt
            })
            .ToList();

        return Ok(projects);
    }
    [HttpGet("{id}")]
public IActionResult GetById(int id)
{
    var project = _projectService.GetById(id);

    if (project == null)
        return NotFound();

    var result = new ProjectDto
    {
        Id = project.Id,
        Name = project.Name,
        Description = project.Description,
        Owner = project.Owner,
        CreatedAt = project.CreatedAt
    };

    return Ok(result);
}

    [HttpGet("{id}/issues")]
    public IActionResult GetIssuesByProject(int id)
    {
        var projectIssues = _issueService.GetByProjectId(id);

        var result = projectIssues.Select(i => new IssueDto
        {
            Id = i.Id,
            Title = i.Title,
            Description = i.Description,
            Status = i.Status,
            Priority = i.Priority,
            CreatedAt = i.CreatedAt,
            ProjectId = i.ProjectId,
            ProjectName = i.Project != null ? i.Project.Name : ""
        }).ToList();

        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create(CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            Owner = dto.Owner
        };

        var created = _projectService.Create(project);

        var result = new ProjectDto
        {
            Id = created.Id,
            Name = created.Name,
            Description = created.Description,
            Owner = created.Owner,
            CreatedAt = created.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, Project project)
    {
        var updated = _projectService.Update(id, project);

        if (updated == null)
            return NotFound();

        var result = new ProjectDto
        {
            Id = updated.Id,
            Name = updated.Name,
            Description = updated.Description,
            Owner = updated.Owner,
            CreatedAt = updated.CreatedAt
        };

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var deleted = _projectService.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}

