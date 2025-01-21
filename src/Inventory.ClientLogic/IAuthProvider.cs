using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public interface IAuthProvider
    {

        ValueTask<string> GetJwtAsync();

    }
}
