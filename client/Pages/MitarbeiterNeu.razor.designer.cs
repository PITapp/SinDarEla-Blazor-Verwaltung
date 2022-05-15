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
    public partial class MitarbeiterNeuComponent : ComponentBase, IDisposable
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

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> _rstBase;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> rstBase
        {
            get
            {
                return _rstBase;
            }
            set
            {
                if (!object.Equals(_rstBase, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstBase", NewValue = value, OldValue = _rstBase };
                    _rstBase = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterArten> _rstMitarbeiterArten;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.MitarbeiterArten> rstMitarbeiterArten
        {
            get
            {
                return _rstMitarbeiterArten;
            }
            set
            {
                if (!object.Equals(_rstMitarbeiterArten, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstMitarbeiterArten", NewValue = value, OldValue = _rstMitarbeiterArten };
                    _rstMitarbeiterArten = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter _dsoMitarbeiter;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter dsoMitarbeiter
        {
            get
            {
                return _dsoMitarbeiter;
            }
            set
            {
                if (!object.Equals(_dsoMitarbeiter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoMitarbeiter", NewValue = value, OldValue = _dsoMitarbeiter };
                    _dsoMitarbeiter = value;
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
            var dbSinDarElaGetBasesResult = await DbSinDarEla.GetBases(orderby:$"Name1, Name2");
            rstBase = dbSinDarElaGetBasesResult.Value.AsODataEnumerable();

            var dbSinDarElaGetMitarbeiterArtensResult = await DbSinDarEla.GetMitarbeiterArtens(orderby:$"Sortierung");
            rstMitarbeiterArten = dbSinDarElaGetMitarbeiterArtensResult.Value.AsODataEnumerable();

            dsoMitarbeiter = new SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter args)
        {
            try
            {
                var dbSinDarElaCreateMitarbeiterResult = await DbSinDarEla.CreateMitarbeiter(mitarbeiter:dsoMitarbeiter);
                DialogService.Close(dbSinDarElaCreateMitarbeiterResult);
            }
            catch (System.Exception dbSinDarElaCreateMitarbeiterException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Mitarbeiter konnte nicht erstellt werden!" });
            }
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
