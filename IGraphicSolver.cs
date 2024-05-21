using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace GUI
{
    public interface IGraphicSolver
    {
        double[] FindMaximumMinimum();
        void CreateGraphic(Form graphicalForm);
        Series CreateSeries(int index);
        double[] FindMinMaxY(Series series);
        void CalculateIntersectionPoints();
    }
}
