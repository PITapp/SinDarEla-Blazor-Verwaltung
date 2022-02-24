﻿using System;
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
    public partial class EditBaseComponent : ComponentBase
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

        [Parameter]
        public dynamic BaseID { get; set; }

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
            var dbSinDarElaGetBaseByBaseIdResult = await DbSinDarEla.GetBaseByBaseId(baseId:BaseID);
            _base = dbSinDarElaGetBaseByBaseIdResult;

            var dbSinDarElaGetBaseAnredensResult = await DbSinDarEla.GetBaseAnredens();
            getBaseAnredensResult = dbSinDarElaGetBaseAnredensResult.Value.AsODataEnumerable();
        }

        protected async System.Threading.Tasks.Task Form0Submit(SinDarElaVerwaltung.Models.DbSinDarEla.Base args)
        {
            try
            {
                var dbSinDarElaUpdateBaseResult = await DbSinDarEla.UpdateBase(baseId:BaseID, _base:_base);
                DialogService.Close(_base);
            }
            catch (System.Exception dbSinDarElaUpdateBaseException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Base" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}