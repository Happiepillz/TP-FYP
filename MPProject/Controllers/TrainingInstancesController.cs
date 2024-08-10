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
    public class TrainingInstancesController : ControllerBase
    {
        private readonly MPContext _context;

        public TrainingInstancesController(MPContext context)
        {
            _context = context;
        }

        // GET: api/TrainingInstances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingInstance>>> GetTrainingInstance()
        {
            return await _context.TrainingInstance.ToListAsync();
        }

        // GET: api/TrainingInstances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingInstance>> GetTrainingInstance(Guid id)
        {
            var trainingInstance = await _context.TrainingInstance.FindAsync(id);

            if (trainingInstance == null)
            {
                return NotFound();
            }

            return trainingInstance;
        }

        // PUT: api/TrainingInstances/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingInstance(Guid id, TrainingInstance trainingInstance)
        {
            if (id != trainingInstance.TrainingInstanceID)
            {
                return BadRequest();
            }

            _context.Entry(trainingInstance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingInstanceExists(id))
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

        // POST: api/TrainingInstances
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TrainingInstance>> PostTrainingInstance(TrainingInstance trainingInstance)
        {
            _context.TrainingInstance.Add(trainingInstance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingInstance", new { id = trainingInstance.TrainingInstanceID }, trainingInstance);
        }

        // DELETE: api/TrainingInstances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingInstance>> DeleteTrainingInstance(Guid id)
        {
            var trainingInstance = await _context.TrainingInstance.FindAsync(id);
            if (trainingInstance == null)
            {
                return NotFound();
            }

            _context.TrainingInstance.Remove(trainingInstance);
            await _context.SaveChangesAsync();

            return trainingInstance;
        }

        //DTO API // 2nd Api 
        [HttpGet("dto/TngInstuser/{id}")]
        public IQueryable<TrgInstUserDto> GetthingsfromTrgInstNUser(Guid id)
        {
            // maybe this where clause works
            //things to display after where clause
            var contents = from Ti in _context.TrainingInstance
                           where Ti.user.UserId
                                 == id
                           orderby Ti.TrainingInstanceDateTime descending   
                           select new TrgInstUserDto()
                           {
                               UserName = Ti.user.UserName,
                               TrainingInstanceID = Ti.TrainingInstanceID,
                               TrainingInstanceDateTime = Ti.TrainingInstanceDateTime
                           };



            //return _context.ActivityLogsModel.Where();
            return contents;
        }

        private bool TrainingInstanceExists(Guid id)
        {
            return _context.TrainingInstance.Any(e => e.TrainingInstanceID == id);
        }
    }
}
