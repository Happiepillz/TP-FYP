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
    public class TrainingSceneStagesController : ControllerBase
    {
        private readonly MPContext _context;

        public TrainingSceneStagesController(MPContext context)
        {
            _context = context;
        }

        // GET: api/TrainingSceneStages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingSceneStage>>> GetTrainingSceneStage()
        {
            return await _context.TrainingSceneStage.ToListAsync();
        }

        // GET: api/TrainingSceneStages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSceneStage>> GetTrainingSceneStage(Guid id)
        {
            var trainingSceneStage = await _context.TrainingSceneStage.FindAsync(id);

            if (trainingSceneStage == null)
            {
                return NotFound();
            }

            return trainingSceneStage;
        }

        // PUT: api/TrainingSceneStages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingSceneStage(Guid id, TrainingSceneStage trainingSceneStage)
        {
            if (id != trainingSceneStage.TSStageID)
            {
                return BadRequest();
            }

            _context.Entry(trainingSceneStage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSceneStageExists(id))
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

        // POST: api/TrainingSceneStages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TrainingSceneStage>> PostTrainingSceneStage(TrainingSceneStage trainingSceneStage)
        {
            _context.TrainingSceneStage.Add(trainingSceneStage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingSceneStage", new { id = trainingSceneStage.TSStageID }, trainingSceneStage);
        }

        // DELETE: api/TrainingSceneStages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingSceneStage>> DeleteTrainingSceneStage(Guid id)
        {
            var trainingSceneStage = await _context.TrainingSceneStage.FindAsync(id);
            if (trainingSceneStage == null)
            {
                return NotFound();
            }

            _context.TrainingSceneStage.Remove(trainingSceneStage);
            await _context.SaveChangesAsync();

            return trainingSceneStage;
        }

        private bool TrainingSceneStageExists(Guid id)
        {
            return _context.TrainingSceneStage.Any(e => e.TSStageID == id);
        }
    }
}
