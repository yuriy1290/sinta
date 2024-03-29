using ApiSocialNetwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiSocialNetwork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackupController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BackupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("backup")]
        public IActionResult BackupDatabase([FromBody] BackupRequest backupRequest)
        {
            try
            {
                if (backupRequest == null || string.IsNullOrWhiteSpace(backupRequest.BackupPath))
                {
                    return BadRequest("Backup path cannot be empty.");
                }

                string connectionString = _configuration.GetConnectionString("con");

                // Вызов метода резервного копирования из предоставленного класса
                DatabaseBackup.BackupDatabase(connectionString, backupRequest.BackupPath);

                return Ok($"Backup completed successfully. Path: {backupRequest.BackupPath}");
            }
            catch (Exception ex)
            {
                // Обработка ошибок и возврат статуса ошибки
                return StatusCode(500, $"Error during backup: {ex.Message}");
            }
        }



    }
}
