using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public class GraphicalForm: Form
    {
        private Equation equation;
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalForm"/> class with the given equation.
        /// </summary>
        /// <param name="equation">The equation to be used for graphing.</param>
        public GraphicalForm(Equation equation):base() 
        {
            this.equation = equation;
            this.MinimumSize = new System.Drawing.Size(600, 600);
        }
        /// <summary>
        /// Finds the minimum and maximum Y values in a given series.
        /// </summary>
        /// <param name="series">The series to analyze.</param>
        /// <returns>An array containing the minimum and maximum Y values.</returns>
        private double[] FindMinMaxY(Series series)
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
        /// <summary>
        /// Creates and configures a new chart for displaying the equations.
        /// </summary>
        /// <returns>The created chart.</returns>
        private Chart CreateChart()
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
        /// <summary>
        /// Adds a series to the chart and associates it with the legend.
        /// </summary>
        /// <param name="chart">The chart to add the series to.</param>
        /// <param name="series">The series to add.</param>
        private void AddSeries(Chart chart, Series series)
        {
            chart.Series.Add(series);
            chart.Series[series.Name].Legend = "Legend";
        }
        /// <summary>
        /// Adds points to the series based on a function that calculates Y values from X values.
        /// </summary>
        /// <param name="series">The series to add points to.</param>
        /// <param name="startX">The starting X value.</param>
        /// <param name="endX">The ending X value.</param>
        /// <param name="calculateY">The function to calculate Y values from X values.</param>
        private void AddPointsToSeries(Series series, double startX, double endX, Func<double, double> calculateY)
        {
            for (double x = startX; x <= endX; x += 0.1)
            {
                double y = calculateY(x);
                series.Points.AddXY(x, y);
            }
        }
        /// <summary>
        /// Adjusts the chart's zoom level to fit the series within the view.
        /// </summary>
        /// <param name="chart">The chart to adjust.</param>
        /// <param name="series1">The first series to consider.</param>
        /// <param name="series2">The second series to consider.</param>
        private void AdjustChartZoom(Chart chart, Series series1, Series series2)
        {
            double minX = Math.Min(series1.Points.Min(p => p.XValue), series2.Points.Min(p => p.XValue));
            double maxX = Math.Max(series1.Points.Max(p => p.XValue), series2.Points.Max(p => p.XValue));
            double minY = Math.Min(series1.Points.Min(p => p.YValues[0]), series2.Points.Min(p => p.YValues[0]));
            double maxY = Math.Max(series1.Points.Max(p => p.YValues[0]), series2.Points.Max(p => p.YValues[0]));

            double padding = 0.1; 

            chart.ChartAreas[0].AxisX.ScaleView.Zoom(minX - padding, maxX + padding);
            chart.ChartAreas[0].AxisY.ScaleView.Zoom(minY - padding, maxY + padding);
            chart.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "F1";
        }
        /// <summary>
        /// Creates the graphical representation of the equations.
        /// </summary>
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

            if (equation.Result[0] < startX || equation.Result[0] > endX || equation.Result[1] < startX || equation.Result[1] > endX)
            {
                AdjustChartZoom(chart, series1, series2);
            }

            Controls.Add(chart);
            Show();
            FormClosed += (sender, e) =>
            {
                Dispose();
            };
        }
        /// <summary>
        /// Handles the mouse wheel event to zoom in and out of the chart.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ChartMouseWheelEventHandler(object sender, MouseEventArgs e)
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
                double xRange = xMax - xMin;
                double yRange = yMax - yMin;

                posXStart = Math.Round(xMin + xRange * 0.25, 1);
                posXFinish = Math.Round(xMax - xRange * 0.25, 1);
                posYStart = Math.Round(yMin + yRange * 0.25, 1);
                posYFinish = Math.Round(yMax - yRange * 0.25, 1);
            }
            else if (e.Delta < 0)
            {
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
        /// <summary>
        /// Creates a new series with the specified index.
        /// </summary>
        /// <param name="index">The index of the series.</param>
        /// <returns>The created series.</returns>
        private Series CreateSeries(int index)
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = (index == 0) ? Color.Blue : Color.Red;
            series.BorderWidth = 3;
            return series;
        }
        /// <summary>
        /// Calculates the intersection points of the equations and stores the results in the equation.
        /// </summary>
        private void CalculateIntersectionPoints()
        {
            equation.Result[0] = (equation.Coefficients[1, 1] * equation.Constants[0] - equation.Coefficients[0, 1] * equation.Constants[1]) / equation.CalculateDeterminant(equation.Coefficients);
            equation.Result[1] = (equation.Coefficients[0, 0] * equation.Constants[1] - equation.Coefficients[1, 0] * equation.Constants[0]) / equation.CalculateDeterminant(equation.Coefficients);
        }
        /// <summary>
        /// Calculates the Y value based on the coefficients and constants of the equation and the given X value.
        /// </summary>
        /// <param name="a1">The coefficient of X.</param>
        /// <param name="a2">The coefficient of Y.</param>
        /// <param name="b">The constant term.</param>
        /// <param name="x">The X value.</param>
        /// <returns>The calculated Y value.</returns>
        private double CalculateY(double a1, double a2, double b, double x)
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
