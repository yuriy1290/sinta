using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class User
    {
    

        public int IdUser { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }

        public string LoginUser { get; set; } = null!;

        public string PasswordUser { get; set; } = null!;
        public string? Salt { get; set; }
        public int? PhotoId { get; set; }
        public int? NumberOfMessages { get; set; }
        public TimeSpan? TimeInTheApp { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string? FcmToken { get; set; } // Добавьте это поле для хранения FCM-токена

        public ICollection<FcmToken> FcmTokens { get; set; } = new List<FcmToken>(); // Навигационное свойство для связи с токенами
    }
}
