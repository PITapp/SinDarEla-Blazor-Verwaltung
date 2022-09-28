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
    public partial class BaseComponent : ComponentBase, IDisposable
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
        protected RadzenDataGrid<SinDarElaVerwaltung.Models.DbSinDarEla.Base> grid0;

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseAnreden> _getBaseAnredensResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.BaseAnreden> getBaseAnredensResult
        {
            get
            {
                return _getBaseAnredensResult;
            }
            set
            {
                if (!object.Equals(_getBaseAnredensResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getBaseAnredensResult", NewValue = value, OldValue = _getBaseAnredensResult };
                    _getBaseAnredensResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _bolDatenLaden;
        protected bool bolDatenLaden
        {
            get
            {
                return _bolDatenLaden;
            }
            set
            {
                if (!object.Equals(_bolDatenLaden, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "bolDatenLaden", NewValue = value, OldValue = _bolDatenLaden };
                    _bolDatenLaden = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        SinDarElaVerwaltung.Models.DbSinDarEla.Base __base;
        protected SinDarElaVerwaltung.Models.DbSinDarEla.Base _base
        {
            get
            {
                return __base;
            }
            set
            {
                if (!object.Equals(__base, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "_base", NewValue = value, OldValue = __base };
                    __base = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _isEdit;
        protected bool isEdit
        {
            get
            {
                return _isEdit;
            }
            set
            {
                if (!object.Equals(_isEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "isEdit", NewValue = value, OldValue = _isEdit };
                    _isEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> _getBasesResult;
        protected IEnumerable<SinDarElaVerwaltung.Models.DbSinDarEla.Base> getBasesResult
        {
            get
            {
                return _getBasesResult;
            }
            set
            {
                if (!object.Equals(_getBasesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getBasesResult", NewValue = value, OldValue = _getBasesResult };
                    _getBasesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getBasesCount;
        protected int getBasesCount
        {
            get
            {
                return _getBasesCount;
            }
            set
            {
                if (!object.Equals(_getBasesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getBasesCount", NewValue = value, OldValue = _getBasesCount };
                    _getBasesCount = value;
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
            var dbSinDarElaGetBaseAnredensResult = await DbSinDarEla.GetBaseAnredens();
            getBaseAnredensResult = dbSinDarElaGetBaseAnredensResult.Value.AsODataEnumerable();

            bolDatenLaden = true;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            _base = new SinDarElaVerwaltung.Models.DbSinDarEla.Base();

            isEdit = false;
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var dbSinDarElaGetBasesResult = await DbSinDarEla.GetBases(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getBasesResult = dbSinDarElaGetBasesResult.Value.AsODataEnumerable();

                getBasesCount = dbSinDarElaGetBasesResult.Count;

                bolDatenLaden = false;
            }
            catch (System.Exception dbSinDarElaGetBasesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Bases" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(SinDarElaVerwaltung.Models.DbSinDarEla.Base args)
        {
            isEdit = true;

            _base = args;
        }

        protected async System.Threading.Tasks.Task Form0Submit(SinDarElaVerwaltung.Models.DbSinDarEla.Base args)
        {
            try
            {
                if (isEdit)
                {
                    var dbSinDarElaUpdateBaseResult = await DbSinDarEla.UpdateBase(baseId:_base.BaseID, _base:_base);
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Summary = $"Success",Detail = $"Base updated!" });


                }
            }
            catch (System.Exception dbSinDarElaUpdateBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Base" });
            }

            try
            {
                if (!this.isEdit)
                {
                    var dbSinDarElaCreateBaseResult = await DbSinDarEla.CreateBase(_base:args);
                    _base = new SinDarElaVerwaltung.Models.DbSinDarEla.Base();

                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Success,Summary = $"Success",Detail = $"Base created!" });
                }
            }
            catch (System.Exception dbSinDarElaCreateBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Base!" });
            }
        }
    }
}
