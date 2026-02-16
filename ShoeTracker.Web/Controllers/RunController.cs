namespace ShoeTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;

    [Authorize]
    public class RunController : Controller
    {
        private readonly IRunService _runService;
        private readonly IShoeService _shoeService;

        public RunController(IRunService runService, IShoeService shoeService)
        {
            _runService = runService;
            _shoeService = shoeService;
        }

        [HttpGet] // Run/AddRun/Id
        public  async Task<IActionResult> AddRun(int shoeId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            Shoe? shoe = await _shoeService.GetByIdAsync(shoeId, userId);

            if (shoe == null)
            {
                return NotFound();
            }

            ViewBag.Shoe = shoe;

            return View();
        }

        [HttpPost] // Run/AddRun
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRun(int shoeId, double distance)
        {
            if (distance <= 0 || distance > 200)
            {
                ModelState.AddModelError("", "Distance must be between 0.1 and 200km");

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                Shoe? shoe = await _shoeService.GetByIdAsync(shoeId,userId);

                ViewBag.Shoe = shoe;

                return View();           
            }

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await _runService.AddRunAsync(shoeId, distance, userId);

                TempData["SuccessMessage"] = $"Succesfully added {distance:F1} km!";

                return RedirectToAction("Details", "Shoe", new {id = shoeId});
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                Shoe? shoe = await _shoeService.GetByIdAsync (shoeId,userId);

                ViewBag.Shoe = shoe;

                return View();
            }
        }
    }
}
