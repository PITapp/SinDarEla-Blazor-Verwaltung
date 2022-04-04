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
    public partial class BenutzerprofilComponent : ComponentBase
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

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwRollen> _rstRollen;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwRollen> rstRollen
        {
            get
            {
                return _rstRollen;
            }
            set
            {
                if (!object.Equals(_rstRollen, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstRollen", NewValue = value, OldValue = _rstRollen };
                    _rstRollen = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Benutzer> _rstBenutzer;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Benutzer> rstBenutzer
        {
            get
            {
                return _rstBenutzer;
            }
            set
            {
                if (!object.Equals(_rstBenutzer, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstBenutzer", NewValue = value, OldValue = _rstBenutzer };
                    _rstBenutzer = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SinDarElaVerwaltung.Models.DbSinDarEla.Benutzer _dsoBenutzer;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Benutzer dsoBenutzer
        {
            get
            {
                return _dsoBenutzer;
            }
            set
            {
                if (!object.Equals(_dsoBenutzer, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoBenutzer", NewValue = value, OldValue = _dsoBenutzer };
                    _dsoBenutzer = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _strNameKontakt;
        protected string strNameKontakt
        {
            get
            {
                return _strNameKontakt;
            }
            set
            {
                if (!object.Equals(_strNameKontakt, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strNameKontakt", NewValue = value, OldValue = _strNameKontakt };
                    _strNameKontakt = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        ApplicationUser _dsoUser;
        protected ApplicationUser dsoUser
        {
            get
            {
                return _dsoUser;
            }
            set
            {
                if (!object.Equals(_dsoUser, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoUser", NewValue = value, OldValue = _dsoUser };
                    _dsoUser = value;
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
            var dbSinDarElaGetVwRollensResult = await DbSinDarEla.GetVwRollens();
            rstRollen = dbSinDarElaGetVwRollensResult.Value.AsODataEnumerable();

            var dbSinDarElaGetBenutzersResult = await DbSinDarEla.GetBenutzers(filter:$"BenutzerName eq '{Security.User.UserName}'", expand:$"Base");
            rstBenutzer = dbSinDarElaGetBenutzersResult.Value.AsODataEnumerable();

            dsoBenutzer = rstBenutzer.FirstOrDefault();;

            strNameKontakt = dsoBenutzer.Base.Name1 + " " + dsoBenutzer.Base.Name2;

            var securityGetUserByIdResult = await Security.GetUserById($"{dsoBenutzer.AspNetUsers_Id}");
            dsoUser = securityGetUserByIdResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Drucken ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task Form0Submit(dynamic args)
        {
            dsoUser.UserName = dsoBenutzer.BenutzerEMail;

            try
            {
                var securityUpdateUserResult = await Security.UpdateUser($"{dsoBenutzer.AspNetUsers_Id}", dsoUser);
                try
                {
                    var dbSinDarElaUpdateBenutzerResult = await DbSinDarEla.UpdateBenutzer(benutzerId:dsoBenutzer.BenutzerID, benutzer:dsoBenutzer);
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Detail = $"Benutzer aktualisiert" });

                    DialogService.Close(dbSinDarElaUpdateBenutzerResult);
                }
                catch (System.Exception dbSinDarElaUpdateBenutzerException)
                {
                    NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzer (Schritt 2) konnte nicht aktualisiert werden!" });

                }
            }
            catch (System.Exception securityUpdateUserException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzer (Schritt 1) konnte nicht aktualisiert werden!" });
            }
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            dsoUser.UserName = dsoBenutzer.BenutzerID.ToString();

            try
            {
                var securityUpdateUserResult = await Security.UpdateUser($"{dsoBenutzer.AspNetUsers_Id}", dsoUser);
                try
                {
                    var dbSinDarElaUpdateBenutzerResult = await DbSinDarEla.UpdateBenutzer(benutzerId:dsoBenutzer.BenutzerID, benutzer:dsoBenutzer);
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Detail = $"Benutzer aktualisiert" });
                }
                catch (System.Exception dbSinDarElaUpdateBenutzerException)
                {
                    NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzer (Schritt 2) konnte nicht aktualisiert werden!" });

                }
            }
            catch (System.Exception securityUpdateUserException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Benutzer (Schritt 1) konnte nicht aktualisiert werden!" });
            }
        }

        protected async System.Threading.Tasks.Task Upload0Error(UploadErrorEventArgs args)
        {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Bild",Detail = $"Hochladen fehlgeschlagen!" });
        }

        protected async System.Threading.Tasks.Task ButtonBildEntfernenClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<MeldungJaNein>($"Bild entfernen", new Dictionary<string, object>() { {"strMeldung", "Soll das Bild von '" + dsoBenutzer.Base.Name1 + " " + dsoBenutzer.Base.Name2 + "' entfernt werden?"} });
            if (dialogResult == "Ja")
            {
                dsoBenutzer.Base.BildURL = "https://medien.sindarela.app/upload/bilder/base/KeinBildPerson.png";
            }

            try
            {
                if (dialogResult == "Ja")
                {
                    var dbSinDarElaUpdateBaseResult = await DbSinDarEla.UpdateBase(baseId:dsoBenutzer.BaseID, _base:dsoBenutzer.Base);
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Detail = $"Bild entfernt" });
                }
            }
            catch (System.Exception dbSinDarElaUpdateBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Bild konnte nicht entfernt werden!" });
            }
        }
    }
}
