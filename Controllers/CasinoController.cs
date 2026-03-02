using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class CasinoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SteakContext _context;

        public CasinoController(UserManager<ApplicationUser> userManager, SteakContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
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
            }
            else
            {
                
                ViewBag.Balance = "N/A"; 
            }

            return View();
        }



        [HttpPost]
        public IActionResult Loan()
        {
            
            var userId = _userManager.GetUserId(User); 
            var user = _userManager.FindByIdAsync(userId).Result; 

            if (user == null)
            {
                return NotFound("User not found.");
            }

            
            var player = _context.Players.FirstOrDefault(p => p.Email == user.Email);

            if (player != null)
            {
                if (player.Balance < 1)
                {
                    player.Balance += 100; 
                    _context.SaveChanges(); 
                    return Ok(new { newBalance = player.Balance });
                }
                return BadRequest("Player has sufficient balance.");
            }

            return NotFound("Player not found in database.");
        }




    }
}