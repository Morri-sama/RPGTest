using BlazorApp.FormEditors;
using Dto;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Units
{
    public partial class EditUnitPage : PageBase
    {
        private UnitFormContext _formContext = new();
        private List<UnitClassDto> _unitClasses = new();

        public EditUnitPage()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");


            var unit = await HttpService.GetAsync<UnitDto>($"units/{Id}");

            _formContext.Mapper = Mapper;
            _formContext.DataItem = unit;
            _formContext.Init();
        }

        private void PutToApi()
        {
            _formContext.PrepareDataItem();

            var unit = _formContext.DataItem;

            HttpService.PutAsync<UnitDto>("units", unit);

            NavigateBack();
        }

        private void NavigateBack()
        {
            NavigationManager.NavigateTo("/units");
        }

        [Parameter]
        public string Id { get; set; }
    }
}
