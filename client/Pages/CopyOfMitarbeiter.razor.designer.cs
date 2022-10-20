﻿using System;
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
    public partial class CopyOfMitarbeiterComponent : ComponentBase, IDisposable
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterFirmen> grid0;
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterTaetigkeiten> datagrid0;
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterKunden> datagrid1;

        SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter _dsoMitarbeiter;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Mitarbeiter dsoMitarbeiter
        {
            get
            {
                return _dsoMitarbeiter;
            }
            set
            {
                if (!object.Equals(_dsoMitarbeiter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "dsoMitarbeiter", NewValue = value, OldValue = _dsoMitarbeiter };
                    _dsoMitarbeiter = value;
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

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterFirmen> _getVwMitarbeiterFirmensResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterFirmen> getVwMitarbeiterFirmensResult
        {
            get
            {
                return _getVwMitarbeiterFirmensResult;
            }
            set
            {
                if (!object.Equals(_getVwMitarbeiterFirmensResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getVwMitarbeiterFirmensResult", NewValue = value, OldValue = _getVwMitarbeiterFirmensResult };
                    _getVwMitarbeiterFirmensResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterTaetigkeiten> _getVwMitarbeiterTaetigkeitensResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterTaetigkeiten> getVwMitarbeiterTaetigkeitensResult
        {
            get
            {
                return _getVwMitarbeiterTaetigkeitensResult;
            }
            set
            {
                if (!object.Equals(_getVwMitarbeiterTaetigkeitensResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getVwMitarbeiterTaetigkeitensResult", NewValue = value, OldValue = _getVwMitarbeiterTaetigkeitensResult };
                    _getVwMitarbeiterTaetigkeitensResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterKunden> _getVwMitarbeiterKundensResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterKunden> getVwMitarbeiterKundensResult
        {
            get
            {
                return _getVwMitarbeiterKundensResult;
            }
            set
            {
                if (!object.Equals(_getVwMitarbeiterKundensResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getVwMitarbeiterKundensResult", NewValue = value, OldValue = _getVwMitarbeiterKundensResult };
                    _getVwMitarbeiterKundensResult = value;
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

            try
            {
                if (Globals.globalBenutzer != null)
                {
                    var dbSinDarElaGetMitarbeiterByMitarbeiterIdResult = await DbSinDarEla.GetMitarbeiterByMitarbeiterId(mitarbeiterId:Globals.globalBenutzer.LetzteMitarbeiterID);
                    dsoMitarbeiter = dbSinDarElaGetMitarbeiterByMitarbeiterIdResult;

                    var dbSinDarElaGetBaseByBaseIdResult = await DbSinDarEla.GetBaseByBaseId(baseId:dsoMitarbeiter.BaseID);
                    dsoBase = dbSinDarElaGetBaseByBaseIdResult;

                    strMitarbeiterName = dbSinDarElaGetBaseByBaseIdResult.Name1 + ' ' + dbSinDarElaGetBaseByBaseIdResult.Name2;

                    var dbSinDarElaGetBaseAnredensResult = await DbSinDarEla.GetBaseAnredens();
                    rstBaseAnreden = dbSinDarElaGetBaseAnredensResult.Value.AsODataEnumerable();
                }
            }
            catch (System.Exception dbSinDarElaGetMitarbeiterByMitarbeiterIdException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Detail = $"Kein Mitarbeiter ausgewählt!" });

            strMitarbeiterName = "Klicke auf [Suchen] um einen Mitarbeiter auszuwählen!";
            }
        }

        protected async System.Threading.Tasks.Task ButtonBerichreClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Berichte ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task ButtonListenClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MitarbeiterListen>($"Listen Mitarbeiter", null, new DialogOptions(){ Width = $"{1700}px",AutoFocusFirstElement = false,Resizable = true,Draggable = true });
        }

        protected async System.Threading.Tasks.Task ButtonSuchenClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<MitarbeiterSuchen>($"Suchen", null, new DialogOptions(){ Width = $"{350}px",AutoFocusFirstElement = false,Draggable = true });
            if (dialogResult != null) {
                Globals.globalBenutzer.LetzteMitarbeiterID = dialogResult;
            }

            if (dialogResult != null)
            {
                await Load();
            }
        }

        protected async System.Threading.Tasks.Task ButtonNeuClick(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<MitarbeiterNeu>($"Neuer Mitarbeiter", null);
            if (dialogResult != null) {
                Globals.globalBenutzer.LetzteMitarbeiterID = dialogResult.MitarbeiterID;
            }

            if (dialogResult != null)
            {
                await Load();
            }
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

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetVwMitarbeiterFirmensResult = await DbSinDarEla.GetVwMitarbeiterFirmens(filter:$"MitarbeiterID eq {Globals.globalBenutzer.LetzteMitarbeiterID}");
                getVwMitarbeiterFirmensResult = dbSinDarElaGetVwMitarbeiterFirmensResult.Value.AsODataEnumerable();
            }
            catch (System.Exception dbSinDarElaGetVwMitarbeiterFirmensException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load VwMitarbeiterFirmens" });
            }
        }

        protected async System.Threading.Tasks.Task Datagrid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetVwMitarbeiterTaetigkeitensResult = await DbSinDarEla.GetVwMitarbeiterTaetigkeitens(filter:$"MitarbeiterID eq {Globals.globalBenutzer.LetzteMitarbeiterID}");
                getVwMitarbeiterTaetigkeitensResult = dbSinDarElaGetVwMitarbeiterTaetigkeitensResult.Value.AsODataEnumerable();
            }
            catch (System.Exception dbSinDarElaGetVwMitarbeiterTaetigkeitensException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load VwMitarbeiterTaetigkeitens" });
            }
        }

        protected async System.Threading.Tasks.Task Datagrid1LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetVwMitarbeiterKundensResult = await DbSinDarEla.GetVwMitarbeiterKundens(filter:$"MitarbeiterID eq {Globals.globalBenutzer.LetzteMitarbeiterID}");
                getVwMitarbeiterKundensResult = dbSinDarElaGetVwMitarbeiterKundensResult.Value.AsODataEnumerable();
            }
            catch (System.Exception dbSinDarElaGetVwMitarbeiterKundensException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load VwMitarbeiterKundens" });
            }
        }
    }
}
