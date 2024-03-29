using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class UserAchievement
    {
        public int IdUserAchievements { get; set; }
        public int ProgressId { get; set; }
        public int UserId { get; set; }

        public virtual Progress Progress { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
