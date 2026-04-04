namespace ShoeTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    using ShoeTracker.Data.Models.Entities;
    using ShoeTracker.Service.Core.Interfaces;

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _profileService;

        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserProfile? profile = await _profileService.GetByUserIdAsync(userId);

            profile ??= new UserProfile
            {
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            return View(profile);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            UserProfile? profile = await _profileService.GetByUserIdAsync(userId);

            profile ??= new UserProfile
            {
                UserId = userId,
            };

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfile model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            model.UserId = userId;

            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _profileService.CreateOrUpdateAsync(model);
            TempData["SuccessMessage"] = "Profile updated successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
