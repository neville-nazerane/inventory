using Inventory.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class LocationForUser
    {

        public int LocationId { get; set; }

        public required string Name { get; set; }

        public bool IsExpanded { get; set; }

    }
}
