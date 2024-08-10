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
    public class ActivityTypesController : ControllerBase
    {
        private readonly MPContext _context;

        public ActivityTypesController(MPContext context)
        {
            _context = context;
        }

        // GET: api/ActivityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityType>>> GetActivityType()
        {
            return await _context.ActivityType.ToListAsync();
        }

        // GET: api/ActivityTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityType>> GetActivityType(Guid id)
        {
            var activityType = await _context.ActivityType.FindAsync(id);

            if (activityType == null)
            {
                return NotFound();
            }

            return activityType;
        }

        // PUT: api/ActivityTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityType(Guid id, ActivityType activityType)
        {
            if (id != activityType.ActivityTypeID)
            {
                return BadRequest();
            }

            _context.Entry(activityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityTypeExists(id))
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

        // POST: api/ActivityTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ActivityType>> PostActivityType(ActivityType activityType)
        {
            try
            {
                _context.ActivityType.Add(activityType);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

            return CreatedAtAction("GetActivityType", new { id = activityType.ActivityTypeID }, activityType);
        }

        // DELETE: api/ActivityTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityType>> DeleteActivityType(Guid id)
        {
            var activityType = await _context.ActivityType.FindAsync(id);
            if (activityType == null)
            {
                return NotFound();
            }

            _context.ActivityType.Remove(activityType);
            await _context.SaveChangesAsync();

            return activityType;
        }

        private bool ActivityTypeExists(Guid id)
        {
            return _context.ActivityType.Any(e => e.ActivityTypeID == id);
        }


        [HttpGet("dto/ActivityType2/{sortOrder?}")]
        public IQueryable<TypeCategoryDTO> SortValues(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "ActivityTypeName_desc";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            var contents = from al in _context.ActivityType
                           select new TypeCategoryDTO()
                           {
                               ActivityCategoryName = al.ActivityCategory.ActivityCategoryName,
                               ActivityTypeID = al.ActivityTypeID,
                               ActivityTypeName = al.ActivityTypeName,
                               ActivityTypeDescription = al.ActivityTypeDescription

                           };



            //return _context.ActivityLogsModel.Where();

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
    }
}
