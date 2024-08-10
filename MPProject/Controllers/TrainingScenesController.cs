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
    public class TrainingScenesController : ControllerBase
    {
        private readonly MPContext _context;

        public TrainingScenesController(MPContext context)
        {
            _context = context;
        }

        // GET: api/TrainingScenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingScene>>> GetTrainingScene()
        {
            return await _context.TrainingScene.ToListAsync();
        }

        // GET: api/TrainingScenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingScene>> GetTrainingScene(Guid id)
        {
            var trainingScene = await _context.TrainingScene.FindAsync(id);

            if (trainingScene == null)
            {
                return NotFound();
            }

            return trainingScene;
        }

        // PUT: api/TrainingScenes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingScene(Guid id, TrainingScene trainingScene)
        {
            if (id != trainingScene.TrainingSceneID)
            {
                return BadRequest();
            }

            _context.Entry(trainingScene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSceneExists(id))
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

        // POST: api/TrainingScenes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TrainingScene>> PostTrainingScene(TrainingScene trainingScene)
        {
            _context.TrainingScene.Add(trainingScene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingScene", new { id = trainingScene.TrainingSceneID }, trainingScene);
        }

        // DELETE: api/TrainingScenes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingScene>> DeleteTrainingScene(Guid id)
        {
            var trainingScene = await _context.TrainingScene.FindAsync(id);
            if (trainingScene == null)
            {
                return NotFound();
            }

            _context.TrainingScene.Remove(trainingScene);
            await _context.SaveChangesAsync();

            return trainingScene;
        }

        private bool TrainingSceneExists(Guid id)
        {
            return _context.TrainingScene.Any(e => e.TrainingSceneID == id);
        }
    }
}
