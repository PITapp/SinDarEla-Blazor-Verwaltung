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

        int _intBenutzerAnzahl;
        protected int intBenutzerAnzahl
        {
            get
            {
                return _intBenutzerAnzahl;
            }
            set
            {
                if (!object.Equals(_intBenutzerAnzahl, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "intBenutzerAnzahl", NewValue = value, OldValue = _intBenutzerAnzahl };
                    _intBenutzerAnzahl = value;
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

        }

        protected async System.Threading.Tasks.Task Login0Login(dynamic args)
        {
            try
            {
                var dbSinDarElaGetBenutzersResult = await DbSinDarEla.GetBenutzers(filter:$"Benutzername eq '{args.Username}'", expand:$"Base", count:true);
                intBenutzerAnzahl = dbSinDarElaGetBenutzersResult.Count;

                if (intBenutzerAnzahl == 1) {
                    Globals.globalBenutzer = dbSinDarElaGetBenutzersResult.Value.AsODataEnumerable().FirstOrDefault();
                }

                if (intBenutzerAnzahl == 1)
                {
                    await WriteLocalStorage("storageBenutzerIDCode", Globals.globalBenutzer.BenutzerIDCode);
                }

                if (Globals.globalBenutzer.Kennwort != GetDeterministicHashCode(args.Password + Globals.globalBenutzer.BaseID))
                {
                    NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzername oder Kennwort falsch!" });
                }

                if (Globals.globalBenutzer.Kennwort == GetDeterministicHashCode(args.Password + Globals.globalBenutzer.BaseID)) {
                UriHelper.NavigateTo("dashboard");
                }
            }
            catch (System.Exception dbSinDarElaGetBenutzersException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Laden Benutzer fehlerhaft!" });
            }
        }
    }
}
