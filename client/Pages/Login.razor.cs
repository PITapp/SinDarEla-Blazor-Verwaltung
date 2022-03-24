using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;
using Radzen.Blazor;
using Microsoft.JSInterop;

namespace SinDarElaVerwaltung.Pages
{
    public partial class LoginComponent
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                /*
                Antwort auf E-Mail am 24.03.2022
                Hallo,
                Sie können stattdessen versuchen, document.querySelector('input').focus() auszuführen. Es konzentriert sich auf das erste <input>-Element auf der Seite, das das Feld Benutzername ist.
                Grüße, Atanas
                */
            {
                // await JSRuntime.InvokeVoidAsync("eval", $@"document.getElementById(""Username"").focus()");

                await JSRuntime.InvokeVoidAsync("eval", $@"document.querySelector('input').focus()");
 
                // System.Console.WriteLine("OnAfterRenderAsync");
            }
        }
    }
}
