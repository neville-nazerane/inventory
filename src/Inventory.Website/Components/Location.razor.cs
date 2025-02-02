﻿using Inventory.ClientLogic;
using Inventory.Models;
using Microsoft.AspNetCore.Components;
using System.Reflection.Emit;

namespace Inventory.Website.Components
{
    public partial class Location(ApiConsumer apiConsumer)
    {

        private readonly ApiConsumer _apiConsumer = apiConsumer;

        string? newItem = null;
        ICollection<ItemForUser>? items = null;
        string? expandedClass;

        [Parameter]
        public LocationForUser? Content { get; set; }

        async Task ExpandedAsync()
        {
            if (Content is null) return;

            Content.IsExpanded = !Content.IsExpanded;
            await _apiConsumer.SetLocationAsExpanded(Content.LocationId, Content.IsExpanded);

            if (items is not null) return;

            items = [];
            var res = _apiConsumer.GetItemForUsersAsync(Content.LocationId);

            await foreach (var item in res)
                if (item is not null)
                    items.Add(item);
        }

        async Task AddItemAsync()
        {
            if (Content is null || string.IsNullOrEmpty(newItem) || items is null) return;

            int id = await _apiConsumer.AddItemAsync(new()
            {
                LocationId = Content.LocationId,
                Name = newItem
            });

            items.Add(new()
            {
                Name = newItem,
                ItemId = id
            });
            newItem = null;
        }

        public Task IncreaseQuantityAsync(ItemForUser item) 
            => _apiConsumer.UpdateItemQuantityAsync(item.ItemId, ++item.Quantity);

        public Task DecreaseQuantityAsync(ItemForUser item)
            => _apiConsumer.UpdateItemQuantityAsync(item.ItemId, --item.Quantity);

    }
}
