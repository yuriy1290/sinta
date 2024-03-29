using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class UserRolesInfo
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string LoginUser { get; set; } = null!;
        public string NameRole { get; set; } = null!;
    }
}
