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
    public partial class InfotexteHtmlComponent : ComponentBase
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> grid0;

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> _getInfotexteHtmlsResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> getInfotexteHtmlsResult
        {
            get
            {
                return _getInfotexteHtmlsResult;
            }
            set
            {
                if (!object.Equals(_getInfotexteHtmlsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getInfotexteHtmlsResult", NewValue = value, OldValue = _getInfotexteHtmlsResult };
                    _getInfotexteHtmlsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getInfotexteHtmlsCount;
        protected int getInfotexteHtmlsCount
        {
            get
            {
                return _getInfotexteHtmlsCount;
            }
            set
            {
                if (!object.Equals(_getInfotexteHtmlsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getInfotexteHtmlsCount", NewValue = value, OldValue = _getInfotexteHtmlsCount };
                    _getInfotexteHtmlsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddInfotexteHtml>("Add Infotexte Html", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetInfotexteHtmlsResult = await DbSinDarEla.GetInfotexteHtmls(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getInfotexteHtmlsResult = dbSinDarElaGetInfotexteHtmlsResult.Value.AsODataEnumerable();

                getInfotexteHtmlsCount = dbSinDarElaGetInfotexteHtmlsResult.Count;
            }
            catch (System.Exception dbSinDarElaGetInfotexteHtmlsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load InfotexteHtmls" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml args)
        {
            var dialogResult = await DialogService.OpenAsync<EditInfotexteHtml>("Edit Infotexte Html", new Dictionary<string, object>() { {"InfotextID", args.InfotextID} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var dbSinDarElaDeleteInfotexteHtmlResult = await DbSinDarEla.DeleteInfotexteHtml(infotextId:data.InfotextID);
                    if (dbSinDarElaDeleteInfotexteHtmlResult != null && dbSinDarElaDeleteInfotexteHtmlResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (dbSinDarElaDeleteInfotexteHtmlResult != null && dbSinDarElaDeleteInfotexteHtmlResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete InfotexteHtml" });
                    }
                }
            }
            catch (System.Exception dbSinDarElaDeleteInfotexteHtmlException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete InfotexteHtml" });
            }
        }
    }
}
