namespace GoalAppWebVersion.Models.Entities
{
    public sealed class HabitBreakGoal : Goal, ITrackable, IResettable
    {
        private HabitBreakGoal() { }   // required by EF Core

        public HabitBreakGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
        }

        public event Action<HabitBreakGoal>? OnRelapse;

        public int CurrentCount => Streak;                              // ITrackable
        public void RecordProgress() => IncrementDaysSinceRelapse();    // ITrackable
        public void Reset() => Relapse();                               // IResettable

        public void IncrementDaysSinceRelapse()
        {
            Streak++;
        }

        public void Relapse()
        {
            Streak = 0;
            OnRelapse?.Invoke(this);
        }
    }
}
