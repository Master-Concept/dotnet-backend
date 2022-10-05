using TodoApi.Models;
namespace TodoApi.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Superhero> GetSuperheroes([Service] ApplicationDbContext context) =>
            context.Superheroes;
        // public IQueryable<Superhero> GetSuperheroes =>
        //     new List<Superhero>().AsQueryable();
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Movie> GetMovies([Service] ApplicationDbContext context) =>
            context.Movies;
        
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Superpower> GetSuperpowers([Service] ApplicationDbContext context) =>
            context.Superpowers;
    }
}