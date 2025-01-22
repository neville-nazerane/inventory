using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic.Utils
{
    public static class AsyncExtensions
    {

        public static async IAsyncEnumerable<TModel> AsAsyncEnumerable<TModel>(this IQueryable<TModel> q, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var res = q.AsAsyncEnumerable().WithCancellation(cancellationToken);
            await foreach (var item in res) 
                yield return item;
        }

    }
}
