using TodoApi.Models;
namespace TodoApi.Data
{
    public class Mutation
    {
    //     private readonly ApplicationDbContext _dbContext;
    // public Mutation(ApplicationDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }
        private readonly List<Superhero> _superheroes;
        public Mutation()
        {
            _superheroes = new List<Superhero>();
        }
        public bool createSuperhero(string name, string description, double height){
            Superhero hero = new Superhero()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Height = height
            };
            _superheroes.Add(hero);
            return true;
        }
        
    }
}