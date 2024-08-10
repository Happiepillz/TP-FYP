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
    public class TrainingScenariosController : ControllerBase
    {
        private readonly MPContext _context;

        public TrainingScenariosController(MPContext context)
        {
            _context = context;
        }

        // GET: api/TrainingScenarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingScenario>>> GetTrainingScenario()
        {
            return await _context.TrainingScenario.ToListAsync();
        }

        // GET: api/TrainingScenarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingScenario>> GetTrainingScenario(Guid id)
        {
            var trainingScenario = await _context.TrainingScenario.FindAsync(id);

            if (trainingScenario == null)
            {
                return NotFound();
            }

            return trainingScenario;
        }

        // PUT: api/TrainingScenarios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingScenario(Guid id, TrainingScenario trainingScenario)
        {
            if (id != trainingScenario.TrainingScenarioID)
            {
                return BadRequest();
            }

            _context.Entry(trainingScenario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingScenarioExists(id))
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

        // POST: api/TrainingScenarios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TrainingScenario>> PostTrainingScenario(TrainingScenario trainingScenario)
        {
            _context.TrainingScenario.Add(trainingScenario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingScenario", new { id = trainingScenario.TrainingScenarioID }, trainingScenario);
        }

        // DELETE: api/TrainingScenarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TrainingScenario>> DeleteTrainingScenario(Guid id)
        {
            var trainingScenario = await _context.TrainingScenario.FindAsync(id);
            if (trainingScenario == null)
            {
                return NotFound();
            }

            _context.TrainingScenario.Remove(trainingScenario);
            await _context.SaveChangesAsync();

            return trainingScenario;
        }

        private bool TrainingScenarioExists(Guid id)
        {
            return _context.TrainingScenario.Any(e => e.TrainingScenarioID == id);
        }
    }
}
