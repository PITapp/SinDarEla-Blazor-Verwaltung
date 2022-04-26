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

        bool _bolBenutzerAngemeldet;
        protected bool bolBenutzerAngemeldet
        {
            get
            {
                return _bolBenutzerAngemeldet;
            }
            set
            {
                if (!object.Equals(_bolBenutzerAngemeldet, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "bolBenutzerAngemeldet", NewValue = value, OldValue = _bolBenutzerAngemeldet };
                    _bolBenutzerAngemeldet = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Globals.PropertyChanged += OnPropertyChanged;
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            Globals.globalBenutzerName = "";

            bolBenutzerAngemeldet = false;

            if (bolBenutzerAngemeldet == true) {
            UriHelper.NavigateTo("dashboard");
            }
        }

        protected async System.Threading.Tasks.Task Login0Login(dynamic args)
        {
            UriHelper.NavigateTo("dashboard");

            Globals.globalBenutzerName = await ReadLocalStorage("storageBenutzerName");
        }
    }
}
