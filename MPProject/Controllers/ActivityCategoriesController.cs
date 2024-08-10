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
    public class ActivityCategoriesController : ControllerBase
    {
        private readonly MPContext _context;

        public ActivityCategoriesController(MPContext context)
        {
            _context = context;
        }

        // GET: api/ActivityCategories
        [HttpGet("get/{sortOrder?}")]
        public IQueryable<ActivityCategory>  SortValues(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "ActivityCategoryName_desc";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            var contents = from categories in _context.ActivityCategory
                           select categories;

            if (descending)
            {
                contents = contents.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                contents = contents.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return contents;
        }

        // GET: api/ActivityCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityCategory>> GetActivityCategory(Guid id)
        {
            var activityCategory = await _context.ActivityCategory.FindAsync(id);

            if (activityCategory == null)
            {
                return NotFound();
            }

            return activityCategory;
        }

        // PUT: api/ActivityCategories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityCategory(Guid id, ActivityCategory activityCategory)
        {
            if (id != activityCategory.ActivityCategoryID)
            {
                return BadRequest();
            }

            _context.Entry(activityCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityCategoryExists(id))
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

        // POST: api/ActivityCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ActivityCategory>> PostActivityCategory(ActivityCategory activityCategory)
        {
            _context.ActivityCategory.Add(activityCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityCategory", new { id = activityCategory.ActivityCategoryID }, activityCategory);
        }

        // DELETE: api/ActivityCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityCategory>> DeleteActivityCategory(Guid id)
        {
            var activityCategory = await _context.ActivityCategory.FindAsync(id);
            if (activityCategory == null)
            {
                return NotFound();
            }

            _context.ActivityCategory.Remove(activityCategory);
            await _context.SaveChangesAsync();

            return activityCategory;
        }

        private bool ActivityCategoryExists(Guid id)
        {
            return _context.ActivityCategory.Any(e => e.ActivityCategoryID == id);
        }
    }
}
