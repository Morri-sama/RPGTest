using BlazorApp.FormEditors;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CreateUnitClassPage()
        {

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }


        List<string> unitFields = new()
        {
            "БазовыйУрон",
            "НедостающееЗдоровье",
            "МаксимальноеЗдоровье",
            "ДистанцияДоЦели",
            "РадиусАтаки",
            "ТекущаяМана"
        };

        public void AddFormulaValue(string value)
        {
            _formulaValues.Add($"{value} {_formulaCounter}");
            _formulaCounter++;
        }

        public void OnFormulaChipClose(MudChip mudChip)
        {
            _formulaValues.Remove(mudChip.Text);
        }

        public void AddFormula2Value(string value)
        {
            _formula2Values.Add($"{value} {_formulaCounter}");
            _formula2Counter++;
        }

        public void OnFormula2ChipClose(MudChip mudChip)
        {
            _formula2Values.Remove(mudChip.Text);
        }

        public void AddConditionValue(string value)
        {
            _conditionValues.Add($"{value} {_formulaCounter}");
            _conditionCounter++;
        }

        public void OnConditionChipClose(MudChip mudChip)
        {
            _conditionValues.Remove(mudChip.Text);
        }
    }
}
