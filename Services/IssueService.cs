using BugTracker.API.Models;

namespace BugTracker.API.Services;

public class IssueService
{
    private readonly List<Issue> _issues = new();

    public List<Issue> GetAll()
    {
        return _issues;
    }

    public Issue Create(Issue issue)
    {
        issue.Id = _issues.Count + 1;
        _issues.Add(issue);
        return issue;
    }

    public List<Issue> GetByProjectId(int projectId)
    {
        return _issues
            .Where(i => i.ProjectId == projectId)
            .ToList();
    }

    public Issue? Update(int id, Issue updatedIssue)
    {
        var existing = _issues.FirstOrDefault(i => i.Id == id);

        if (existing == null)
            return null;

        existing.Title = updatedIssue.Title;
        existing.Description = updatedIssue.Description;
        existing.Status = updatedIssue.Status;
        existing.Priority = updatedIssue.Priority;
        existing.ProjectId = updatedIssue.ProjectId;

        return existing;
    }

    public bool Delete(int id)
    {
        var issue = _issues.FirstOrDefault(i => i.Id == id);

        if (issue == null)
            return false;

        _issues.Remove(issue);
        return true;
    }
}