﻿using System;
using System.Collections.Generic;

namespace ApiSocialNetwork.Models
{
    public partial class Token
    {
        public int TokenId { get; set; }
        public string Token1 { get; set; } = null!;
        public DateTime TokenDatetime { get; set; }
    }
}
