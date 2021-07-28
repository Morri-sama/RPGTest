using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Dto;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class Index : PageBase
    {
        private event EventHandler _coordinatesChanged;

        private Canvas2DContext _context;

        private BECanvasComponent _canvasReference;

        private List<UnitDto> _units;
        private List<UnitClassDto> _unitClasses;

        private UnitDto _selectedUnit;

        private string _action;

        private UnitDto _secondUnit;
        private Point _point;

        public Index()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            _unitClasses = await HttpService.GetAsync<List<UnitClassDto>>("unitclasses");
            _units = await HttpService.GetAsync<List<UnitDto>>("units");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await _canvasReference.CreateCanvas2DAsync();
                await DrawGridAsync();
            }
        }

        public async Task DrawGridAsync()
        {
            for (var i = 0; i < Width; i += 20)
            {
                await _context.MoveToAsync(i, 0);
                await _context.LineToAsync(i, Height);
            }

            for (var i = 0; i < Height; i += 20)
            {
                await _context.MoveToAsync(0, i);
                await _context.LineToAsync(Width, i);
            }

            await _context.SetStrokeStyleAsync(@"#ddd");
            await _context.StrokeAsync();

            await StartAsync();
        }

        public async Task StartAsync()
        {
            if (_units != null)
            {
                foreach (var unit in _units)
                {
                    await DrawUnitAsync(unit);
                }
            }
        }


        public async Task DrawUnitAsync(UnitDto unitDto)
        {
            var bytes = Encoding.UTF8.GetBytes(unitDto.Id);
            await _context.BeginPathAsync();
            await _context.SetLineWidthAsync(1);
            await _context.SetStrokeStyleAsync("black");
            await _context.SetFillStyleAsync($"rgb({bytes[0]}, {bytes[1]}, {bytes[2]})");
            await _context.FillRectAsync(unitDto.X, unitDto.Y, 20, 20);
            await _context.StrokeAsync();
            await _context.SetFontAsync("12px century-gothic, sans-serif");
            await _context.StrokeTextAsync(unitDto.Name, unitDto.X + 22, unitDto.Y + 12);
        }

        public async Task SelectUnitAsync()
        {
            EventHandler handler = null;

            _coordinatesChanged += handler = async (object sender, EventArgs e) =>
            {
                var unit = _units.Where(u => Point.X >= u.X && Point.X <= u.X + 20 && Point.Y >= u.Y && Point.Y <= u.Y + 20).FirstOrDefault();

                _selectedUnit = unit;
                this.StateHasChanged();
                _coordinatesChanged -= handler;
            };

            _coordinatesChanged += handler;
        }

        public void SetClickedPointCoordinates(MouseEventArgs e)
        {
            double offsetX = e.OffsetX;
            double offsetY = e.OffsetY;

            Point = new Point(offsetX, offsetY);

            //if (!string.IsNullOrEmpty(_action))
            //{
            //    if (_action == "передвинуться")
            //    {
            //        _action = null;

            //        _selectedUnit.X = (int)offsetX;
            //        _selectedUnit.Y = (int)offsetY;

            //        await HttpService.PutAsync<UnitDto>("units", _selectedUnit);

            //        await _context.ClearRectAsync(0, 0, Width, Height);
            //        await DrawGrid();
            //        _selectedUnit = null;
            //    }
            //}
            //else
            //{
            //    var unit = _units.Where(u => offsetX >= u.X && offsetX <= u.X + 20 && offsetY >= u.Y && offsetY <= u.Y + 20).FirstOrDefault();

            //    if (unit is not null)
            //    {
            //        bool? result = await DialogService.ShowMessageBox($"{unit.Id}", "Выберите действие", yesText: "Передвинуться", cancelText: "Cancel");
            //        string state = result == null ? "Cancelled" : "передвинуться";

            //        if (state == "передвинуться")
            //        {
            //            _action = "передвинуться";
            //            _selectedUnit = unit;
            //        }
            //    }
            //}

            //var unit2 = _units.Where(u => offsetX >= u.X && offsetX <= u.X + 20 && offsetY >= u.Y && offsetY <= u.Y + 20).FirstOrDefault();
            //_selectedUnit = unit2;
        }

        public async Task MoveAsync()
        {
            EventHandler handler = null;

            _coordinatesChanged += handler = async (object sender, EventArgs e) =>
            {
                _selectedUnit.X = (int)_point.X;
                _selectedUnit.Y = (int)_point.Y;

                await HttpService.PutAsync<UnitDto>("units", _selectedUnit);

                await _context.ClearRectAsync(0, 0, Width, Height);
                await DrawGridAsync();

                this.StateHasChanged();

                _coordinatesChanged -= handler;
            };

            _coordinatesChanged += handler;
        }


        public int Width { get; set; } = 1000;

        public int Height { get; set; } = 1000;

        public Point Point
        {
            get => _point;
            set
            {
                _point = value;
                _coordinatesChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public struct Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y) => (X, Y) = (x, y);
    }
}
