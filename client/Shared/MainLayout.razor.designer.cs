using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SinDarElaVerwaltung.Models.DbSinDarEla;
using Microsoft.AspNetCore.Identity;
using SinDarElaVerwaltung.Models;
using SinDarElaVerwaltung.Pages;

namespace SinDarElaVerwaltung.Layouts
{
    public partial class MainLayoutComponent : LayoutComponentBase
    {
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
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected DbSinDarElaService DbSinDarEla { get; set; }

        protected RadzenBody body0;
        protected RadzenSidebar sidebar0;

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwBenutzerBase> _rstBenutzer;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwBenutzerBase> rstBenutzer
        {
            get
            {
                return _rstBenutzer;
            }
            set
            {
                if(!object.Equals(_rstBenutzer, value))
                {
                    _rstBenutzer = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        string _strBildURL;
        protected string strBildURL
        {
            get
            {
                return _strBildURL;
            }
            set
            {
                if(!object.Equals(_strBildURL, value))
                {
                    _strBildURL = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        private void Authenticated()
        {
             StateHasChanged();
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
             if (Security != null)
             {
                  Security.Authenticated += Authenticated;

                  await Security.InitializeAsync(AuthenticationStateProvider);
             }
             await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            try
            {
                if (Security.IsAuthenticated())
                {
                    var dbSinDarElaGetVwBenutzerBasesResult = await DbSinDarEla.GetVwBenutzerBases(filter:$"Benutzername eq '{Security.User.UserName}'");
                    rstBenutzer = dbSinDarElaGetVwBenutzerBasesResult.Value.AsODataEnumerable();

                    strBildURL = rstBenutzer.FirstOrDefault().BildURL;
                }
            }
            catch (System.Exception dbSinDarElaGetVwBenutzerBasesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzer wurde nicht gefunden!" });
            }
        }

        protected async System.Threading.Tasks.Task SidebarToggle0Click(dynamic args)
        {
            await InvokeAsync(() => { sidebar0.Toggle(); });

            await InvokeAsync(() => { body0.Toggle(); });
        }

        protected async System.Threading.Tasks.Task Profilemenu1Click(dynamic args)
        {
            if (args.Text == "Logout")
            {
                Security.Logout();
            }
        }
    }
}
