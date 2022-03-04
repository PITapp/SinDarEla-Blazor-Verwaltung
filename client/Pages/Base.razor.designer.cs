using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SinDarElaVerwaltung.Models.DbSinDarEla;
using Microsoft.AspNetCore.Identity;
using SinDarElaVerwaltung.Models;
using SinDarElaVerwaltung.Client.Pages;

namespace SinDarElaVerwaltung.Pages
{
    public partial class BaseComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

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
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

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
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {

        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddBase>("Add Base", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
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
            var dialogResult = await DialogService.OpenAsync<EditBase>("Edit Base", new Dictionary<string, object>() { {"BaseID", args.BaseID} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }
    }
}
