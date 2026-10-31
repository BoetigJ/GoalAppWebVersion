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
        public async Task<IActionResult> Add(Models.GoalViewModel goalViewModel)
        {
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                Name = goalViewModel.Name,
                Description = goalViewModel.Description,
                Streak = goalViewModel.Streak
            };
            await dbContext.AddAsync(goal);
            await dbContext.SaveChangesAsync();

            return View();
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
                Streak = goal.Streak
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
            goal.Streak = viewGoal.Streak;

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
    }
}
