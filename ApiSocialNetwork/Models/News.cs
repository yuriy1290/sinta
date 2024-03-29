using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class News
    {
      
        public int IdNews { get; set; }
        public string DescriptionNews { get; set; } = null!;
        public int? Likes { get; set; }
        public int? DisLike { get; set; }
        public int IdUser { get; set; }
        public int PictureId { get; set; }
        public DateTime SendingTime { get; set; }

    }
}
