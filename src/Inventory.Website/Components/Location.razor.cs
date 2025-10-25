using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Emit;

namespace Inventory.Website.Components
{
    public partial class Location(ApiConsumer apiConsumer, IJSRuntime js, AppState appState)
    {

        private readonly ApiConsumer _apiConsumer = apiConsumer;
        private readonly IJSRuntime _js = js;
        private readonly AppState _appState = appState;
        bool showExpandLoading = false;
        string? newItem = null;
        ICollection<ItemForUser>? items = null;

        [Parameter]
        public LocationForUser? Content { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Content?.IsExpanded == true)
                await SetExpandedAsync();
        }

        async Task SwapExpandAsync()
        {
            if (showExpandLoading || Content is null) return;

            try
            {
                showExpandLoading = true;
                Content.IsExpanded = !Content.IsExpanded;
                await _apiConsumer.SetLocationAsExpanded(Content.LocationId, Content.IsExpanded);
                await SetExpandedAsync();
            }
            finally
            {
                showExpandLoading = false;
            }
        }

        async Task SetExpandedAsync()
        {
            if (Content is null) return;

            showExpandLoading = true;

            items = [];
            try
            {
                if (Content.IsExpanded)
                {
                    var res = _apiConsumer.GetItemForUsersAsync(Content.LocationId);

                    await foreach (var item in res)
                        if (item is not null)
                            items.Add(item);

                    await _js.ExpandCollapseableAsync($"collapseWrapper{Content?.LocationId}");
                }
                else
                {
                    await _js.CollapseCollapseableAsync($"collapseWrapper{Content?.LocationId}");
                }
            }
            finally
            {
                showExpandLoading = false;
            }
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

        public async Task EditItemAsync(ItemForUser item)
        {
            await _appState.EditItemAsync(item);
            StateHasChanged();
        }
    }
}
