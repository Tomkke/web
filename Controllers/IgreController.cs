using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class IgreController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SteakContext _context;

        public IgreController(UserManager<ApplicationUser> userManager, SteakContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Mines()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

            
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username; 
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A";
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/Mines/Index.cshtml");
        }

        [HttpPost]
        [Route("Igre/InsertGameState")]
        public async Task<IActionResult> InsertGameState([FromBody] GameState gameState)
        {
            if (gameState == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                
                var existingGameState = _context.GameStates
                    .FirstOrDefault(gs => gs.ID_player == gameState.ID_player && gs.ID_game == gameState.ID_game);

                if (existingGameState != null)
                {
                    
                    existingGameState.TotalWins = (existingGameState.TotalWins ?? 0) + (gameState.TotalWins ?? 0);
                    existingGameState.TotalLosses = (existingGameState.TotalLosses ?? 0) + (gameState.TotalLosses ?? 0);
                    existingGameState.TotalProfitLoss = existingGameState.TotalWins - existingGameState.TotalLosses;

                    _context.GameStates.Update(existingGameState);
                }
                else
                {
                    
                    gameState.TotalProfitLoss = (gameState.TotalWins ?? 0) - (gameState.TotalLosses ?? 0);
                    _context.GameStates.Add(gameState);
                }

                
                await _context.SaveChangesAsync();

                return Ok(new { message = "GameState processed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }



        public class UpdateBalanceRequest
        {
            public int IdPlayer { get; set; }
            public decimal SessionProfit { get; set; }
        }
        
        [HttpPost]
        [Route("Igre/PosodobiBalance")]
        public async Task<IActionResult> PosodobiBalance([FromBody] UpdateBalanceRequest request)
        {
            try
            {
                
                var player = _context.Players.FirstOrDefault(p => p.ID_player == request.IdPlayer);

                if (player == null)
                {
                    return NotFound(new { message = "Player not found." });
                }

                
                player.Balance = (player.Balance ?? 0) + request.SessionProfit;

                
                _context.Players.Update(player);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Balance updated successfully.", newBalance = player.Balance });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        


        [HttpGet]
        public IActionResult Plinko()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

            
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username; 
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A"; 
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/Plinko/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Doors()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

            
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username; 
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A"; 
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/Doors/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Slot()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

           
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username; 
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A"; 
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/Slot/Index.cshtml");
        }

        [HttpGet]
        public IActionResult HigherLower()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

            
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username; 
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A"; 
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/HigherLower/Index.cshtml");
        }

        [HttpGet]
        public IActionResult ChickenCross()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                Console.WriteLine("User not found in Identity!");
                return NotFound("User not found.");
            }

            
            var userEmail = user.Email;
            Console.WriteLine($"User Email: {userEmail}");

            
            var player = _context.Players.FirstOrDefault(p => p.Email == userEmail);

            if (player != null)
            {
                
                ViewBag.Balance = player.Balance; 
                ViewBag.Username = player.Username;
                ViewBag.PlayerId = player.ID_player; 
            }
            else
            {
                Console.WriteLine("Player not found in Players table!");
                ViewBag.Balance = "N/A"; 
                ViewBag.Username = "Unknown"; 
                ViewBag.PlayerId = -1; 
            }

            return View("~/Views/Igre/ChickenCross/Index.cshtml");
        }







    }
}
