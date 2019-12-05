using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameServer.Models;

namespace GameServer.Controllers
{
    [Route("api/obstacles")]
    [ApiController]
    public class ObsticalesController : ControllerBase
    {
        private readonly PlayerContext _context;

        public ObsticalesController(PlayerContext context)
        {
            _context = context;
        }

        // GET: api/Obsticales
        [HttpGet]
        public IEnumerable<Obsticale> GetObsticale()
        {
            return _context.Obsticale;
        }

        // GET: api/Obsticales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetObsticale([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obsticale = await _context.Obsticale.FindAsync(id);

            if (obsticale == null)
            {
                return NotFound();
            }

            return Ok(obsticale);
        }

        // PUT: api/Obsticales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObsticale([FromRoute] long id, [FromBody] Obsticale obsticale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obsticale.id)
            {
                return BadRequest();
            }

            _context.Entry(obsticale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObsticaleExists(id))
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

        // POST: api/Obsticales
        [HttpPost]
        public async Task<IActionResult> PostObsticale([FromBody] Obsticale obsticale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Obsticale.Add(obsticale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObsticale", new { id = obsticale.id }, obsticale);
        }

        // DELETE: api/Obsticales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObsticale([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obsticale = await _context.Obsticale.FindAsync(id);
            if (obsticale == null)
            {
                return NotFound();
            }

            _context.Obsticale.Remove(obsticale);
            await _context.SaveChangesAsync();

            return Ok(obsticale);
        }

        private bool ObsticaleExists(long id)
        {
            return _context.Obsticale.Any(e => e.id == id);
        }
    }
}