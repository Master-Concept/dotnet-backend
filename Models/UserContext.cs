using Microsoft.EntityFrameworkCore;
using TodoApi.Data.ContextConfigurations;

namespace TodoApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User>? Users { get; set; }
        //From graphQL-server start
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Generate three GUIDS and place them in an arrays
            var ids = new Guid[] {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            // Apply configuration for the three contexts in our application
            // This will create the demo data for our GraphQL endpoint.
            builder.ApplyConfiguration(new SuperheroContextConfiguration(ids));
            builder.ApplyConfiguration(new SuperpowerContextConfiguration(ids));
            builder.ApplyConfiguration(new MovieContextConfiguration(ids));
        }

        // Add the DbSets for each of our models we would like at our database
        public DbSet<Superhero> Superheroes { get; set; }
        public DbSet<Superpower> Superpowers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        //From graphQL-server end
    }
}
