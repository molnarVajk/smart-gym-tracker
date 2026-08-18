using Microsoft.EntityFrameworkCore;
using SmartGymTracker.Api.Models;

namespace SmartGymTracker.Api.Config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<WorkoutRoutine> WorkoutRoutines { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
}
