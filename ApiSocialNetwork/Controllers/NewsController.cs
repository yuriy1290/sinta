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
    public class NewsController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public NewsController(SocialNetworkContext context)
        {
            _context = context;
        }
        /*
                // GET: api/News
                [HttpGet]
                public async Task<ActionResult<IEnumerable<News>>> GetNews()
                {
                    if (_context.News == null)
                    {
                        return NotFound();
                    }

                    // Отсортируйте новости в порядке убывания даты
                    var orderedNews = await _context.News.OrderByDescending(news => news.IdNews).ToListAsync();

                    return orderedNews;
                }*/

        // GET: api/News?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDto>>> GetNews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (_context.News == null)
            {
                return NotFound();
            }

            var newsItems = await _context.News
              .OrderByDescending(n => n.IdNews)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .Select(n => new NewsDto
              {
                  Id = n.IdNews,
                  Description = n.DescriptionNews,
                  PictureId = n.PictureId, // Теперь передаём ID изображения
                  Likes = n.Likes ?? 0,
                  Dislikes = n.DisLike ?? 0,
                  SendingTime = n.SendingTime,
                  IdUser = n.IdUser,
                  LikedByCurrentUser = n.Likes > 0, // Если Likes больше 0, то лайк поставлен
                  DislikedByCurrentUser = n.DisLike > 0 // Если Dislikes больше 0, то дизлайк поставлен
              })
              .ToListAsync();


            return newsItems;
        }

        [HttpPut("like/{id}")]
        public async Task<IActionResult> LikeNews(int id, [FromQuery] int idUser)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.NewsId == id && l.UserId == idUser);
            var existingDislike = await _context.Dislikes.FirstOrDefaultAsync(d => d.NewsId == id && d.UserId == idUser);

            // Если уже стоит дизлайк, удаляем его
            if (existingDislike != null)
            {
                _context.Dislikes.Remove(existingDislike);
                news.DisLike--;
            }

            // Если лайк не стоит, добавляем его
            if (existingLike == null)
            {
                _context.Likes.Add(new Like { NewsId = id, UserId = idUser });
                news.Likes++;
            }

            await _context.SaveChangesAsync();

            // Возвращаем только количество лайков и дизлайков вместе с текущим состоянием пользователя
            return Ok(new { Likes = news.Likes, Dislikes = news.DisLike, LikedByCurrentUser = existingLike != null, DislikedByCurrentUser = false });
        }

        [HttpPut("dislike/{id}")]
        public async Task<IActionResult> DislikeNews(int id, [FromQuery] int idUser)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            var existingDislike = await _context.Dislikes.FirstOrDefaultAsync(d => d.NewsId == id && d.UserId == idUser);
            var existingLike = await _context.Likes.FirstOrDefaultAsync(l => l.NewsId == id && l.UserId == idUser);

            // Если уже стоит лайк, удаляем его
            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                news.Likes--;
            }

            // Если дизлайк не стоит, добавляем его
            if (existingDislike == null)
            {
                _context.Dislikes.Add(new Dislike { NewsId = id, UserId = idUser });
                news.DisLike++;
            }

            await _context.SaveChangesAsync();

            // Возвращаем только количество лайков и дизлайков вместе с текущим состоянием пользователя
            return Ok(new { Likes = news.Likes, Dislikes = news.DisLike, LikedByCurrentUser = false, DislikedByCurrentUser = existingDislike != null });
        }



        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
          if (_context.News == null)
          {
              return NotFound();
          }
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return news;
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNews(int id, [FromQuery] string newDescription)
        {
            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            news.DescriptionNews = newDescription;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
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

        // POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<News>> PostNews(News news)
        {
          if (_context.News == null)
          {
              return Problem("Entity set 'SocialNetworkContext.News'  is null.");
          }
            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNews", new { id = news.IdNews }, news);
        }

        [HttpDelete("pictures/{id}")]
        public async Task<IActionResult> DeleteNewsPictures(int id)
        {
            var newsPictures = await _context.NewsPuctires
                .Where(np => np.NewsId == id)
                .ToListAsync();

            _context.NewsPuctires.RemoveRange(newsPictures);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("likes/{id}")]
        public async Task<IActionResult> DeleteNewsLikes(int id)
        {
            var newsLikes = await _context.Likes
                .Where(l => l.NewsId == id)
                .ToListAsync();

            _context.Likes.RemoveRange(newsLikes);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            if (_context.News == null)
            {
                return NotFound();
            }
            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool NewsExists(int id)
        {
            return (_context.News?.Any(e => e.IdNews == id)).GetValueOrDefault();
        }
    }
}
