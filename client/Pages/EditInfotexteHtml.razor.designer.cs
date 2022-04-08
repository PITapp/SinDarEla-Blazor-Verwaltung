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
    public partial class EditInfotexteHtmlComponent : ComponentBase
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

        [Parameter]
        public dynamic InfotextID { get; set; }

        SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml _infotextehtml;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml infotextehtml
        {
            get
            {
                return _infotextehtml;
            }
            set
            {
                if (!object.Equals(_infotextehtml, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "infotextehtml", NewValue = value, OldValue = _infotextehtml };
                    _infotextehtml = value;
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
            var dbSinDarElaGetInfotexteHtmlByInfotextIdResult = await DbSinDarEla.GetInfotexteHtmlByInfotextId(infotextId:InfotextID);
            infotextehtml = dbSinDarElaGetInfotexteHtmlByInfotextIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml args)
        {
            try
            {
                var dbSinDarElaUpdateInfotexteHtmlResult = await DbSinDarEla.UpdateInfotexteHtml(infotextId:InfotextID, infotexteHtml:infotextehtml);
                DialogService.Close(infotextehtml);
            }
            catch (System.Exception dbSinDarElaUpdateInfotexteHtmlException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update InfotexteHtml" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
