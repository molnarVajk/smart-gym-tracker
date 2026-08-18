namespace SmartGymTracker.Api.Models
{
    public class WorkoutRoutine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // pl. "Push Day", "Pull Day"
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Idegen kulcs és navigációs tulajdonság a User-hez
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Reláció: Egy edzéstervben több gyakorlat található (1-to-Many)
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
