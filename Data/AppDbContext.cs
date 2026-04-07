using Microsoft.EntityFrameworkCore;
using BugTracker.API.Models;

namespace BugTracker.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Issue> Issues { get; set; }
    
    public DbSet<User> Users { get; set; }

}