namespace ShoeTracker.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return statusCode switch
            {
                404 => View("NotFound"),
                500 => View("InternalServerError"),
                _ => View("GenericError")
            };
        }
    }
}
