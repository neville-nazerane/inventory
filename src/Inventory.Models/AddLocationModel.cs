﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class AddLocationModel
    {

        public int LocationId { get; set; }

        public required string Name { get; set; }

    }
}
