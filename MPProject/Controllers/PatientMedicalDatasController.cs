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
    public class PatientMedicalDatasController : ControllerBase
    {
        private readonly MPContext _context;

        public PatientMedicalDatasController(MPContext context)
        {
            _context = context;
        }

        // GET: api/PatientMedicalDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientMedicalData>>> GetPatientMedicalDatas()
        {
            return await _context.PatientMedicalDatas.FromSqlRaw("select patientmedicaldata.*, patient.patientname from patientmedicaldata, patient where patientmedicaldata.patientid = patient.patientid").ToListAsync();
            //return await _context.PatientMedicalDatas.ToListAsync();
        }

        // GET: api/PatientMedicalDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientMedicalData>> GetPatientMedicalData(Guid id)
        {
            var patientMedicalData = await _context.PatientMedicalDatas.FromSqlInterpolated($"select patientmedicaldata.*, patient.patientname from patientmedicaldata, patient where patientmedicaldata.patientid = patient.patientid and patientmedicaldata.patientmedicaldataid = {id}").FirstOrDefaultAsync();

            if (patientMedicalData == null)
            {
                return NotFound();
            }

            return patientMedicalData;
        }

        // PUT: api/PatientMedicalDatas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientMedicalData(Guid id, PatientMedicalData patientMedicalData)
        {
            if (id != patientMedicalData.PatientMedicalDataID)
            {
                return BadRequest();
            }

            _context.Entry(patientMedicalData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientMedicalDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(patientMedicalData);
        }

        // POST: api/PatientMedicalDatas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PatientMedicalData>> PostPatientMedicalData(PatientMedicalData patientMedicalData)
        {
            _context.PatientMedicalDatas.Add(patientMedicalData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientMedicalData", new { id = patientMedicalData.PatientMedicalDataID }, patientMedicalData);
        }

        // DELETE: api/PatientMedicalDatas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientMedicalData>> DeletePatientMedicalData(Guid id)
        {
            var patientMedicalData = await _context.PatientMedicalDatas.FindAsync(id);
            if (patientMedicalData == null)
            {
                return NotFound();
            }

            _context.PatientMedicalDatas.Remove(patientMedicalData);
            await _context.SaveChangesAsync();

            return patientMedicalData;
        }

        private bool PatientMedicalDataExists(Guid id)
        {
            return _context.PatientMedicalDatas.Any(e => e.PatientMedicalDataID == id);
        }
    }
}
