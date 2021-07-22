using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorApp.Dialogs
{
    public partial class FormulaEditDialog : ComponentBase
    {
        private List<string> _values = new();
        private int _chipCounter;
        private int _numericChipValue;

        private List<string> _unitFields = new()
        {
            "БазовыйУрон",
            "НедостающееЗдоровье",
            "МаксимальноеЗдоровье",
            "ДистанцияДоЦели",
            "РадиусАтаки",
            "ТекущаяМана",
            "ТекущееЗдоровье"
        };

        public FormulaEditDialog()
        {

        }

        protected override void OnInitialized()
        {
            AddChipsFromExpressionString();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Save()
        {
            MudDialog.Close(DialogResult.Ok<string>(Formula));
        }

        public void AddFormulaValue(string value)
        {
            _values.Add($"{value} {_chipCounter++}");

            Formula = ChipValuesToString();
        }

        public void OnFormulaChipClose(MudChip mudChip)
        {
            _values.Remove(mudChip.Text);

            Formula = ChipValuesToString();
        }

        private void AddChipsFromExpressionString()
        {
            string[] splittedExpression = Regex.Split(Formula, @"\s+");

            foreach (var value in splittedExpression)
            {
                _values.Add($"{value} {_chipCounter++}");
            }
        }

        private string ChipValuesToString()
        {
            string result = string.Empty;

            foreach (var value in _values)
            {
                result += Regex.Match(value, @"[а-яА-Я+\-*=z/><()0-9]*") + " ";
            }

            return result.Trim();
        }

        [Parameter]
        public string Title { get; set; } = "Редактирование";

        [Parameter]
        public string Formula { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
    }
}
