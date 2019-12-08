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
    [Route("api/[controller]")]
    [ApiController]
    public class BulletsController : ControllerBase
    {
        private readonly PlayerContext _context;

        public BulletsController(PlayerContext context)
        {
            _context = context;
        }

        // GET: api/Bullets
        [HttpGet]
        public IEnumerable<Bullet> GetBullet()
        {
            return _context.Bullet;
        }

        // GET: api/Bullets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBullet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bullet = await _context.Bullet.FindAsync(id);

            if (bullet == null)
            {
                return NotFound();
            }

            return Ok(bullet);
        }

        // PUT: api/Bullets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBullet([FromRoute] int id, [FromBody] Bullet bullet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bullet.bulletID)
            {
                return BadRequest();
            }

            _context.Entry(bullet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BulletExists(id))
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

        // POST: api/Bullets
        [HttpPost]
        public async Task<IActionResult> PostBullet([FromBody] Bullet bullet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bullet.Add(bullet);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Bullets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBullet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bullet = await _context.Bullet.FindAsync(id);
            if (bullet == null)
            {
                return NotFound();
            }

            _context.Bullet.Remove(bullet);
            await _context.SaveChangesAsync();

            return Ok(bullet);
        }

        private bool BulletExists(int id)
        {
            return _context.Bullet.Any(e => e.bulletID == id);
        }
    }
}