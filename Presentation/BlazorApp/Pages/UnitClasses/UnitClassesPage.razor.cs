using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.UnitClasses
{
    public partial class UnitClassesPage : PageBase
    {
        private List<UnitClassDto> _unitClasses;
        private bool _isDataReady;

        public UnitClassesPage()
        {

        }

        public void NavigateToAddUnitClass()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");            
            _isDataReady = true;
        }

    }
}
