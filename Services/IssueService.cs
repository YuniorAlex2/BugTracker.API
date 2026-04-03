using BugTracker.API.Data;
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
    public List<Issue> GetAll()
    {
        return _context.Issues
            .Include(i => i.Project)
            .ToList();
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