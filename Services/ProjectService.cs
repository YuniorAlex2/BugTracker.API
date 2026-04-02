using BugTracker.API.Models;

namespace BugTracker.API.Services
{
    public class ProjectService
    {
        private readonly List<Project> _projects = new();

        public List<Project> GetAll()
        {
            return _projects;
        }

        public Project Create(Project project)
        {
            project.Id = _projects.Count + 1;
            _projects.Add(project);
            return project;
        }

        public Project? Update(int id, Project updatedProject)
        {
            var existing = _projects.FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return null;

            existing.Name = updatedProject.Name;
            existing.Description = updatedProject.Description;
            existing.Owner = updatedProject.Owner;

            return existing;
        }

        public bool Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
                return false;

            _projects.Remove(project);
            return true;
        }
    }
}
