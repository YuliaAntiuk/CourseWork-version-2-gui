using GUI_Demo;
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
        public readonly List<TextBox> coefficientTextBoxes;
        public readonly List<TextBox> constantTextBoxes;
        public EquationPanel()
        {
            coefficientTextBoxes = new List<TextBox>();
            constantTextBoxes = new List<TextBox>();
        }
        private TextBox CreateEquationInput(int x, int y, int width, string name)
        {
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.Width = width;
            textBox.Location = new Point(x, y);
            textBox.Validating += Validation.TextBox_Validating;
            return textBox;
        }
        private Label CreateEquationLabel(int x, int y, string text)
        {
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;
            label.Location = new Point(x, y);
            return label;
        }
        public void DisplayEquationsInput(int dimension)
        {
            Controls.Clear();
            coefficientTextBoxes.Clear();
            constantTextBoxes.Clear();
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
