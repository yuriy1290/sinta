using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class MessageUser
    {

        public int IdMessage { get; set; }
        public string TextMessage { get; set; } = null!;
        public DateTime SendingTime { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
 }
}
