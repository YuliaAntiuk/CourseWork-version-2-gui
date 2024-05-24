using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public class GraphicalForm: Form
    {
        public Equation equation;
        public GraphicalForm(Equation equation):base() 
        {
            this.equation = equation;
        }
        public double[] FindMinMaxY(Series series)
        {
            double minY = double.PositiveInfinity;
            double maxY = double.NegativeInfinity;

            foreach (DataPoint point in series.Points)
            {
                double yValue = point.YValues[0];
                if (yValue < minY)
                {
                    minY = yValue;
                }
                if (yValue > maxY)
                {
                    maxY = yValue;
                }
            }
            return new double[] { minY, maxY };
        }
        public Chart CreateChart()
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            ChartArea plot = new ChartArea("Графічний метод");
            chart.ChartAreas.Add(plot);
            plot.AxisX.Minimum = double.NaN;
            plot.AxisX.Maximum = double.NaN;
            plot.AxisY.Minimum = double.NaN;
            plot.AxisY.Maximum = double.NaN;
            plot.AxisX.IsStartedFromZero = false;
            plot.AxisY.IsStartedFromZero = false;

            chart.MouseWheel += ChartMouseWheelEventHandler;

            chart.Legends.Add(new Legend("Legend"));

            return chart;
        }
        public void AddSeries(Chart chart, Series series)
        {
            chart.Series.Add(series);
            chart.Series[series.Name].Legend = "Legend";
        }
        public void AddPointsToSeries(Series series, double startX, double endX, Func<double, double> calculateY)
        {
            for (double x = startX; x <= endX; x += 0.1)
            {
                double y = calculateY(x);
                series.Points.AddXY(x, y);
            }
        }
        public void CreateGraphic()
        {
            Text = "Графіки рівнянь";
            Size = new System.Drawing.Size(600, 400);

            Chart chart = CreateChart();

            Series series1 = CreateSeries(0);
            Series series2 = CreateSeries(1);
            series1.Name = "Рівняння 1";
            series2.Name = "Рівняння 2";

            double max = equation.FindMaximumMinimum()[0];
            double startX = (max > 0) ? (-max) : max;
            double endX = Math.Abs(max);

            if (equation.Coefficients[0, 1] == 0)
            {
                AddPointsToSeries(series2, startX, endX, x => CalculateY(equation.Coefficients[1, 0], equation.Coefficients[1, 1], equation.Constants[1], x));
                double straightX = equation.Constants[0] / equation.Coefficients[0, 0];
                series1.Points.AddXY(straightX, FindMinMaxY(series2)[0]);
                series1.Points.AddXY(straightX, FindMinMaxY(series2)[1]);
            }
            else if (equation.Coefficients[1, 1] == 0)
            {
                AddPointsToSeries(series1, startX, endX, x => CalculateY(equation.Coefficients[0, 0], equation.Coefficients[0, 1], equation.Constants[0], x));
                double straightX = equation.Constants[1] / equation.Coefficients[1, 0];
                series2.Points.AddXY(straightX, FindMinMaxY(series1)[0]);
                series2.Points.AddXY(straightX, FindMinMaxY(series1)[1]);
            }
            else
            {
                AddPointsToSeries(series1, startX, endX, x => CalculateY(equation.Coefficients[0, 0], equation.Coefficients[0, 1], equation.Constants[0], x));
                AddPointsToSeries(series2, startX, endX, x => CalculateY(equation.Coefficients[1, 0], equation.Coefficients[1, 1], equation.Constants[1], x));
            }

            AddSeries(chart, series1);
            AddSeries(chart, series2);

            CalculateIntersectionPoints();

            Controls.Add(chart);
            Show();
            FormClosed += (sender, e) =>
            {
                Dispose();
            };
        }
        public void ChartMouseWheelEventHandler(object sender, MouseEventArgs e)
        {
            Chart chart = (Chart)sender;

            double xMin = chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double xMax = chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double yMin = chart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double yMax = chart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

            double posXStart = xMin;
            double posXFinish = xMax;
            double posYStart = yMin;
            double posYFinish = yMax;

            if (e.Delta > 0)
            {
                // Zoom in
                double xRange = xMax - xMin;
                double yRange = yMax - yMin;

                posXStart = Math.Round(xMin + xRange * 0.25, 1);
                posXFinish = Math.Round(xMax - xRange * 0.25, 1);
                posYStart = Math.Round(yMin + yRange * 0.25, 1);
                posYFinish = Math.Round(yMax - yRange * 0.25, 1);
            }
            else if (e.Delta < 0)
            {
                // Zoom out
                double xRange = xMax - xMin;
                double yRange = yMax - yMin;

                posXStart = Math.Round(xMin - xRange * 0.25, 1);
                posXFinish = Math.Round(xMax + xRange * 0.25, 1);
                posYStart = Math.Round(yMin - yRange * 0.25, 1);
                posYFinish = Math.Round(yMax + yRange * 0.25, 1);
            }

            chart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
            chart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "F1";
        }
        public Series CreateSeries(int index)
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = (index == 0) ? Color.Blue : Color.Red;
            series.BorderWidth = 3;
            return series;
        }
        public void CalculateIntersectionPoints()
        {
            equation.Result[0] = (equation.Coefficients[1, 1] * equation.Constants[0] - equation.Coefficients[0, 1] * equation.Constants[1]) / equation.CalculateDeterminant(equation.Coefficients);
            equation.Result[1] = (equation.Coefficients[0, 0] * equation.Constants[1] - equation.Coefficients[1, 0] * equation.Constants[0]) / equation.CalculateDeterminant(equation.Coefficients);
        }
        public double CalculateY(double a1, double a2, double b, double x)
        {
            if (a2 != 0)
            {
                return (b - a1 * x) / a2;
            }
            else
            {
                return b / a1;
            }
        }
    }
}
