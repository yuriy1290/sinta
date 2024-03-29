using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Correspondence
    {
        public int IdCorrespondence { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
  }
}
