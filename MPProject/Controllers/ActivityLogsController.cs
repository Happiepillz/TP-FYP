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
    public class ActivityLogsController : ControllerBase
    {
        private readonly MPContext _context;
        //private MPContext db = new MPContext();

        public ActivityLogsController(MPContext context)
        {
            _context = context;
        }

        // GET: api/ActivityLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityLog>>> GetActivityLogsModel()
        {
            return await _context.ActivityLogsModel.ToListAsync();
        }

        // GET: api/ActivityLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityLog>> GetActivityLog(Guid id)
        {
            var activityLog = await _context.ActivityLogsModel.FindAsync(id);

            if (activityLog == null)
            {
                return NotFound();
            }

            return activityLog;
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

        // PUT: api/ActivityLogs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityLog(Guid id, ActivityLog activityLog)
        {
            if (id != activityLog.ActivityLogID)
            {
                return BadRequest();
            }

            _context.Entry(activityLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityLogExists(id))
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

        // POST: api/ActivityLogs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ActivityLog>> PostActivityLog(ActivityLog activityLog)
        {
            _context.ActivityLogsModel.Add(activityLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityLog", new { id = activityLog.ActivityLogID }, activityLog);
        }

        // DELETE: api/ActivityLogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActivityLog>> DeleteActivityLog(Guid id)
        {
            var activityLog = await _context.ActivityLogsModel.FindAsync(id);
            if (activityLog == null)
            {
                return NotFound();
            }

            _context.ActivityLogsModel.Remove(activityLog);
            await _context.SaveChangesAsync();

            return activityLog;
        }
        //DTO API // 2nd Api 
        [HttpGet("dto/loguser/{id}/{userid}")]
        public IQueryable<ActivityLogUserDTO> GetthingsfromLogNUser(Guid id, Guid userid)
        {
            // maybe this where clause works
            //things to display after where clause
            var contents = from al in _context.ActivityLogsModel
                           where al.TrainingInstanceID
                                 == id && al.UserId == userid 
                           
                           orderby al.ActivityDateTime descending
                           select new ActivityLogUserDTO()
                           {

                               Id = al.ActivityLogID,
                               UserName = al.User.UserName,
                               ActivityDateTime = al.ActivityDateTime,
                               ActivityDataName = al.ActivityDataName,
                               ActivityDataValue = al.ActivityDataValue,
                               ActivityStatus = al.ActivityStatus,
                               DrugName = al.Drug.DrugName,
                               TSStageName = al.TSStage.TSStageName,
                               ActivityTypeName = al.ActivityType.ActivityTypeName,
                               TrainingInstanceID = al.TrainingInstanceID
                           };
                            
                            

            //return _context.ActivityLogsModel.Where();
            return contents;
        }

        //DTO API //Replace all GUID to its value(name,date..)
       [HttpGet("dto/ActivityLog/{sortOrder?}")]
        public IQueryable<ActivityLogUserDTO> GetValues(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                 sortOrder = "ActivityDateTime_desc";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            var contents = from al in _context.ActivityLogsModel
                           select new ActivityLogUserDTO()
                           {

                               //Id = al.ActivityLogID,
                               ActivityDateTime = al.ActivityDateTime,
                               UserName = al.User.UserName,
                               ActivityDataName = al.ActivityDataName,
                               ActivityDataValue = al.ActivityDataValue,
                               ActivityStatus = al.ActivityStatus,
                               DrugName = al.Drug.DrugName,
                               TSStageName = al.TSStage.TSStageName,
                               ActivityTypeName = al.ActivityType.ActivityTypeName
                               //TrainingInstanceID = al.TrainingInstanceID
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

        private bool ActivityLogExists(Guid id)
        {
            return _context.ActivityLogsModel.Any(e => e.ActivityLogID == id);
        }


        

    }
}
