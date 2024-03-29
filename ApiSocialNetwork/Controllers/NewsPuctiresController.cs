using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSocialNetwork.Models;

namespace ApiSocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPuctiresController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public NewsPuctiresController(SocialNetworkContext context)
        {
            _context = context;
        }

        // GET: api/NewsPuctires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsPuctire>>> GetNewsPuctires()
        {
          if (_context.NewsPuctires == null)
          {
              return NotFound();
          }
            return await _context.NewsPuctires.ToListAsync();
        }

        // GET: api/NewsPuctires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsPuctire>> GetNewsPuctire(int id)
        {
          if (_context.NewsPuctires == null)
          {
              return NotFound();
          }
            var newsPuctire = await _context.NewsPuctires.FindAsync(id);

            if (newsPuctire == null)
            {
                return NotFound();
            }

            return newsPuctire;
        }

        // PUT: api/NewsPuctires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsPuctire(int id, NewsPuctire newsPuctire)
        {
            if (id != newsPuctire.IdNewsPuctires)
            {
                return BadRequest();
            }

            _context.Entry(newsPuctire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsPuctireExists(id))
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

        // POST: api/NewsPuctires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsPuctire>> PostNewsPuctire(NewsPuctire newsPuctire)
        {
          if (_context.NewsPuctires == null)
          {
              return Problem("Entity set 'SocialNetworkContext.NewsPuctires'  is null.");
          }
            _context.NewsPuctires.Add(newsPuctire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsPuctire", new { id = newsPuctire.IdNewsPuctires }, newsPuctire);
        }

        // DELETE: api/NewsPuctires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsPuctire(int id)
        {
            if (_context.NewsPuctires == null)
            {
                return NotFound();
            }
            var newsPuctire = await _context.NewsPuctires.FindAsync(id);
            if (newsPuctire == null)
            {
                return NotFound();
            }

            _context.NewsPuctires.Remove(newsPuctire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsPuctireExists(int id)
        {
            return (_context.NewsPuctires?.Any(e => e.IdNewsPuctires == id)).GetValueOrDefault();
        }
    }
}
