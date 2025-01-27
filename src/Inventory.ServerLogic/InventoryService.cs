using Inventory.Models;
using Inventory.Models.Entities;
using Inventory.Models.Exceptions;
using Inventory.ServerLogic.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.ServerLogic
{
    public class InventoryService(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public IAsyncEnumerable<LocationForUser> GetLocationsForUserAsync(int userId, CancellationToken cancellationToken = default)
            => _dbContext.Locations
                         .Where(l => l.LocationPermissions.Any(u => u.UserId == userId))
                         .Select(l => new
                         {
                             Location = l,
                             State = l.LocationStates.SingleOrDefault(s => s.UserId == userId),
                         })
                         .Select(l => new LocationForUser()
                         {
                             LocationId = l.Location.Id,
                             Name = l.Location.Name,
                             IsExpanded = l.State != null && l.State.IsExpanded
                         })
                         .AsAsyncEnumerable(cancellationToken);

        public IAsyncEnumerable<ItemForUser> GetItemForUsersAsync(int userId, int locationId, CancellationToken cancellationToken = default)
            => _dbContext.Items
                         .Where(i => i.Location != null && i.Location.Id == locationId && i.Location.LocationPermissions.Any(p => p.UserId == userId))
                         .Select(i => new ItemForUser
                         {
                             ItemId = i.Id,
                             Name = i.Name,
                             Quantity = i.Quantity,
                         })
                        .AsAsyncEnumerable(cancellationToken);

        public async Task<int> AddLocationAsync(string name, int ownerId, CancellationToken cancellationToken = default)
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

            return location.Id;
        }

        public async Task<int> AddItemAsync(AddLocationModel model,
                                       int userId,
                                       CancellationToken cancellationToken = default)
        {
            var isLocationValid = await _dbContext.LocationPermissions
                                                    .AnyAsync(p => p.LocationId == model.LocationId && p.UserId == userId && p.CanWrite, 
                                                              cancellationToken: cancellationToken);

            if (!isLocationValid)
                throw new SingleErrorException("Unable to add item into this location");

            var item = new Item
            {
                Name = model.Name,
                LocationId = model.LocationId,
            };

            await _dbContext.Items.AddAsync(item, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }

        public async Task SetLocationAsExpanded(int locationId,
                                                bool isExpanded,       
                                                int userId,
                                                CancellationToken cancellationToken = default)
        {
            await ThrowIfCantAccessLocationAsync(locationId, userId, cancellationToken);

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var updateCount = await _dbContext.LocationStates
                                                .Where(s => s.LocationId == locationId && s.UserId == userId)
                                                .ExecuteUpdateAsync(s => s.SetProperty(l => l.IsExpanded, isExpanded), cancellationToken: cancellationToken);

            if (updateCount == 0)
            {
                await _dbContext.LocationStates.AddAsync(new()
                {
                    LocationId = locationId,
                    IsExpanded = isExpanded,
                    Id = locationId
                }, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task ThrowIfCantAccessLocationAsync(int locationId, int userId, CancellationToken cancellationToken = default)
        {
            var canAccessLocation = await _dbContext.LocationPermissions
                                                    .AnyAsync(p => p.LocationId == locationId && p.UserId == userId,
                                                              cancellationToken: cancellationToken);

            if (!canAccessLocation)
                throw new SingleErrorException("Can't access this location");
        }

        public async Task ThrowIfCantWriteToLocationAsync(int locationId, int userId, CancellationToken cancellationToken = default)
        {
            var canAccessLocation = await _dbContext.LocationPermissions
                                                    .AnyAsync(p => p.LocationId == locationId && p.UserId == userId && p.CanWrite,
                                                              cancellationToken: cancellationToken);

            if (!canAccessLocation)
                throw new SingleErrorException("Can't make changes in this location");
        }

    }
}
