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
        private readonly List<string> _fields;
        private readonly List<string> _brackets;
        private readonly List<string> _mathOperations;
        private readonly List<string> _logicalOperations;
        private readonly List<string> _operationsList = new();

        public FormulaEditDialog()
        {
            _fields = new()
            {
                "БазовыйУрон",
                "НедостающееЗдоровье",
                "МаксимальноеЗдоровье",
                "ДистанцияДоЦели",
                "РадиусАтаки",
                "ТекущаяМана",
                "ТекущееЗдоровье"
            };

            _brackets = new()
            {
                "(",
                ")"
            };

            _mathOperations = new()
            {
                "+",
                "-",
                "/",
                "*",
                "(",
                ")",
                "ОкруглитьВБольшуюСторону",
                "ОкруглитьВМеньшуюСторону"
            };

            _logicalOperations = new()
            {
                "=",
                "!=",
                ">=",
                "<=",
                ">",
                "<"
            };
        }

        protected override void OnInitialized()
        {
            if (UseFields)
            {
                _operationsList.AddRange(_fields);
            }

            if (UseBrackets)
            {
                _operationsList.AddRange(_brackets);
            }

            if (UseMathOperations)
            {
                _operationsList.AddRange(_mathOperations);
            }

            if (UseLogicalOperations)
            {
                _operationsList.AddRange(_logicalOperations);
            }

            AddChipsFromExpressionString();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Save()
        {
            MudDialog.Close(DialogResult.Ok<string>(Expression));
        }

        public void AddValue(string value)
        {
            _values.Add($"{value} {_chipCounter++}");

            Expression = ChipValuesToString();
        }

        public void OnChipClose(MudChip mudChip)
        {
            _values.Remove(mudChip.Text);

            Expression = ChipValuesToString();
        }

        private void AddChipsFromExpressionString()
        {
            string[] splittedExpression = Regex.Split(Expression, @"\s+");

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
        public string Expression { get; set; }

        [Parameter]
        public bool UseFields { get; set; } = false;

        [Parameter]
        public bool UseBrackets { get; set; } = false;

        [Parameter]
        public bool UseMathOperations { get; set; } = false;

        [Parameter]
        public bool UseLogicalOperations { get; set; } = false;

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }
    }
}
