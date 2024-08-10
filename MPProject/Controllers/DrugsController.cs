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
    public class DrugsController : ControllerBase
    {
        private readonly MPContext _context;

        public DrugsController(MPContext context)
        {
            _context = context;
        }

        // GET: api/Drugs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Drug>>> GetDrug()
        //{
        //    return await _context.Drug.ToListAsync();
        //}

        [HttpGet("Sort/{sortOrder?}")]
        public IQueryable<Drug> GetValues(string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "DrugName_desc";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }
            var contents = from al in _context.Drug
                           select al;
                           

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

        // GET: api/Drugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drug>> GetDrug(Guid id)
        {
            var drug = await _context.Drug.FindAsync(id);

            if (drug == null)
            {
                return NotFound();
            }

            return drug;
        }


        // PUT: api/Drugs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrug(Guid id, Drug drug)
        {
            if (id != drug.DrugId)
            {
                return BadRequest();
            }

            _context.Entry(drug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugExists(id))
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

        // POST: api/Drugs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Drug>> PostDrug(Drug drug)
        {
            _context.Drug.Add(drug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrug", new { id = drug.DrugId }, drug);
        }

        // DELETE: api/Drugs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drug>> DeleteDrug(Guid id)
        {
            var drug = await _context.Drug.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }

            _context.Drug.Remove(drug);
            await _context.SaveChangesAsync();

            return drug;
        }

        private bool DrugExists(Guid id)
        {
            return _context.Drug.Any(e => e.DrugId == id);
        }
    }
}
