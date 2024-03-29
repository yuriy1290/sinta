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
    public class PicturesController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public PicturesController(SocialNetworkContext context)
        {
            _context = context;
        }

        // GET: api/Pictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            if (_context.Pictures == null)
            {
                return NotFound();
            }

            // Отсортируйте изображения в порядке убывания даты
            var orderedPictures = await _context.Pictures.OrderByDescending(picture => picture.IdPicture).ToListAsync();

            return orderedPictures;
        }


        /*  // GET: api/Pictures/5
          [HttpGet("{id}")]
          public async Task<ActionResult<Picture>> GetPicture(int id)
          {
            if (_context.Pictures == null)
            {
                return NotFound();
            }
              var picture = await _context.Pictures.FindAsync(id);

              if (picture == null)
              {
                  return NotFound();
              }

              return picture;
          }*/
        [HttpGet("{pictureId}")]
        public IActionResult GetPicture(int pictureId)
        {
            var picture = _context.Pictures.Find(pictureId);

            if (picture != null)
            {
                return File(picture.PhotoData, "image/jpeg"); // Используйте соответствующий MIME-тип
            }

            return NotFound(); // Вернуть 404, если нет изображения
        }


        // PUT: api/Pictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.IdPicture)
            {
                return BadRequest();
            }

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            try
            {
                using (var dbContext = new SocialNetworkContext())
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        byte[] photoData = stream.ToArray();

                        var picture = new Picture
                        {
                            PhotoData = photoData, // Binary image data
                            UploadDate = DateTime.Now // Upload date
                        };

                        dbContext.Pictures.Add(picture);
                        dbContext.SaveChanges();

                        Console.WriteLine($"Picture added successfully. ID: {picture.IdPicture}");

                        return CreatedAtAction("GetPicture", new { pictureId = picture.IdPicture }, picture);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PostPicture: {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }



        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            if (_context.Pictures == null)
            {
                return NotFound();
            }
            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PictureExists(int id)
        {
            return (_context.Pictures?.Any(e => e.IdPicture == id)).GetValueOrDefault();
        }
    }
}
