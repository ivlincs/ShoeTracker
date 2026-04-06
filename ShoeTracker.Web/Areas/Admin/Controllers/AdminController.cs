namespace ShoeTracker.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using ShoeTracker.Data;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ShoeTrackerDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ShoeTrackerDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            int totalShoes = await _context.Shoes.CountAsync();
            int totalRuns = await _context.Runs.CountAsync();
            int totalUsers = await _userManager.Users.CountAsync();
            int totalComments = await _context.Comments.CountAsync();

            ViewBag.TotalShoes = totalShoes;
            ViewBag.TotalRuns = totalRuns;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalComments = totalComments;

            ViewBag.RecentShoes = await _context.Shoes
                .Include(s => s.Category)
                .OrderByDescending(s => s.PurchaseDate)
                .Take(50)
                .ToListAsync();

            return View();

        }
    }
}
