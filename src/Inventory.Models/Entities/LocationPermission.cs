using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Entities
{
    public class LocationPermission
    {

        public int Id { get; set; }

        public bool CanWrite { get; set; }

        [Required]
        public int? LocationId { get; set; }

        public Location? Location { get; set; }

        [Required]
        public int? UserId { get; set; }

        public User? User { get; set; }

    }
}
