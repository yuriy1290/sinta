using ApiSocialNetwork.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly SocialNetworkContext _context;

        public ExportController(SocialNetworkContext context)
        {
            _context = context;
        }

        // GET: api/Export/userscsv
        [HttpGet("userscsv")]
        public async Task<IActionResult> ExportUsersToCsv()
        {
            try
            {
                var users = await _context.Users.Include(u => u.Role).ToListAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found for export.");
                }

                var configuration = new CsvConfiguration(new System.Globalization.CultureInfo("ru-RU"));
                var csvContent = new StringBuilder();

                using (var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.GetEncoding("Windows-1251")))
                using (var csvWriter = new CsvWriter(streamWriter, configuration))
                {
                    csvWriter.WriteRecords(users);
                    streamWriter.Flush();

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var streamReader = new StreamReader(memoryStream, Encoding.GetEncoding("Windows-1251")))
                    {
                        csvContent.Append(streamReader.ReadToEnd());
                    }
                }






                var fileName = $"users_export_{DateTime.Now:yyyyMMddHHmmss}.csv";
                var csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

                return File(csvBytes, "text/csv", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error during CSV export: {ex.Message}");
            }
        }



        private string GenerateCsvContent(List<User> users)
        {
            // Создание строк CSV из данных
            var csvLines = new List<string>
    {
        "ID пользователя;Имя;Фамилия;Отчество;Логин;Пароль;Соль;ID фото;Количество сообщений;Время в приложении;ID роли;Название роли" // Заголовки
    };

            foreach (var user in users)
            {
                var line = $"{user.IdUser};" +
                           $"{EscapeCsvString(user.FirstName)};" +
                           $"{EscapeCsvString(user.LastName)};" +
                           $"{EscapeCsvString(user.MiddleName)};" +
                           $"{EscapeCsvString(user.LoginUser)};" +
                           $"{EscapeCsvString(user.PasswordUser)};" +
                           $"{EscapeCsvString(user.Salt)};" +
                           $"{user.PhotoId};" +
                           $"{user.NumberOfMessages};" +
                           $"{user.TimeInTheApp};" +
                           $"{user.RoleId};" +
                           $"{EscapeCsvString(user.Role?.NameRole)}";

                csvLines.Add(line);
            }

            // Соединение строк CSV
            var csvContent = string.Join(Environment.NewLine, csvLines);

            return csvContent;
        }

        private string EscapeCsvString(string input)
        {
            // Экранирование символов, если они содержат точку с запятой
            return input?.Replace(";", "") ?? "";
        }


    }
}
