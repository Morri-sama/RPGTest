using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Services
{
    public interface ICoordinatesService
    {
        public double CalculateDistance(int x1, int y1, int x2, int y2);
    }
}
