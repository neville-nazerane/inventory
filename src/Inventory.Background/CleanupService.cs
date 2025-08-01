using Auth.ServerSDK;
using Inventory.ServerLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Background
{
    public class CleanupService(AuthSDK authSDK, AppDbContext dbContext)
    {
        private readonly AuthSDK _authSDK = authSDK;
        private readonly AppDbContext _dbContext = dbContext;

        public async Task CleanupNonExistingUserDataAsync(CancellationToken cancellationToken = default)
        {
            var userIds = await _dbContext.Locations.Select(l => l.OwnerId.GetValueOrDefault()).Distinct().ToListAsync(cancellationToken: cancellationToken);
            var existingUserIds = _authSDK.GetValidIdsAsync(userIds, cancellationToken);

            var removedUserIds = userIds.ToList();

            await foreach (var userId in existingUserIds)
                removedUserIds.Remove(userId);

            await _dbContext.Locations
                            .Where(l => removedUserIds.Contains(l.OwnerId.GetValueOrDefault()))
                            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
        }

    }
}
