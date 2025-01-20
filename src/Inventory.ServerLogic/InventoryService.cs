using Inventory.Models;
using Inventory.Models.Entities;
using Inventory.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic
{
    public class InventoryService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public ConfiguredCancelableAsyncEnumerable<LocationForUser> GetLocationsForUserAsync(int userId, CancellationToken cancellationToken = default)
            => _dbContext.Locations
                         .Where(l => l.LocationPermissions.Any(u => u.UserId == userId))
                         .Select(l => new
                         {
                             Location = l,
                             State = l.LocationStates.Single(s => s.UserId == userId),
                         })
                         .Select(l => new LocationForUser()
                         {
                             LocationId = l.Location.Id,
                             Name = l.Location.Name,
                             IsExpanded = l.State.IsExpanded
                         })
                         .AsAsyncEnumerable().WithCancellation(cancellationToken);

        public ConfiguredCancelableAsyncEnumerable<ItemForUser> GetItemForUsersAsync(int userId, int locationId, CancellationToken cancellationToken = default)
            => _dbContext.Items
                         .Where(i => i.Location != null && i.Location.Id == locationId && i.Location.LocationPermissions.Any(p => p.UserId == userId))
                         .Select(i => new ItemForUser
                         {
                             ItemId = i.Id,
                             Name = i.Name,
                             Quantity = i.Quantity,
                         })
                        .AsAsyncEnumerable().WithCancellation(cancellationToken);

        public async Task AddLocationAsync(string name, int ownerId, CancellationToken cancellationToken = default)
        {
            var location = new Location
            {
                Name = name,
                OwnerId = ownerId,
                LocationPermissions = [
                    new()
                    {
                        UserId = ownerId,
                        CanWrite = true
                    }
                ]
            };

            await _dbContext.Locations.AddAsync(location, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddItemAsync(string name,
                                       int userId,
                                       int locationId,
                                       CancellationToken cancellationToken = default)
        {
            var isLocationValid = await _dbContext.LocationPermissions
                                                    .AnyAsync(p => p.LocationId == locationId && p.UserId == userId && p.CanWrite, 
                                                              cancellationToken: cancellationToken);

            if (!isLocationValid)
                throw new SingleErrorException("Unable to add item into this location");

            var item = new Item
            {
                Name = name,
                LocationId = locationId
            };

            await _dbContext.Items.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
