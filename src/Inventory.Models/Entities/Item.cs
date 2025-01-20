using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Entities
{
    public class Item
    {

        public int Id { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(400)]
        public string? Notes { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public Location? Location { get; set; }

        public Item()
        {
            CreatedOn = DateTime.UtcNow;
        }

    }
}
