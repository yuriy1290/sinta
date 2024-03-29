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
    public class CorrespondencesController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public CorrespondencesController(SocialNetworkContext context)
        {
            _context = context;
        }

        // GET: api/Correspondences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Correspondence>>> GetCorrespondences()
        {
          if (_context.Correspondences == null)
          {
              return NotFound();
          }
            return await _context.Correspondences.ToListAsync();
        }

        // GET: api/Correspondences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Correspondence>> GetCorrespondence(int id)
        {
          if (_context.Correspondences == null)
          {
              return NotFound();
          }
            var correspondence = await _context.Correspondences.FindAsync(id);

            if (correspondence == null)
            {
                return NotFound();
            }

            return correspondence;
        }

        // PUT: api/Correspondences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorrespondence(int id, Correspondence correspondence)
        {
            if (id != correspondence.IdCorrespondence)
            {
                return BadRequest();
            }

            _context.Entry(correspondence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorrespondenceExists(id))
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

        // POST: api/Correspondences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Correspondence>> PostCorrespondence(Correspondence correspondence)
        {
          if (_context.Correspondences == null)
          {
              return Problem("Entity set 'SocialNetworkContext.Correspondences'  is null.");
          }
            _context.Correspondences.Add(correspondence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCorrespondence", new { id = correspondence.IdCorrespondence }, correspondence);
        }

        // DELETE: api/Correspondences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorrespondence(int id)
        {
            if (_context.Correspondences == null)
            {
                return NotFound();
            }
            var correspondence = await _context.Correspondences.FindAsync(id);
            if (correspondence == null)
            {
                return NotFound();
            }

            _context.Correspondences.Remove(correspondence);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("GetMessagesByUsers")]
        public async Task<ActionResult<IEnumerable<MessageUser>>> GetMessagesByUsers(int userId, int senderId)
        {
            if (_context.MessageUsers == null || _context.Correspondences == null)
            {
                return NotFound();
            }

            var userMessages = await _context.MessageUsers
                .Where(m =>
                    (m.UserId == userId && m.SenderId == senderId) ||
                    (m.UserId == senderId && m.SenderId == userId)
                )
                .OrderBy(m => m.SendingTime)
                .ToListAsync();

            return userMessages;
        }







        private bool CorrespondenceExists(int id)
        {
            return (_context.Correspondences?.Any(e => e.IdCorrespondence == id)).GetValueOrDefault();
        }
    }
}
