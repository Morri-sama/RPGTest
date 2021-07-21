using BlazorApp.Dialogs;
using Dto;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages.Units
{
    public partial class UnitsPage : PageBase
    {
        private List<UnitDto> _units;
        private List<UnitClassDto> _unitClasses;
        private bool _isDataReady;

        public UnitsPage()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");
            _units = await HttpService.GetAsync<List<UnitDto>>("units");
            _isDataReady = true;
        }

        private void NavigateToCreateUnit()
        {
            NavigationManager.NavigateTo("/units/create");
        }

        private void NavigateToEditUnit(string id)
        {
            NavigationManager.NavigateTo($"/units/edit/{id}");
        }

        private async Task DeleteUnit(UnitDto unit)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", $"Вы действительно хотите удалить юнита {unit.Id}?");
            parameters.Add("ButtonText", "Удалить");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.Show<ConfirmItemDeleteDialogComponent>($"Удаление юнита", parameters, options).Result;

            if (!dialog.Cancelled)
            {
                await HttpService.DeleteAsync($"units/{unit.Id}");
            }
        }
    }
}
