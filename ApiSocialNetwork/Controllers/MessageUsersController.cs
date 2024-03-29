using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSocialNetwork.Models;
using FirebaseAdmin.Messaging;

namespace ApiSocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageUsersController : ControllerBase
    {
        private readonly SocialNetworkContext _context;
        private readonly FirebaseMessaging _firebaseMessaging;
        private readonly ILogger<MessageUsersController> _logger; // Добавьте ILogger

        public MessageUsersController(SocialNetworkContext context, FirebaseMessaging firebaseMessaging, ILogger<MessageUsersController> logger)
        {
            _context = context;
            _firebaseMessaging = firebaseMessaging;
            _logger = logger;
        }

        // GET: api/MessageUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageUser>>> GetMessageUsers()
        {
          if (_context.MessageUsers == null)
          {
              return NotFound();
          }
            return await _context.MessageUsers.ToListAsync();
        }

        // Inside your controller class
        [HttpGet("GetLastMessage/{userId}")]
        public async Task<ActionResult<MessageUser>> GetLastMessage(int userId)
        {
            var lastMessage = await _context.MessageUsers
              .Where(m => m.UserId == userId || m.SenderId == userId)
              .OrderByDescending(m => m.SendingTime)
              .FirstOrDefaultAsync();

            if (lastMessage == null)
            {
                return NotFound();
            }

            return lastMessage;
        }



        // GET: api/MessageUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageUser>> GetMessageUser(int id)
        {
          if (_context.MessageUsers == null)
          {
              return NotFound();
          }
            var messageUser = await _context.MessageUsers.FindAsync(id);

            if (messageUser == null)
            {
                return NotFound();
            }

            return messageUser;
        }

        // PUT: api/MessageUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageUser(int id, MessageUser messageUser)
        {
            if (id != messageUser.IdMessage)
            {
                return BadRequest();
            }

            _context.Entry(messageUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageUserExists(id))
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

        // POST: api/MessageUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageUser>> PostMessageUser(MessageUser messageUser)
        {
            if (_context.MessageUsers == null)
            {
                return Problem("Entity set 'SocialNetworkContext.MessageUsers' is null.");
            }

            _context.MessageUsers.Add(messageUser);
            await _context.SaveChangesAsync();

            var sender = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == messageUser.UserId);

            if (sender != null)
            {
                await SendNotification(messageUser.UserId, $"{sender.FirstName} {sender.LastName}", messageUser.TextMessage);
            }

            return CreatedAtAction("GetMessageUser", new { id = messageUser.IdMessage }, messageUser);
        }

        private async Task SendNotification(int recipientId, string title, string body)
        {
            try
            {
                // Получение FCM токена получателя из вашей базы данных
                var recipient = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == recipientId);
                if (recipient == null || string.IsNullOrEmpty(recipient.FcmToken))
                {
                    // Добавьте лог, чтобы уведомить о том, что получатель не найден или у него отсутствует токен
                    _logger.LogInformation("Получатель не найден или у него отсутствует FCM токен.");
                    return;
                }

                var message = new Message
                {
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                    },
                 /*   Data = new Dictionary<string, string>
                    {
                        { "messageText", messageText }
                    },*/
                    Token = recipient.FcmToken
                };



                // Добавьте лог перед отправкой уведомления
                _logger.LogInformation($"Отправка уведомления: {title}, {body}");

                await _firebaseMessaging.SendAsync(message);

                // Добавьте лог после успешной отправки уведомления
                _logger.LogInformation("Уведомление успешно отправлено.");
            }
            catch (Exception ex)
            {
                // Добавьте лог при ошибке отправки уведомления
                _logger.LogError($"Ошибка при отправке уведомления: {ex.Message}");
            }
        }




        // DELETE: api/MessageUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageUser(int id)
        {
            if (_context.MessageUsers == null)
            {
                return NotFound();
            }
            var messageUser = await _context.MessageUsers.FindAsync(id);
            if (messageUser == null)
            {
                return NotFound();
            }

            _context.MessageUsers.Remove(messageUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageUserExists(int id)
        {
            return (_context.MessageUsers?.Any(e => e.IdMessage == id)).GetValueOrDefault();
        }
    }
}
