using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public abstract class LayoutBase : ComponentBase
    {
        protected LayoutBase()
        {

        }

        public void RefreshState()
        {
            StateHasChanged();
        }

        [Parameter]
        public RenderFragment Body { get; set; }
    }
}
