using BlazorApp.FormEditors;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Units
{
    public partial class CreateUnitPage : PageBase
    {
        private UnitFormContext _formContext = new();
        private List<UnitClassDto> _unitClasses = new()
            ;

        public CreateUnitPage()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");
            _formContext.Mapper = Mapper;
        }

        private void PostToApi()
        {
            _formContext.PrepareDataItem();

            var unit = _formContext.DataItem;

            HttpService.PostAsync<UnitDto>("units", unit);
        }

        private void NavigateBack()
        {
            NavigationManager.NavigateTo("/units");
        }
    }
}
