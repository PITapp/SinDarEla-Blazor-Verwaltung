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
    public partial class KundenComponent : ComponentBase, IDisposable
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected GlobalsService Globals { get; set; }

        partial void OnDispose();

        public void Dispose()
        {
            Globals.PropertyChanged -= OnPropertyChanged;
            OnDispose();
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.Base> grid0;

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> _getBasesResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> getBasesResult
        {
            get
            {
                return _getBasesResult;
            }
            set
            {
                if (!object.Equals(_getBasesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getBasesResult", NewValue = value, OldValue = _getBasesResult };
                    _getBasesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getBasesCount;
        protected int getBasesCount
        {
            get
            {
                return _getBasesCount;
            }
            set
            {
                if (!object.Equals(_getBasesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getBasesCount", NewValue = value, OldValue = _getBasesCount };
                    _getBasesCount = value;
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
            if (Globals.globalBenutzer == null) {
            UriHelper.NavigateTo("anmeldung-laden");
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Drucken ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetBasesResult = await DbSinDarEla.GetBases(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getBasesResult = dbSinDarElaGetBasesResult.Value.AsODataEnumerable();

                getBasesCount = dbSinDarElaGetBasesResult.Count;
            }
            catch (System.Exception dbSinDarElaGetBasesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Bases" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.Base args)
        {
            UriHelper.NavigateTo($"edit-base-2/{args.BaseID}");
        }
    }
}
