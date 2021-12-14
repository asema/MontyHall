using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonteyHall.Web.Model;

namespace MonteyHall.Web.Pages
{
    public class MontyHallGameModel : PageModel
    {
        private readonly IMontyHall _montyHall;

        [BindProperty] 
        public MontyHallModel MontyHallModel { get; set; }

        [BindProperty]
        public string FinalOutput { get; set; }

        public MontyHallGameModel(IMontyHall montyHall)
        {
            _montyHall = montyHall;
        }
        public void OnGet()
        {
            MontyHallModel = new MontyHallModel();
        }

        public async Task<IActionResult> OnPost(MontyHallModel montyHallModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                var numberOfAttempts = $"{montyHallModel.NumberOfSimulation:n0}";
                var gameType = montyHallModel.IsDoorChanged
                    ? $"{numberOfAttempts} attempts with Door changed has "
                    : $"{numberOfAttempts} attempts With Same Door has ";
                var numberOfWins = await _montyHall.PlayTheGame(montyHallModel.NumberOfSimulation, montyHallModel.IsDoorChanged);
                FinalOutput = FormatResult(gameType, montyHallModel.NumberOfSimulation, numberOfWins);
            }
            return Page();

        }

        private static string FormatResult(string gameType, int numberOfSimulation, int wins)
        {
            return (gameType + ": " + ((float)wins / numberOfSimulation * 100) + "% wins and "+wins+" wins");
        }
    }
}
