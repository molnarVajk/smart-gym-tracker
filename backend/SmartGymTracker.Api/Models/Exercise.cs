namespace SmartGymTracker.Api.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // pl. "Fekvenyomás", "Guggolás"
        public int Sets { get; set; }                     // pl. 4 sorozat
        public int Reps { get; set; }                     // pl. 8 ismétlés
        public double Weight { get; set; }                // pl. 80.0 kg

        // Idegen kulcs és navigációs tulajdonság a WorkoutRoutine-hoz
        public int WorkoutRoutineId { get; set; }
        public WorkoutRoutine WorkoutRoutine { get; set; } = null!;
    }
}
