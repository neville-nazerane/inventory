using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models
{
    public class ItemEditorModel
    {

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Name { get; set; }

        public string? Notes { get; set; }

    }
}
