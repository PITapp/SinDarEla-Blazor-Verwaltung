using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace SinDarElaVerwaltung.Pages
{
    public partial class AbmeldungComponent
    {
        public async Task RemoveLocalStorage(string key)
        {
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
