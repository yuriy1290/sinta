using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Support
    {
        public int IdSupports { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int SpecialistId { get; set; }

        public virtual MessageUser Message { get; set; } = null!;
        public virtual User Specialist { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
