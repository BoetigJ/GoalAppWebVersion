using System.ComponentModel.DataAnnotations;
namespace GoalAppWebVersion.Models
{
    public class GoalViewModel
    {
        public Guid Id { get; set; }
        [Required] public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        [Required] public string GoalType { get; set; } = "";   // "HabitMake", "HabitBreak", "Exercise"
        public string HowOften { get; set; } = "";
        public string ReminderFrequency { get; set; } = "";
    }
}
