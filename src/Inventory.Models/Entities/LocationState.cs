using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Entities
{
    public class LocationState
    {

        public int Id { get; set; }

        [Required]
        public int? LocationId { get; set; }

        [Required]
        public Location? Location { get; set; }

        [Required]
        public int? UserId { get; set; }

    }
}
