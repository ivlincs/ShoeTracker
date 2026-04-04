namespace ShoeTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Runtime.InteropServices;

    using ShoeTracker.Service.Core.Interfaces;

    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int shoeId, string content)
        {
            if (string.IsNullOrWhiteSpace(content) || content.Length > 500)
            {
                TempData["ErrorMessage"] = "Comment must be between 1 and 500 characters.";
                return RedirectToAction("Details", "Shoe", new { id = shoeId });
            }

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                await _commentService.AddAsync(shoeId, content, userId);
                TempData["SuccessMessage"] = "Comment added successfully!";
            }
            catch (InvalidOleVariantTypeException)
            {
                TempData["ErrorMessage"] = $"Could not add comment.";
            }

            return RedirectToAction("Details", "Shoe", new { id = shoeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int commentId, int shoeId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _commentService.DeleteAsync(commentId, userId);
            TempData["SuccessMessage"] = "Comment deleted successfully!";

            return RedirectToAction("Details", "Shoe", new { id = shoeId });
        }
    }
}
