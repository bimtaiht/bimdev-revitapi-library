﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class EntMapping
    {
        public string? type { get; set; }

        public List<EntMappingItem>? items { get; set; }
    }
}