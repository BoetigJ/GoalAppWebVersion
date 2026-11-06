namespace GoalAppWebVersion.Models.Entities
{
    public delegate void GoalCompletedEventHandler(Goal goal);

    public abstract class Goal
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HowOften { get; set; } = string.Empty;
        public string ReminderFrequency { get; set; } = string.Empty;
        public bool IsCompleted { get; private set; }
        public int Streak { get; protected set; }

        public event GoalCompletedEventHandler? OnCompleted;

        protected Goal() { }   // required by EF Core

        protected Goal(string name, string howOften, string frequency)
        {
            Name = name;
            HowOften = howOften;
            ReminderFrequency = frequency;
            IsCompleted = false;
        }

        public void MarkAsCompleted()
        {
            if (IsCompleted) return;
            IsCompleted = true;
            OnCompleted?.Invoke(this);
        }
    }
}
