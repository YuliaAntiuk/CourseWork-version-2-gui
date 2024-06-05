using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;
namespace GUI
{
    /// <summary>
    /// Provides validation methods for ensuring the correctness of input data and equation properties.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Maximum value restriction for input.
        /// </summary>
        public static double maxRestriction = 1e6;
        /// <summary>
        /// Minimum value restriction for input.
        /// </summary>
        public static double minRestriction = 1e-6;
        /// <summary>
        /// Maximum difference allowed between maximum and minimum coefficients for graphical method.
        /// </summary>
        public static double graphicalDiffRestriction = 500.0;
        /// <summary>
        /// Checks if the dimension input is valid.
        /// </summary>
        /// <param name="DimensionInput">The input TextBox for dimension.</param>
        /// <returns>True if dimension is entered and within range; otherwise, false.</returns>
        public static bool IsDimensionEntered(TextBox DimensionInput)
        {
            if (int.TryParse(DimensionInput.Text, out int dimension))
            {
                return dimension > 0 && dimension <= 10;
            }
            return false;
        }
        /// <summary>
        /// Checks if a method is selected in the ComboBox.
        /// </summary>
        /// <param name="comboBox">The ComboBox control.</param>
        /// <returns>True if a method is selected; otherwise, false.</returns>
        public static bool IsMethodSelected(ComboBox comboBox)
        {
            return comboBox.SelectedIndex != -1;
        }
        /// <summary>
        /// Checks if an item exists in the ComboBox.
        /// </summary>
        /// <param name="itemToFind">The item to find.</param>
        /// <param name="comboBox">The ComboBox control.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        public static bool IsItemInComboBox(object itemToFind, ComboBox comboBox)
        {
            foreach (object item in comboBox.Items)
            {
                if (item.Equals(itemToFind))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Handles the validating event for TextBox input.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public static void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text;
            if (!Regex.IsMatch(text, @"^$|^[+-]?(\d+(\.\d*)?|\.\d+)([eE][+-]?\d+)?$") || text.Length > 10)
            {
                MessageBox.Show("Введено некоретктні символи або їх кількість більша 10!", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Checks if the equation coefficients and constants are within the valid range.
        /// </summary>
        /// <param name="equation">The equation to validate.</param>
        /// <returns>True if the equation is valid; otherwise, false.</returns>
        public static bool IsEquationValid(Equation equation)
        {
            for (int i = 0; i < equation.Size; i++)
            {
                for (int j = 0; j < equation.Size; j++)
                {
                    if ((Math.Abs(equation.Coefficients[i, j]) > maxRestriction || Math.Abs(equation.Coefficients[i, j]) < minRestriction) && equation.Coefficients[i, j] != 0)
                    {
                        return false;
                    }
                }
                if ((Math.Abs(equation.Constants[i]) > maxRestriction || Math.Abs(equation.Constants[i]) < minRestriction) && equation.Constants[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Checks if the equation is solvable.
        /// </summary>
        /// <param name="equation">The equation to check.</param>
        /// <returns>True if the equation is solvable; otherwise, false.</returns>
        public static bool IsSolvable(Equation equation)
        {
            return (equation.CalculateDeterminant(equation.Coefficients) != 0);
        }
        /// <summary>
        /// Checks if the equation matrix is symmetric.
        /// </summary>
        /// <param name="equation">The equation to validate.</param>
        /// <returns>True if the matrix is symmetric; otherwise, false.</returns>
        public static bool IsSymetrical(Equation equation)
        {
            double[,] transposed = equation.Transpose(equation.Coefficients, equation.Size);
            for (int i = 0; i < equation.Size; i++)
            {
                for (int j = 0; j < equation.Size; j++)
                {
                    if (equation.Coefficients[i, j] != transposed[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Checks if the equation matrix is positive definite.
        /// </summary>
        /// <param name="equation">The equation to validate.</param>
        /// <returns>True if the matrix is positive definite; otherwise, false.</returns>
        public static bool IsPositiveDefinite(Equation equation)
        {
            int n = equation.Coefficients.GetLength(0);

            for (int k = 1; k <= n; k++)
            {
                double[,] minor = GetLeadingMinor(equation.Coefficients, k);
                double det = equation.CalculateDeterminant(minor);
                if (det <= 0)
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Retrieves the leading principal minor of the given size from a matrix.
        /// </summary>
        /// <param name="matrix">The input matrix.</param>
        /// <param name="size">The size of the leading minor.</param>
        /// <returns>The leading minor of the matrix.</returns>
        private static double[,] GetLeadingMinor(double[,] matrix, int size)
        {
            double[,] minor = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    minor[i, j] = matrix[i, j];
                }
            }
            return minor;
        }
        /// <summary>
        /// Checks if the equation is graphically solvable.
        /// </summary>
        /// <param name="equation">The equation to validate.</param>
        /// <returns>True if the equation is graphically solvable; otherwise, false.</returns>
        public static bool IsEquationGraphicallySolvable(Equation equation)
        {
            double[] maxMin = equation.FindMaximumMinimum();
            double maxCoef = maxMin[0];
            double minCoef = maxMin[1];
            return (Math.Abs(maxCoef - minCoef) <= graphicalDiffRestriction);
        }
    }
}
