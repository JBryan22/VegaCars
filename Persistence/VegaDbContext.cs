using Microsoft.EntityFrameworkCore;
using Vega_New.Core.Models;
using Vega_New.Models;

namespace Vega_New.Persistence
{
    public class VegaDbContext : DbContext
    {
        //right now we don't need to create a dbset for models because models are part of make class. when we get makes, it returns a list of models associated with that make
        //we would need create a dbset for models only if we wanted to query them directly, which is not one of our use cases
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleId, vf.FeatureId});
        }

    }
}