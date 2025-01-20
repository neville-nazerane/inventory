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

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public IEnumerable<Item> Items { get; set; } = [];

        public IEnumerable<LocationPermission> LocationPermissions { get; set; } = [];

        public IEnumerable<LocationState> LocationStates { get; set; } = [];

        public Location()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
