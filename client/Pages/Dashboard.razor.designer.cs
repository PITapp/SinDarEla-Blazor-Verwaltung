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
    public partial class DashboardComponent : ComponentBase, IDisposable
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

        string _strLocalStorage;
        protected string strLocalStorage
        {
            get
            {
                return _strLocalStorage;
            }
            set
            {
                if (!object.Equals(_strLocalStorage, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strLocalStorage", NewValue = value, OldValue = _strLocalStorage };
                    _strLocalStorage = value;
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
            strLocalStorage = "";
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Drucken ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }
    }
}
