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
    public partial class EditBaseKontakteComponent : ComponentBase
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
        public dynamic KontaktID { get; set; }

        SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte _basekontakte;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte basekontakte
        {
            get
            {
                return _basekontakte;
            }
            set
            {
                if (!object.Equals(_basekontakte, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "basekontakte", NewValue = value, OldValue = _basekontakte };
                    _basekontakte = value;
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
            var dbSinDarElaGetBaseKontakteByKontaktIdResult = await DbSinDarEla.GetBaseKontakteByKontaktId(kontaktId:Convert.ChangeType(KontaktID, Type.GetTypeCode(typeof(int))));
            basekontakte = dbSinDarElaGetBaseKontakteByKontaktIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SinDarElaVerwaltung.Models.DbSinDarEla.BaseKontakte args)
        {
            try
            {
                var dbSinDarElaUpdateBaseKontakteResult = await DbSinDarEla.UpdateBaseKontakte(kontaktId:Convert.ChangeType(KontaktID, Type.GetTypeCode(typeof(int))), baseKontakte:basekontakte);
                UriHelper.NavigateTo("base-kontaktes");
            }
            catch (System.Exception dbSinDarElaUpdateBaseKontakteException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update BaseKontakte" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("base-kontaktes");
        }
    }
}
