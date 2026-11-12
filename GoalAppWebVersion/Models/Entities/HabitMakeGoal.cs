namespace GoalAppWebVersion.Models.Entities
{
    public delegate void StreakMilestoneHandler(HabitMakeGoal goal, int streak);

    public sealed class HabitMakeGoal : Goal, ITrackable, IResettable
    {
        private HabitMakeGoal() { }   // required by EF Core

        public HabitMakeGoal(string name, string howOften, string frequency)
            : base(name, howOften, frequency)
        {
        }

        public int CurrentCount => Streak;                    // ITrackable
        public void RecordProgress() => IncrementStreak();    // ITrackable
        public void Reset() => BreakStreak();                 // IResettable

        public event StreakMilestoneHandler? OnMilestone;
        private static readonly int[] milestones = { 7, 30, 100 };

        public void IncrementStreak()
        {
            Streak++;
            if (milestones.Contains(Streak))
            {
                OnMilestone?.Invoke(this, Streak);
            }
        }

        public void BreakStreak()
        {
            Streak = 0;
        }
    }
}