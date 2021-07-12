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

        public void OnClose(MudChip mudChip)
        {
            _formulaValues.Remove(mudChip.Text);
        }
    }
}
