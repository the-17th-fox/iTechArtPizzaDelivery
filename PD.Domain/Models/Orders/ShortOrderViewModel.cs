﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class ShortOrderViewModel
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long UserId { get; set; }
    }
}
