using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoeTracker.Web.Data
{
    public class ShoeTrackerDbContext : IdentityDbContext
    {
        public ShoeTrackerDbContext(DbContextOptions<ShoeTrackerDbContext> options)
            : base(options)
        {
        }
    }
}
