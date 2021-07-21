using BlazorApp.Dialogs;
using Dto;
using MudBlazor;
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


        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");
            _isDataReady = true;
        }

        public void NavigateToCreateUnitClass()
        {
            NavigationManager.NavigateTo("/unitclasses/create");
        }

        private void NavigateToEditUnitClass(string id)
        {
            NavigationManager.NavigateTo($"/unitclasses/edit/{id}");
        }

        private async Task DeleteUnitClass(UnitClassDto unitClass)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Вы действительно хотите удалить класс юнита?");
            parameters.Add("ButtonText", "Удалить");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.Show<ConfirmItemDeleteDialogComponent>($"Удаление класса юнита", parameters, options).Result;

            if (!dialog.Cancelled)
            {
                await HttpService.DeleteAsync($"unitclasses/{unitClass.Id}");
            }
        }
    }
}
