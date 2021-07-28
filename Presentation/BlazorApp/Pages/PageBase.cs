using AutoMapper;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public abstract class PageBase : ComponentBase
    {
        public PageBase() : base()
        {

        }

        [Inject]
        public IHttpService HttpService { get; init; }

        [Inject]
        public IMapper Mapper { get; init; }

        [Inject]
        public NavigationManager NavigationManager { get; init; }

        [Inject]
        public IDialogService DialogService { get; init; }

        [Inject]
        public ISnackbar Snackbar { get; init; }
    }
}
