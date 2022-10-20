﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using SinDarElaVerwaltung.Models.DbSinDarEla;
using SinDarElaVerwaltung.Pages;

namespace SinDarElaVerwaltung.Layouts
{
    public partial class StandardLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected GlobalsService Globals { get; set; }

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

        protected RadzenBody body0;

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
             await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {

        }
    }
}
