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
    public partial class MitarbeiterComponent : ComponentBase, IDisposable
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

        dynamic _intBaseID;
        protected dynamic intBaseID
        {
            get
            {
                return _intBaseID;
            }
            set
            {
                if (!object.Equals(_intBaseID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "intBaseID", NewValue = value, OldValue = _intBaseID };
                    _intBaseID = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        dynamic _intMitarbeiterID;
        protected dynamic intMitarbeiterID
        {
            get
            {
                return _intMitarbeiterID;
            }
            set
            {
                if (!object.Equals(_intMitarbeiterID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "intMitarbeiterID", NewValue = value, OldValue = _intMitarbeiterID };
                    _intMitarbeiterID = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SinDarElaVerwaltung.Models.DbSinDarEla.Base _dsoBase;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Base dsoBase
        {
            get
            {
                return _dsoBase;
            }
            set
            {
                if (!object.Equals(_dsoBase, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoBase", NewValue = value, OldValue = _dsoBase };
                    _dsoBase = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _strMitarbeiterName;
        protected string strMitarbeiterName
        {
            get
            {
                return _strMitarbeiterName;
            }
            set
            {
                if (!object.Equals(_strMitarbeiterName, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strMitarbeiterName", NewValue = value, OldValue = _strMitarbeiterName };
                    _strMitarbeiterName = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseAnreden> _rstBaseAnreden;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseAnreden> rstBaseAnreden
        {
            get
            {
                return _rstBaseAnreden;
            }
            set
            {
                if (!object.Equals(_rstBaseAnreden, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstBaseAnreden", NewValue = value, OldValue = _rstBaseAnreden };
                    _rstBaseAnreden = value;
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
            intBaseID = 249;

            intMitarbeiterID = 62;

            var dbSinDarElaGetBaseByBaseIdResult = await DbSinDarEla.GetBaseByBaseId(baseId:intBaseID);
            dsoBase = dbSinDarElaGetBaseByBaseIdResult;

            strMitarbeiterName = dbSinDarElaGetBaseByBaseIdResult.Name1 + ' ' + dbSinDarElaGetBaseByBaseIdResult.Name2;

            var dbSinDarElaGetBaseAnredensResult = await DbSinDarEla.GetBaseAnredens();
            rstBaseAnreden = dbSinDarElaGetBaseAnredensResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task ButtonBerichreClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Berichte ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task ButtonListenClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Listen ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task ButtonSuchenClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Suchen ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task ButtonNeuClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<MitarbeiterNeu>($"Neuer Mitarbeiter", null);
            intBaseID = dialogResult.BaseID;

            intMitarbeiterID = dialogResult.MitarbeiterID;
        }

        protected async System.Threading.Tasks.Task ButtonSpeichernClick(MouseEventArgs args)
        {
            try
            {
                var dbSinDarElaUpdateBaseResult = await DbSinDarEla.UpdateBase(baseId:dsoBase.BaseID, _base:dsoBase);
                    NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Detail = $"Änderungen gespeichert" });
            }
            catch (System.Exception dbSinDarElaUpdateBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Änderungen konnten nicht gespeichert werden!" });
            }
        }
    }
}
