using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Application
    {
        public int IdApplications { get; set; }
        public int RecipientId { get; set; }
        public int SenderId { get; set; }

   }
}
