namespace GoalAppWebVersion.Models.Entities
{
    public interface ITrackable
    {
        int CurrentCount { get; }
        void RecordProgress();
    }
}