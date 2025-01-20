using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class ItemForUser
    {

        public int ItemId { get; set; }

        public required string Name { get; set; }

        public int Quantity { get; set; }

    }
}
