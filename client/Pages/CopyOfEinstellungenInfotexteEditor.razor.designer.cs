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
    public partial class CopyOfEinstellungenInfotexteEditorComponent : ComponentBase
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

        bool _bolEditorAenderungen;
        protected bool bolEditorAenderungen
        {
            get
            {
                return _bolEditorAenderungen;
            }
            set
            {
                if (!object.Equals(_bolEditorAenderungen, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "bolEditorAenderungen", NewValue = value, OldValue = _bolEditorAenderungen };
                    _bolEditorAenderungen = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml _dsoInfotexteHTML;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml dsoInfotexteHTML
        {
            get
            {
                return _dsoInfotexteHTML;
            }
            set
            {
                if (!object.Equals(_dsoInfotexteHTML, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoInfotexteHTML", NewValue = value, OldValue = _dsoInfotexteHTML };
                    _dsoInfotexteHTML = value;
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
            bolEditorAenderungen = false;

            var dbSinDarElaGetInfotexteHtmlByInfotextIdResult = await DbSinDarEla.GetInfotexteHtmlByInfotextId(infotextId:InfotextID);
            dsoInfotexteHTML = dbSinDarElaGetInfotexteHtmlByInfotextIdResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            try
            {
                var dbSinDarElaUpdateInfotexteHtmlResult = await DbSinDarEla.UpdateInfotexteHtml(infotextId:InfotextID, infotexteHtml:dsoInfotexteHTML);
                    NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Detail = $"Infotext gespeichert" });

                DialogService.Close(dbSinDarElaUpdateInfotexteHtmlResult);
            }
            catch (System.Exception dbSinDarElaUpdateInfotexteHtmlException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Infotext konnte nicht gespeichert werden!" });
            }
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            if (this.bolEditorAenderungen == false) {
              DialogService.Close();
              await JSRuntime.InvokeAsync<string>("window.history.back");
            }

            if (this.bolEditorAenderungen == true) {
              var dialogResult = await DialogService.OpenAsync<MeldungJaNein>($"Text geändert", new Dictionary<string, object>() { {"strMeldung", "Der Text wurde geändert! Soll die Bearbeitung wirklich abgebrochen werden?"} }, new DialogOptions(){ Width = $"{720}px" });
                if (dialogResult == "Ja") {
                  DialogService.Close();
                  await JSRuntime.InvokeAsync<string>("window.history.back");
                }
            }
        }
    }
}
