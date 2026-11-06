using GoalAppWebVersion.Data;
using Microsoft.AspNetCore.Mvc;
using GoalAppWebVersion.Models.Entities;
using GoalAppWebVersion.Models;
using Microsoft.EntityFrameworkCore;

namespace GoalAppWebVersion.Controllers
{
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public GoalsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GoalViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Goal? goal = vm.GoalType switch
            {
                "HabitMake" => new HabitMakeGoal(vm.Name, vm.HowOften, vm.ReminderFrequency),
                "HabitBreak" => new HabitBreakGoal(vm.Name, vm.HowOften, vm.ReminderFrequency),
                "Exercise" => new ExerciseGoal(vm.Name, vm.HowOften, vm.ReminderFrequency),
                _ => null
            };

            if (goal is null)
            {
                ModelState.AddModelError(nameof(vm.GoalType), "Pick a goal type.");
                return View(vm);
            }

            goal.Description = vm.Description;
            await dbContext.AddAsync(goal);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ListGoals));
        }
        [HttpGet]
        public async Task<IActionResult> ListGoals()
        {
            var goals = await dbContext.Goal.ToListAsync();
            return View(goals);
        }
        [HttpGet]
        public async Task<IActionResult> editGoal(Guid id)
        {
            // Retrieve the goal from the database
            var goal = await dbContext.Goal.FindAsync(id);

            if (goal == null)
            {
                return NotFound();
            }

            // Convert Goal into GoalViewModel
            var viewGoal = new GoalViewModel
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
            };

            // Send the ViewModel to the view
            return View(viewGoal);
        }

        [HttpPost]
        public async Task<IActionResult> editGoal(GoalViewModel viewGoal)
        {
            if (!ModelState.IsValid)
            {
                return View(viewGoal);
            }

            var goal = await dbContext.Goal.FindAsync(viewGoal.Id);

            if (goal == null)
            {
                return NotFound();
            }

            goal.Name = viewGoal.Name;
            goal.Description = viewGoal.Description;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("ListGoals");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGoal(Guid id)
        {
            var goal = await dbContext.Goal.FindAsync(id);
            if (goal is not null)
            {
                dbContext.Goal.Remove(goal); 
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListGoals", "Goals");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordProgress(Guid id)
        {
            var goal = await dbContext.Goal.FindAsync(id);
            if (goal is null) return NotFound();

            if (goal is HabitMakeGoal make)
                make.OnMilestone += (g, streak) => TempData["Message"] = $"{g.Name}: {streak}-day milestone!";

            if (goal is ITrackable t) t.RecordProgress();

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ListGoals));
        }
    }
}
