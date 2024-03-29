using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class NewsLikesInfo
    {
        public int IdNews { get; set; }
        public string DescriptionNews { get; set; } = null!;
        public int? Likes { get; set; }
        public int? DisLike { get; set; }
        public string UserName { get; set; } = null!;
    }
}
