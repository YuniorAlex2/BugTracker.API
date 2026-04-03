using BugTracker.API.Data;
using BugTracker.API.DTOs;
using BugTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Services;

public class IssueService
{
    private readonly AppDbContext _context;

    public IssueService(AppDbContext context)
    {
        _context = context;
    }

    public List<Issue> GetAll(IssueQueryParametersDto queryParams)
    {
        var query = _context.Issues
            .Include(i => i.Project)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryParams.Status))
        {
            query = query.Where(i => i.Status.ToString().ToLower() == queryParams.Status.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Priority))
        {
            query = query.Where(i => i.Priority.ToString().ToLower() == queryParams.Priority.ToLower());
        }

        if (queryParams.ProjectId.HasValue)
        {
            query = query.Where(i => i.ProjectId == queryParams.ProjectId.Value);
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var search = queryParams.Search.ToLower();

            query = query.Where(i =>
                i.Title.ToLower().Contains(search) ||
                i.Description.ToLower().Contains(search));
        }

        if (queryParams.PageNumber < 1)
            queryParams.PageNumber = 1;

        if (queryParams.PageSize < 1)
            queryParams.PageSize = 10;

        query = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        return query.ToList();
    }

    public Issue? GetById(int id)
    {
        return _context.Issues
            .Include(i => i.Project)
            .FirstOrDefault(i => i.Id == id);
    }

    public Issue Create(Issue issue)
    {
        _context.Issues.Add(issue);
        _context.SaveChanges();
        return issue;
    }

    public List<Issue> GetByProjectId(int projectId)
    {
        return _context.Issues
            .Where(i => i.ProjectId == projectId)
            .ToList();
    }

    public Issue? Update(int id, Issue updatedIssue)
    {
        var existing = _context.Issues.FirstOrDefault(i => i.Id == id);

        if (existing == null)
            return null;

        existing.Title = updatedIssue.Title;
        existing.Description = updatedIssue.Description;
        existing.Status = updatedIssue.Status;
        existing.Priority = updatedIssue.Priority;
        existing.ProjectId = updatedIssue.ProjectId;

        _context.SaveChanges();

        return existing;
    }

    public bool Delete(int id)
    {
        var issue = _context.Issues.FirstOrDefault(i => i.Id == id);

        if (issue == null)
            return false;

        _context.Issues.Remove(issue);
        _context.SaveChanges();
        return true;
    }
}