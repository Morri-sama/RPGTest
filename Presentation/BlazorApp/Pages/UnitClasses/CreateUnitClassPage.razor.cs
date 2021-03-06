using BlazorApp.Dialogs;
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

            NavigateBack();
        }

        private void NavigateBack()
        {
            NavigationManager.NavigateTo("/unitclasses");
        }

        private async Task EditFormula()
        {
            string result = await EditExpression(expression: _formContext.Formula,
                                                 title: "Редактирование выражения, когда условия нет, либо оно верно.",
                                                 useFields: true,
                                                 useBrackets: true,
                                                 useMathOperators: true,
                                                 useLogicalOperators: false);

            _formContext.Formula ??= result;
        }

        private async Task EditFormula2()
        {
            string result = await EditExpression(expression: _formContext.Formula2,
                                                 title: "Редактирование выражения, когда условие ложно.",
                                                 useFields: true,
                                                 useBrackets: true,
                                                 useMathOperators: true,
                                                 useLogicalOperators: false);

            _formContext.Formula2 ??= result;
        }

        private async Task EditCondition()
        {
            string result = await EditExpression(expression: _formContext.Condition,
                                                 title: "Редактирование условия",
                                                 useFields: true,
                                                 useBrackets: true,
                                                 useMathOperators: true,
                                                 useLogicalOperators: true);

            _formContext.Condition ??= result;
        }

        private async Task EditPostTrueCondition()
        {
            string result = await EditExpression(expression: _formContext.PostTrueConditionAction,
                                                 title: "Редактирование выражения для изменения свойства юнита после атаки, когда условия нет, либо оно верно",
                                                 useFields: true,
                                                 useBrackets: true,
                                                 useMathOperators: true,
                                                 useLogicalOperators: false);

            _formContext.PostTrueConditionAction ??= result;
        }

        private async Task EditPostFalseCondition()
        {
            string result = await EditExpression(expression: _formContext.PostFalseConditionAction,
                                                 title: "Редактирование выражения для изменения свойства юнита после атаки, когда условие ложно",
                                                 useFields: true,
                                                 useBrackets: true,
                                                 useMathOperators: true,
                                                 useLogicalOperators: false);

            _formContext.PostFalseConditionAction ??= result;
        }

        private async Task<string> EditExpression(string expression,
                                                  string title = "Редактирование выражения",
                                                  bool useFields = false,
                                                  bool useBrackets = false,
                                                  bool useMathOperators = false,
                                                  bool useLogicalOperators = false)
        {
            string expressionNewValue = null;

            var parameters = new DialogParameters
            {
                ["Expression"] = expression,
                ["Title"] = title,
                ["UseFields"] = useFields,
                ["UseBrackets"] = useBrackets,
                ["UseMathOperators"] = useMathOperators,
                ["UseLogicalOperators"] = useLogicalOperators
            };

            var dialog = DialogService.Show<FormulaEditDialog>(title, parameters);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                expressionNewValue = result.Data.ToString();
            }

            return expressionNewValue;
        }
    }
}
