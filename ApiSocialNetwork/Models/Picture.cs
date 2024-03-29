using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Picture
    {
      

        public int IdPicture { get; set; }
        public byte[] PhotoData { get; set; } = null!;
        public DateTime UploadDate { get; set; }

    }
}
