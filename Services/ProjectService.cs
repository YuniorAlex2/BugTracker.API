using BugTracker.API.Data;
using BugTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.API.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public List<Project> GetAll()
        {
            return _context.Projects
                .Include(p => p.Issues)
                .ToList();
        }

        public Project? GetById(int id)
        {
            return _context.Projects
                .Include(p => p.Issues)
                .FirstOrDefault(p => p.Id == id);
        }

        public Project Create(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();

            return _context.Projects
                .Include(p => p.Issues)
                .First(p => p.Id == project.Id);
        }

        public Project? Update(int id, Project updatedProject)
        {
            var existing = _context.Projects.FirstOrDefault(p => p.Id == id);

            if (existing == null)
                return null;

            existing.Name = updatedProject.Name;
            existing.Description = updatedProject.Description;
            existing.Owner = updatedProject.Owner;

            _context.SaveChanges();

            return _context.Projects
                .Include(p => p.Issues)
                .FirstOrDefault(p => p.Id == id);
        }

        public bool Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return true;
        }
    }
}