using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.JSInterop;

namespace SinDarElaVerwaltung.Pages
{
    public partial class ZzTestLocalStorageComponent
    {
        public async Task WriteLocalStorage(string key, string value)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", new object[] { key, value });
        }

        public async Task<string> ReadLocalStorage(string key)
        {
            return await JSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async Task RemoveLocalStorage(string key)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task ClearLocalStorage()
        {
            await JSRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
