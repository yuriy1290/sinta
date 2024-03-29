using ApiSocialNetwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// В вашем контроллере или сервисе
namespace ApiSocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FcmTokensController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public FcmTokensController(SocialNetworkContext context)
        {
            _context = context;
        }

        // В вашем контроллере, отвечающем за работу с токенами
        [HttpPost("save-token")]
        public async Task<IActionResult> SaveToken([FromBody] SaveTokenRequest request)
        {
            try
            {
                // Ваш код для сохранения токена в базе данных
                var user = await _context.Users
                    .Where(u => u.IdUser == request.UserId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    // Обновите FcmToken для пользователя
                    user.FcmToken = request.Token;
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Токен успешно сохранен." });
                }

                return NotFound(new { Message = "Пользователь не найден." });
            }
            catch (Exception ex)
            {
                // Обработка ошибок при сохранении токена
                return StatusCode(500, new { Error = "Внутренняя ошибка сервера." });
            }
        }


        // Модель запроса для сохранения токена
        public class SaveTokenRequest
        {
            public int UserId { get; set; }
            public string Token { get; set; }
        }

    }
}

