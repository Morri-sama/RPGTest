using BlazorApp.FormEditors;
using Dto;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dto.EnumsDto;

namespace BlazorApp.Pages.UnitClasses
{
    public partial class CreateUnitClassPage : PageBase
    {
        private UnitClassFormContext _formContext = new();

        private List<string> _formulaValues = new();
        private int _formulaCounter;

        private List<string> _formula2Values = new();
        private int _formula2Counter;

        private List<string> _conditionValues = new();
        private int _conditionCounter;

        private List<string> _unitFields = new()
        {
            "БазовыйУрон",
            "НедостающееЗдоровье",
            "МаксимальноеЗдоровье",
            "ДистанцияДоЦели",
            "РадиусАтаки",
            "ТекущаяМана"
        };

        public CreateUnitClassPage()
        {

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _formContext.Mapper = Mapper;
        }

        public void PostToApi()
        {
            _formContext.PrepareDataItem();

            var unitClass = _formContext.DataItem;
            HttpService.PostAsync<UnitClassDto>("unitclasses", unitClass);
        }

        private void NavigateBack()
        {
            NavigationManager.NavigateTo("/unitclasses");
        }

        public void AddFormulaValue(string value)
        {
            _formulaValues.Add($"{value} {_formulaCounter}");
            _formulaCounter++;

            _formContext.Formula = ChipValuesToString(_formulaValues);
        }

        public void OnFormulaChipClose(MudChip mudChip)
        {
            _formulaValues.Remove(mudChip.Text);

            _formContext.Formula = ChipValuesToString(_formulaValues);
        }

        public void AddFormula2Value(string value)
        {
            _formula2Values.Add($"{value} {_formulaCounter}");
            _formula2Counter++;

            _formContext.Formula2 = ChipValuesToString(_formula2Values);
        }

        public void OnFormula2ChipClose(MudChip mudChip)
        {
            _formula2Values.Remove(mudChip.Text);

            _formContext.Formula2 = ChipValuesToString(_formula2Values);
        }

        public void AddConditionValue(string value)
        {
            _conditionValues.Add($"{value} {_formulaCounter}");
            _conditionCounter++;

            _formContext.Condition = ChipValuesToString(_conditionValues);
        }

        public void OnConditionChipClose(MudChip mudChip)
        {
            _conditionValues.Remove(mudChip.Text);

            _formContext.Condition = ChipValuesToString(_conditionValues);
        }

        public string ChipValuesToString(List<string> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            string result = string.Empty;

            foreach(var value in values)
            {
                result += Regex.Match(value, "[^ ]* (.*)").Groups[1].Value + " ";
            }

            return result;
        }
    }
}
