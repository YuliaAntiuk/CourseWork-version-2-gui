using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public class EquationPanel:Panel
    {
        /// <summary>
        /// List of TextBox controls for entering the coefficients of the equations.
        /// </summary>
        public readonly List<TextBox> coefficientTextBoxes;
        /// <summary>
        /// List of TextBox controls for entering the constant terms of the equations.
        /// </summary>
        public readonly List<TextBox> constantTextBoxes;
        /// <summary>
        /// Initializes a new instance of the <see cref="EquationPanel"/> class.
        /// </summary>
        public EquationPanel()
        {
            coefficientTextBoxes = new List<TextBox>();
            constantTextBoxes = new List<TextBox>();
        }
        /// <summary>
        /// Creates a TextBox for entering an equation coefficient or constant.
        /// </summary>
        /// <param name="x">The x-coordinate of the TextBox location.</param>
        /// <param name="y">The y-coordinate of the TextBox location.</param>
        /// <param name="width">The width of the TextBox.</param>
        /// <param name="name">The name of the TextBox.</param>
        /// <returns>A configured TextBox control.</returns>
        private TextBox CreateEquationInput(int x, int y, int width, string name)
        {
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.Width = width;
            textBox.Location = new Point(x, y);
            textBox.Validating += Validation.TextBox_Validating;
            textBox.Font = new Font("Segoe UI", 8);
            return textBox;
        }
        /// <summary>
        /// Creates a Label for the equation input panel.
        /// </summary>
        /// <param name="x">The x-coordinate of the Label location.</param>
        /// <param name="y">The y-coordinate of the Label location.</param>
        /// <param name="text">The text of the Label.</param>
        /// <returns>A configured Label control.</returns>
        private Label CreateEquationLabel(int x, int y, string text)
        {
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;
            label.Location = new Point(x, y);
            label.Font = new Font("Segoe UI", 8);
            return label;
        }
        /// <summary>
        /// Clears all controls from the panel and resets the TextBox lists.
        /// </summary>
        private void ClearPanel()
        {
            Controls.Clear();
            coefficientTextBoxes.Clear();
            constantTextBoxes.Clear();
        }
        /// <summary>
        /// Displays input controls for entering the coefficients and constants of a system of linear equations.
        /// </summary>
        /// <param name="dimension">The number of equations (and unknowns) in the system.</param>
        public void DisplayEquationsInput(int dimension)
        {
            ClearPanel();
            const int textBoxWidth = 50;
            const int textBoxSpacing = 5;
            int yOffset = 30;

            for (int i = 0; i < dimension; i++)
            {
                int x = 0;
                for (int j = 0; j < dimension; j++)
                {
                    TextBox coefficientTextBox = CreateEquationInput(x, yOffset * i, textBoxWidth, $"textBoxCoeff{i + 1}{j + 1}");
                    coefficientTextBoxes.Add(coefficientTextBox);
                    Controls.Add(coefficientTextBox);
                    x = coefficientTextBox.Right + textBoxSpacing;
                }
                Label variableLabel = CreateEquationLabel(x, yOffset * i, $"x{i + 1}");
                Controls.Add(variableLabel);
                x = variableLabel.Right + textBoxSpacing;

                Label equalsLabel = CreateEquationLabel(x, yOffset * i, " = ");
                Controls.Add(equalsLabel);
                x = equalsLabel.Right + textBoxSpacing;

                TextBox constantTextBox = CreateEquationInput(x, yOffset * i, textBoxWidth, $"textBoxConstant{i + 1}");
                constantTextBoxes.Add(constantTextBox);
                Controls.Add(constantTextBox);
            }
            Height = yOffset * dimension;
        }
    }
}
