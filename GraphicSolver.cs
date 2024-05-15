﻿using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace GUI
{
    public interface GraphicSolver
    {
        double FindMaximum();
        void CreateGraphic(Form graphicalForm);
        Series CreateSeries(int index);
        double[] FindMinMaxY(Series series);
        void CalculateIntersectionPoints();
    }
}