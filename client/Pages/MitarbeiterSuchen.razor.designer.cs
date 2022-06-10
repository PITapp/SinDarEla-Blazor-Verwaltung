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
    public partial class MitarbeiterSuchenComponent : ComponentBase, IDisposable
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

        int _intMitarbeiterID;
        protected int intMitarbeiterID
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

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterSuchen> _rstMitarbeiter;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.VwMitarbeiterSuchen> rstMitarbeiter
        {
            get
            {
                return _rstMitarbeiter;
            }
            set
            {
                if (!object.Equals(_rstMitarbeiter, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstMitarbeiter", NewValue = value, OldValue = _rstMitarbeiter };
                    _rstMitarbeiter = value;
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
            intMitarbeiterID = 0;

            var dbSinDarElaGetVwMitarbeiterSuchensResult = await DbSinDarEla.GetVwMitarbeiterSuchens();
            rstMitarbeiter = dbSinDarElaGetVwMitarbeiterSuchensResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task ButtonUebernehmenClick(MouseEventArgs args)
        {
            if (intMitarbeiterID != 0) {
              DialogService.Close(intMitarbeiterID);
            }
        }

        protected async System.Threading.Tasks.Task ButtonAbbruchClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
