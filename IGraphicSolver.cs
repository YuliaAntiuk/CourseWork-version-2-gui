using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System;

namespace GUI
{
    public interface IGraphicSolver
    {
        double[] FindMaximumMinimum();
        void CreateGraphic(Form graphicalForm);
        Series CreateSeries(int index);
        double[] FindMinMaxY(Series series);
        void CalculateIntersectionPoints();
        Chart CreateChart();
        void AddSeries(Chart chart, Series series);
        void AddPointsToSeries(Series series, double startX, double endX, Func<double, double> calculateY);
        void ChartMouseWheelEventHandler(object sender, MouseEventArgs e);
        double CalculateY(double a1, double a2, double b, double x);
    }
}
