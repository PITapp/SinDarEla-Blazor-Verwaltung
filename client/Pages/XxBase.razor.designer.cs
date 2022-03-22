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
    public partial class XxBaseComponent : ComponentBase
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte> grid1;

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

        SinDarElaVerwaltung.Models.DbSinDarEla.Base _master;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Base master
        {
            get
            {
                return _master;
            }
            set
            {
                if (!object.Equals(_master, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "master", NewValue = value, OldValue = _master };
                    _master = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _lastFilter;
        protected string lastFilter
        {
            get
            {
                return _lastFilter;
            }
            set
            {
                if (!object.Equals(_lastFilter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "lastFilter", NewValue = value, OldValue = _lastFilter };
                    _lastFilter = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte> _BaseKontaktes;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte> BaseKontaktes
        {
            get
            {
                return _BaseKontaktes;
            }
            set
            {
                if (!object.Equals(_BaseKontaktes, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "BaseKontaktes", NewValue = value, OldValue = _BaseKontaktes };
                    _BaseKontaktes = value;
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
            UriHelper.NavigateTo("add-xx-base");
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

        protected async void Grid0Render(DataGridRenderEventArgs<SinDarElaVerwaltung.Models.DbSinDarEla.Base> args)
        {
            if (grid0.Query.Filter != lastFilter) {
                master = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter)
            {
                await grid0.SelectRow(master);
            }

            lastFilter = grid0.Query.Filter;
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<SinDarElaVerwaltung.Models.DbSinDarEla.Base> args)
        {
            UriHelper.NavigateTo($"edit-xx-base/{args.Data.BaseID}");
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.Base args)
        {
            master = args;

            if (args == null) {
                BaseKontaktes = null;
            }

            if (args != null)
            {
                var dbSinDarElaGetBaseKontaktesResult = await DbSinDarEla.GetBaseKontaktes(filter:$"BaseID eq {args.BaseID}");
                BaseKontaktes = dbSinDarElaGetBaseKontaktesResult.Value;
            }
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var dbSinDarElaDeleteBaseResult = await DbSinDarEla.DeleteBase(baseId:data.BaseID);
                    if (dbSinDarElaDeleteBaseResult != null && dbSinDarElaDeleteBaseResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (dbSinDarElaDeleteBaseResult != null && dbSinDarElaDeleteBaseResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Base" });
                    }
                }
            }
            catch (System.Exception dbSinDarElaDeleteBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Base" });
            }
        }

        protected async System.Threading.Tasks.Task BaseKontakteAddButtonClick(MouseEventArgs args)
        {
            UriHelper.NavigateTo($"add-base-kontakte/{master.BaseID}");
        }

        protected async System.Threading.Tasks.Task Grid1RowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte args)
        {
            UriHelper.NavigateTo($"edit-base-kontakte/{args.KontaktID}");
        }

        protected async System.Threading.Tasks.Task BaseKontakteDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var dbSinDarElaDeleteBaseKontakteResult = await DbSinDarEla.DeleteBaseKontakte(kontaktId:data.KontaktID);
                    await Grid0RowSelect(master);

                    if (dbSinDarElaDeleteBaseKontakteResult != null)
                    {
                        await grid1.Reload();
                    }
                }
            }
            catch (System.Exception dbSinDarElaDeleteBaseKontakteException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Base" });
            }
        }
    }
}
