using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SinDarElaVerwaltung.Models.DbSinDarEla;
using SinDarElaVerwaltung.Client.Pages;

namespace SinDarElaVerwaltung.Pages
{
    public partial class AnmeldungComponent : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected GlobalsService Globals { get; set; }

        public void Dispose()
        {
            Globals.PropertyChanged -= OnPropertyChanged;
        }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected DbSinDarElaService DbSinDarEla { get; set; }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Globals.PropertyChanged += OnPropertyChanged;
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            Globals.globalBenutzerName = "";

            Globals.globalBenutzerID = "";

            Globals.globalBenutzerBaseID = "";

            Globals.globalBenutzerName = await ReadLocalStorage("storageBenutzerName");

            if (Globals.globalBenutzerName != null) {
            UriHelper.NavigateTo("dashboard");
            }
        }

        protected async System.Threading.Tasks.Task Login0Login(dynamic args)
        {
            Globals.globalBenutzerName = "Günther Painsi";
Globals.globalBenutzerID = "156";
Globals.globalBenutzerBaseID = "279";
await WriteLocalStorage("storageBenutzerName", "Günther Painsi");
await WriteLocalStorage("storageBenutzerID", "156");
await WriteLocalStorage("storageBenutzerBaseID", "279");

            UriHelper.NavigateTo("dashboard");
        }
    }
}
