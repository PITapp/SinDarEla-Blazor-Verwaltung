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
    public partial class EinstellungenComponent : ComponentBase
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> datagridInfotexte;

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> _rstInfotexte;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml> rstInfotexte
        {
            get
            {
                return _rstInfotexte;
            }
            set
            {
                if (!object.Equals(_rstInfotexte, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstInfotexte", NewValue = value, OldValue = _rstInfotexte };
                    _rstInfotexte = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _rstInfotexteCount;
        protected int rstInfotexteCount
        {
            get
            {
                return _rstInfotexteCount;
            }
            set
            {
                if (!object.Equals(_rstInfotexteCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "rstInfotexteCount", NewValue = value, OldValue = _rstInfotexteCount };
                    _rstInfotexteCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _strTitel;
        protected string strTitel
        {
            get
            {
                return _strTitel;
            }
            set
            {
                if (!object.Equals(_strTitel, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strTitel", NewValue = value, OldValue = _strTitel };
                    _strTitel = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        string _strTextHTML;
        protected string strTextHTML
        {
            get
            {
                return _strTextHTML;
            }
            set
            {
                if (!object.Equals(_strTextHTML, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "strTextHTML", NewValue = value, OldValue = _strTextHTML };
                    _strTextHTML = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _intInfotextID;
        protected int intInfotextID
        {
            get
            {
                return _intInfotextID;
            }
            set
            {
                if (!object.Equals(_intInfotextID, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "intInfotextID", NewValue = value, OldValue = _intInfotextID };
                    _intInfotextID = value;
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
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await DialogService.OpenAsync<MeldungOk>($"Info", new Dictionary<string, object>() { {"strMeldung", "Drucken ist für dieses Modul noch nicht aktiviert!"} }, new DialogOptions(){ Width = $"{600}px" });
        }

        protected async System.Threading.Tasks.Task DatagridInfotexteLoadData(LoadDataArgs args)
        {
            var dbSinDarElaGetInfotexteHtmlsResult = await DbSinDarEla.GetInfotexteHtmls(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
            rstInfotexte = dbSinDarElaGetInfotexteHtmlsResult.Value.AsODataEnumerable();

            rstInfotexteCount = dbSinDarElaGetInfotexteHtmlsResult.Count;

            await datagridInfotexte.SelectRow(rstInfotexte.FirstOrDefault());
        }

        protected async System.Threading.Tasks.Task DatagridInfotexteRowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.InfotexteHtml args)
        {
            strTitel = args.Titel;

            strTextHTML = args.Inhalt;

            intInfotextID = args.InfotextID;
        }

        protected async System.Threading.Tasks.Task ButtonBearbeitenInfotextEditorClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<EinstellungenInfotexteEditor>($"Bearbeiten", new Dictionary<string, object>() { {"InfotextID", intInfotextID} }, new DialogOptions(){ Width = $"{725}px",AutoFocusFirstElement = false,CloseDialogOnEsc = false,Resizable = true,Draggable = true });
        }
    }
}
