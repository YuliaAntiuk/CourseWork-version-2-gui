using System.Text.RegularExpressions;
using System.Windows.Forms;
using System;
namespace GUI
{
    public static class Validation
    {
        public static double maxRestriction = 1e6;
        public static double minRestriction = 1e-6;
        public static bool IsDimensionEntered(TextBox DimensionInput)
        {
            if (int.TryParse(DimensionInput.Text, out int dimension))
            {
                return dimension > 0 && dimension <= 10;
            }
            return false;
        }
        public static bool IsMethodSelected(ComboBox comboBox)
        {
            return comboBox.SelectedIndex != -1;
        }
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
        public static bool IsSolvable(Equation equation)
        {
            return (equation.CalculateDeterminant(equation.Coefficients) != 0);
        }
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
        public static bool IsEquationGraphicallySolvable(Equation equation)
        {
            double[] maxMin = equation.FindMaximumMinimum();
            double maxCoef = maxMin[0];
            double minCoef = maxMin[1];
            return ((maxCoef > minRestriction || maxCoef < maxRestriction || maxCoef == 0) && (minCoef > minRestriction || minCoef < maxRestriction || minCoef == 0));
        }
    }
}
