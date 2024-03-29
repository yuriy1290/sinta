using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Progress
    {
        public Progress()
        {
            UserAchievements = new HashSet<UserAchievement>();
        }

        public int IdProgress { get; set; }
        public string NameProgress { get; set; } = null!;
        public string DescriptionProgress { get; set; } = null!;
        public int IdPicture { get; set; }

        public virtual Picture IdPictureNavigation { get; set; } = null!;
        public virtual ICollection<UserAchievement> UserAchievements { get; set; }
    }
}
