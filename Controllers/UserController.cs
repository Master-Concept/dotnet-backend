using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers;

// [Route("api/[controller]")]
[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    //Reference:WeatherForecastController
    private readonly UserContext _dbContext;
    public UsersController(UserContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //GET: api/users
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }
        return _dbContext.Users.ToList();
    }

    //GET: api/users/:id
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return user;
    }
    
    //POST: api/users
    [HttpPost]
    public ActionResult<User> PostUser(User user)
    {
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        _dbContext.Users!.Add(user);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetUser), new { 
            id = user.Id
             }, user);
    }
    
    //PUT: api/users/:id
    [HttpPut("{id}")]
    public IActionResult PutUser(int id, User user)
    {
        Console.WriteLine("PutUser=id:{0}, obj:{1}",id, user.Id);
        if (id != user.Id)
        {
            return BadRequest();
        }

        try
        {
            var dbuser = _dbContext.Users.Where(p=>p.Id == id).FirstOrDefault();
            Console.WriteLine("PutUser=id:{0}, dbuser.CreatedAt:{1}, dbuser.UpdatedAt:{2}",
            id, dbuser.CreatedAt, dbuser.UpdatedAt);
            _dbContext.Entry(dbuser).State = EntityState.Modified;
            if (dbuser != null)
            {
                dbuser.Name = user.Name;
                dbuser.Username = user.Username;
                dbuser.Email = user.Email;
                dbuser.Phone = user.Phone;
                dbuser.Website = user.Website;
                dbuser.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    
    //DELETE: api/users/:id
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        if (_dbContext.Users == null)
        {
            return NotFound();
        }

        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        return NoContent();
    }
    private bool UserExists(long id)
    {
        return (_dbContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
