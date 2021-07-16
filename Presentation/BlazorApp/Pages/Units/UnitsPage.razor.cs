using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Units
{
    public partial class UnitsPage : PageBase
    {
        private List<UnitDto> _units;
        private bool _isDataReady;

        public UnitsPage()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _units = await HttpService.GetAsync<List<UnitDto>>("units");
            _isDataReady = true;
        }


    }
}
