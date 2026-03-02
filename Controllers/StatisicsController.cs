using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web.Data;
using web.Models;
using System.Linq;

namespace web.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly SteakContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticsController(SteakContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = (user?.Email ?? "").Trim().ToLower();

            var playerId = _context.Players
                .Where(p => (p.Email ?? "").Trim().ToLower() == email)
                .Select(p => p.ID_player)
                .FirstOrDefault();


            var statistics = _context.Games
                .Select(game => new
                {
                    Game = game,
                    TotalWins = _context.GameStates
                        .Where(gs => gs.ID_player == playerId && gs.ID_game == game.ID_game)
                        .Sum(gs => (decimal?)gs.TotalWins) ?? 0m,
                    TotalLosses = _context.GameStates
                        .Where(gs => gs.ID_player == playerId && gs.ID_game == game.ID_game)
                        .Sum(gs => (decimal?)gs.TotalLosses) ?? 0m,
                    TotalProfitLoss = _context.GameStates
                        .Where(gs => gs.ID_player == playerId && gs.ID_game == game.ID_game)
                        .Sum(gs => (decimal?)gs.TotalProfitLoss) ?? 0m
                })
                .ToList();

            return View(statistics);
        }
    }
}
