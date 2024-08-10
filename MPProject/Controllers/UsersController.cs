using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPProject.Data;
using MPProject.Models;

namespace MPProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MPContext _context;

        public UsersController(MPContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.UserModel.ToListAsync();
        }

        [HttpGet("get/{sortOrder?}")]
        public IQueryable<User> SortUser(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "UserName_desc";

            }
            bool descending = false;
            if (sortOrder.EndsWith("desc"))

            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            var contents = from user in _context.UserModel
                           select user;
            if (descending)
            {
                contents = contents.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                contents = contents.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            //return await _context.UserModel.ToListAsync();
            return contents;
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.UserModel.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        //GET: 
        [HttpGet("getuser/{username}")]
        public User GetUserByName(string username)
        {
            User user = new User();

            try
            {
                user = _context.UserModel.SingleOrDefault(u => u.UserName == username);
            }
            catch
            {
                user = null;
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.UserModel.FindAsync(id);
                    if (user == null)
            {
                return NotFound();
            }

            _context.UserModel.Remove(user);
            //await _context.SaveChangesAsync();

            _context.SaveChanges();
            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.UserModel.Any(e => e.UserId == id);
        }

        [HttpGet("search/{strings}")]
        public IQueryable<User> SearchUser(string strings)
        {
            var contents2 = _context.UserModel.Where(u => u.UserName.Contains(strings));

            var contents = from u in _context.UserModel
                           where u.UserName.Contains(strings)
                           select u;


            return contents2;
        }


    }
    
}
