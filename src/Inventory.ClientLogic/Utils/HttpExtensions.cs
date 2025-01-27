using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic.Utils
{
    internal static class HttpExtensions
    {

        internal static async Task<int> ParseIntAsync(this HttpResponseMessage response, CancellationToken cancellationToken = default) 
            => int.Parse(await response.Content.ReadAsStringAsync(cancellationToken));

    }
}
