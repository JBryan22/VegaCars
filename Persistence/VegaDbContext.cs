using Microsoft.EntityFrameworkCore;

namespace Vega_New.Persistence
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {
            
        }
    }
}