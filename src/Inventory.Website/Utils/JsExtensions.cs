using Auth.WebAPI.Exceptions;
using Microsoft.JSInterop;

namespace Inventory.Website.Utils
{
    public static class JsExtensions
    {

        public static ValueTask OpenModalAsync(this IJSRuntime js, string id)
            => js.InvokeVoidAsync("window.openModal", id);

        public static ValueTask CloseModalAsync(this IJSRuntime js, string id)
            => js.InvokeVoidAsync("window.closeModal", id);

        public static ValueTask ShowErrorAsync(this IJSRuntime js, string errorMsg = "Something went wrong. Try again")
            => js.InvokeVoidAsync("window.showError", errorMsg);

        public static ValueTask SaveToLocalStorageAsync(this IJSRuntime js, string key, string value)
            => js.InvokeVoidAsync("window.saveToLocalStorage", key, value);

        public static ValueTask<string> GetFromLocalStorageAsync(this IJSRuntime js, string key)
            => js.InvokeAsync<string>("window.getFromLocalStorage", key);

        public static ValueTask DeleteFromLocalStorageAsync(this IJSRuntime js, string key)
            => js.InvokeVoidAsync("window.deleteFromLocalStorage", key);

        public static ValueTask ShowErrorAsync(this IJSRuntime js, BadRequestException ex)
            => js.ShowErrorAsync(string.Join(',', ex.Messages));

    }
}
