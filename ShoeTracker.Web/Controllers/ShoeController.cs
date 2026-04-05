namespace ShoeTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;
    using ShoeTracker.Data.Models.Statistics;
    using ShoeTracker.Common;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    [Authorize]
    public class ShoeController : Controller
    {
        private readonly IShoeService _shoeService;
        private readonly ICommentService _commentService;
        private const int DEFAULT_PAGE_SIZE = 6;

        public ShoeController(IShoeService shoeService, ICommentService commentService)
        {
            _shoeService = shoeService;
            _commentService = commentService;
        }

        [HttpGet] // Shoe/Index
        public async Task<IActionResult> Index(string searchTerm = "", int pageNumber = 1)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ViewData["SearchTerm"] = searchTerm;

            PaginatedList<Shoe> shoes = string.IsNullOrWhiteSpace(searchTerm)
                ? await _shoeService.GetPaginatedAsync(userId, pageNumber, DEFAULT_PAGE_SIZE)
                : await _shoeService.SearchAsync(userId, searchTerm, pageNumber, DEFAULT_PAGE_SIZE);

            return View(shoes);
        }

        [HttpGet] // Shoe/Details/Id
        public async Task<IActionResult> Details(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Shoe? shoe = await _shoeService.GetByIdAsync(id, userId);

            if (shoe == null)
            {
                return NotFound();
            }

            ViewBag.Comments = await _commentService.GetByShoeIdAsync(id);

            return View(shoe);
        }

        [HttpGet] // Shoe/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _shoeService.GetAllCategoriesAsync();

            return View();
        }

        [HttpPost] // Shoe/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shoe shoeModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserId");
            ModelState.Remove("Runs");
            ModelState.Remove("Category");
            ModelState.Remove("TotalDistance");
            ModelState.Remove("Notes");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _shoeService.GetAllCategoriesAsync();

                return View(shoeModel);
            }

            shoeModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            shoeModel.TotalDistance = 0;

            await _shoeService.AddAsync(shoeModel);

            TempData["SuccessMessage"] = $"Shoe '{shoeModel.Brand} {shoeModel.Model}' added successfully!";

            return RedirectToAction(nameof(Index));

        }

        [HttpGet] // Shoe/Edit/Id
        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Shoe? shoe = await _shoeService.GetByIdAsync(id, userId);

            if (shoe == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _shoeService.GetAllCategoriesAsync();

            return View(shoe);
        }

        [HttpPost] // Shoe/Edit/Id
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shoe shoeModel)
        {
            if (id != shoeModel.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("Runs");
            ModelState.Remove("Category");
            ModelState.Remove("TotalDistance");
            ModelState.Remove("Notes");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _shoeService.GetAllCategoriesAsync();

                return View(shoeModel);
            }

            shoeModel.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _shoeService.UpdateAsync(shoeModel);

            TempData["SuccessMessage"] = $"Shoe '{shoeModel.Brand} {shoeModel.Model}' updated successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet] // Shoe/Delete/Id
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Shoe? shoe = await _shoeService.GetByIdAsync(id, userId);

            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }

        [HttpPost, ActionName("Delete")] // Shoe/Delete/Id
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _shoeService.ArchiveAsync(id);

            TempData["SuccessMessage"] = "Shoe deleted successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet] // Shoe/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            ShoeStatistics? stats = await _shoeService.GetStatisticsAsync(userId);

            return View(stats);
        }

        [HttpGet] // Shoe/Archived
        public async Task<IActionResult> Archived()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            IEnumerable<Shoe> archivedShoes = await _shoeService.GetArchivedAsync(userId);

            return View(archivedShoes);
        }
    }
}
