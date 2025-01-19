using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Entities
{
    public class Location
    {

        public int Id { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public int? OwnerId { get; set; }

        public IEnumerable<Item>? Items { get; set; }

    }
}
