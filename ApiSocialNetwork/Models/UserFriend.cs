using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class UserFriend
    {
        public string UserFullName { get; set; } = null!;
        public string FriendFullName { get; set; } = null!;
    }
}
