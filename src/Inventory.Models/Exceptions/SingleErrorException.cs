using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Models.Exceptions
{
    public class SingleErrorException(string message) : Exception(message)
    {
    }
}
