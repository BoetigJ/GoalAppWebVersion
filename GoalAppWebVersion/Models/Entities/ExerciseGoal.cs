namespace GoalAppWebVersion.Models.Entities
{
    public class ExerciseItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ExerciseType { get; set; } = string.Empty;
        public int Reps { get; set; }
    }

    public sealed class ExerciseGoal : Goal, ITrackable, IResettable
    {
        private ExerciseGoal() { }   // required by EF Core

        public ExerciseGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
        }

        public List<ExerciseItem> Exercises { get; } = new();

        public int CurrentCount => Streak;       // ITrackable
        public void RecordProgress() => Streak++;
        public void Reset() => Streak = 0;       // IResettable

        // Overload 1: a single exercise with reps
        public void AddExercise(string exerciseType, int reps)
        {
            if (string.IsNullOrWhiteSpace(exerciseType))
                throw new ArgumentException("Exercise name required.", nameof(exerciseType));
            if (reps < 0)
                throw new ArgumentOutOfRangeException(nameof(reps));
            Exercises.Add(new ExerciseItem { ExerciseType = exerciseType, Reps = reps });
        }

        // Overload 2: a single exercise, reps default to 0
        public void AddExercise(string exerciseType)
        {
            Exercises.Add(new ExerciseItem { ExerciseType = exerciseType, Reps = 0 });
        }

        // Overload 3: several exercises with the same reps
        public void AddExercise(int reps, params string[] exerciseTypes)
        {
            foreach (var type in exerciseTypes)
            {
                Exercises.Add(new ExerciseItem { ExerciseType = type, Reps = reps });
            }
        }

        // Overload 4: a batch that already has individual rep counts
        public void AddExercise(List<(string ExerciseType, int Reps)> exercisesToAdd)
        {
            foreach (var (type, reps) in exercisesToAdd)
            {
                Exercises.Add(new ExerciseItem { ExerciseType = type, Reps = reps });
            }
        }
    }
}