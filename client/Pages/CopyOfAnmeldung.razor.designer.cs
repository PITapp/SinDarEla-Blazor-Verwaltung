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
    public partial class CopyOfAnmeldungComponent : ComponentBase, IDisposable
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

        string _strHashPasswort;
        protected string strHashPasswort
        {
            get
            {
                return _strHashPasswort;
            }
            set
            {
                if (!object.Equals(_strHashPasswort, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strHashPasswort", NewValue = value, OldValue = _strHashPasswort };
                    _strHashPasswort = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _strHashPasswortErgebnis;
        protected string strHashPasswortErgebnis
        {
            get
            {
                return _strHashPasswortErgebnis;
            }
            set
            {
                if (!object.Equals(_strHashPasswortErgebnis, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strHashPasswortErgebnis", NewValue = value, OldValue = _strHashPasswortErgebnis };
                    _strHashPasswortErgebnis = value;
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
            strHashPasswort = "";

            strHashPasswortErgebnis = "";
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            // strHashPasswortErgebnis = HashPassword(strHashPasswort);
        }

        protected async System.Threading.Tasks.Task ButtonAnmeldenClick(MouseEventArgs args)
        {
            UriHelper.NavigateTo("dashboard");
        }
    }
}
