using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Represents the main interface form for the equation solver application.
    /// </summary>
    public partial class InterfaceForm : Form, IEventHandler
    {
        private EquationPanel equationPanel;
        private Equation equation;
        private GraphicalForm graphicalForm;
        private ResultPanel resultPanel;
        private int equationDimension;
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceForm"/> class.
        /// </summary>
        public InterfaceForm()
        {
            InitializeComponent();
            equationPanel = new EquationPanel();
            SelectLabel.Text += $"(|{Validation.minRestriction}| - |{Validation.maxRestriction.ToString("0.E+0")}| або 0):";
        }
        /// <summary>
        /// Reads the values of the equations from the input controls.
        /// </summary>
        private void ReadEquationsValues()
        {
            double[,] coefficients = new double[equationDimension, equationDimension];
            double[] constants = new double[equationDimension];
            for (int i = 0; i < equationDimension; i++)
            {
                for (int j = 0; j < equationDimension; j++)
                {
                    TextBox coefficientTextBox = (TextBox)equationPanel.Controls[$"textBoxCoeff{i + 1}{j + 1}"];
                    if (coefficientTextBox != null && double.TryParse(coefficientTextBox.Text, out double coefficient))
                    {
                        coefficients[i, j] = coefficient;
                    } else
                    {
                        MessageBox.Show("Не вдається записати коефіцієнти", "Помилка запису", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }

                TextBox constantTextBox = (TextBox)equationPanel.Controls[$"textBoxConstant{i + 1}"];
                if (constantTextBox != null && double.TryParse(constantTextBox.Text, out double constant))
                {
                    constants[i] = constant;
                }
                else
                {
                    MessageBox.Show("Не вдається записати коефіцієнти", "Помилка запису", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.equation = new Equation(coefficients, constants, equationDimension);
        }
        /// <summary>
        /// Handles the KeyPress event of the DimensionInput control to set up the equations input fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void DimensionInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (int.TryParse(DimensionInput.Text, out equationDimension))
                {
                    if (equationDimension < 2 || equationDimension > 10)
                    {
                        MessageBox.Show("Розмірність системи повинна бути між 2 та 10", "Помилка вводу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        equationPanel.DisplayEquationsInput(equationDimension);
                        equationPanel.Location = new Point(15, PrimaryPanel.Bottom + 15);
                        equationPanel.Width = PrimaryPanel.Width;
                        Controls.Add(equationPanel);
                        foreach(Control control in equationPanel.Controls)
                        {
                            if(control is TextBox textBox)
                            {
                                control.TextChanged += TextBox_TextChanged;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректну розмірність системи.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (equationDimension == 2)
                {
                    if (!Validation.IsItemInComboBox("Графічний метод", comboBoxMethods))
                    {
                        comboBoxMethods.Items.Add("Графічний метод");
                    }
                }
                else
                {
                    comboBoxMethods.Items.Remove("Графічний метод");
                }
            }
        }
        /// <summary>
        /// Disables the input controls.
        /// </summary>
        private void DisableInputs()
        {
            DimensionInput.Enabled = false;
            comboBoxMethods.Enabled = false;
            foreach (Control control in equationPanel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Enables the input controls.
        /// </summary>
        private void EnableInputs()
        {
            DimensionInput.Enabled = true;
            comboBoxMethods.Enabled = true;
            foreach (Control control in equationPanel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Enables the menu items.
        /// </summary>
        private void EnableMenuItems()
        {
            ClearToolStripMenuItem.Enabled = true;
            ChangeToolStripMenuItem.Enabled = true;
            ExportToolStripMenuItem.Enabled = true;
            if(comboBoxMethods.SelectedItem.ToString() != "Графічний метод")
            {
                ComplexityToolStripMenuItem.Enabled = true;
            }
        }
        /// <summary>
        /// Disables the menu items.
        /// </summary>
        private void DisableMenuItems()
        {
            ClearToolStripMenuItem.Enabled = false;
            ChangeToolStripMenuItem.Enabled = false;
            ExportToolStripMenuItem.Enabled = false;
            ComplexityToolStripMenuItem.Enabled = false;
            SolveToolStripMenuItem.Enabled = false;
        }
        /// <summary>
        /// Updates the state of the Solve menu item based on the input validation.
        /// </summary>
        private void UpdateSolveMenuState()
        {
            bool areCoefficientsEntered = true;
            bool areConstantsEntered = true;

            foreach (TextBox textBox in equationPanel.coefficientTextBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    areCoefficientsEntered = false;
                    break;
                }
            }

            foreach (TextBox textBox in equationPanel.constantTextBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    areConstantsEntered = false;
                    break;
                }
            }
            SolveToolStripMenuItem.Enabled = Validation.IsDimensionEntered(DimensionInput) && Validation.IsMethodSelected(comboBoxMethods) && areCoefficientsEntered && areConstantsEntered;
        }
        /// <summary>
        /// Solves the equation system using the selected method.
        /// </summary>
        /// <param name="selectedMethod">The selected method to solve the equation system.</param>
        private void SolveEquation(string selectedMethod)
        {
            switch (selectedMethod)
            {
                case "Метод квадратного кореня":
                    if (!Validation.IsPositiveDefinite(equation) || !Validation.IsSymetrical(equation))
                    {
                        MessageBox.Show("Матриця коефіцієнтів несиметрична або від'ємно визначена", "Систему неможливо розв'язати даним методом", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DisableInputs();
                        return;
                    }
                    else
                    {
                        equation.CalculateSqrtMethod();
                    }
                    break;
                case "Метод обертання":
                    equation.CalculateRotationMethod();
                    break;
                case "LUP-метод":
                    equation.CalculateLUPMethod();
                    break;
                case "Графічний метод":
                    if (!Validation.IsEquationGraphicallySolvable(equation))
                    {
                        MessageBox.Show("Система має невалідні коефіцієнти для відображення графіка", "Систему неможливо розв'язати даним методом", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DisableInputs();
                        return;
                    }
                    else
                    {
                        if (graphicalForm == null || graphicalForm.IsDisposed)
                        {
                            graphicalForm = new GraphicalForm(equation);
                        }
                        graphicalForm.CreateGraphic();
                    }
                    break;
                default:
                    break;
            }
            OutputResults(equation.Result);
        }
        /// <summary>
        /// Outputs the results of the equation solving process.
        /// </summary>
        /// <param name="result">The result of the equation solving process.</param>
        private void OutputResults(double[] result)
        {
            int offset = 15;
            int panelX = 18;
            int panelY = equationPanel.Bottom + offset;
            resultPanel = new ResultPanel(result);
            resultPanel.Width = 300;
            resultPanel.Height = 12 * result.Length;
            resultPanel.Name = "resultPanel";
            resultPanel.Location = new System.Drawing.Point(panelX, panelY);
            this.Controls.Add(resultPanel);
            resultPanel.UpdatePanelContent();
            SolveToolStripMenuItem.Enabled = false;
            int controlPanelY = resultPanel.Bottom + offset;
            DisableInputs();
            EnableMenuItems();
        }
        /// <summary>
        /// Handles the Click event of the ClearToolStripMenuItem to clear the input fields and reset the form.
        /// </summary>        
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equationPanel.Controls.Clear();
            equationPanel.Height = 0;
            DimensionInput.Text = "";
            comboBoxMethods.SelectedItem = null;
            comboBoxMethods.Items.Remove("Графічний метод");
            if (graphicalForm != null)
            {
                graphicalForm.Dispose();
            }
            Controls.RemoveByKey("resultPanel");
            Controls.RemoveByKey("complexityLabel");
            EnableInputs();
            DisableMenuItems();
        }
        /// <summary>
        /// Handles the Click event of the SolveToolStripMenuItem to solve the equations.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void SolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int dimension = Convert.ToInt32(DimensionInput.Text);
            ReadEquationsValues();
            string selectedMethod = comboBoxMethods.SelectedItem.ToString();
            if (!Validation.IsEquationValid(equation))
            {
                MessageBox.Show($"Коефіцієнти виходять за межі обмежень\n|{Validation.minRestriction}| - |{Validation.maxRestriction.ToString("0.E+0")}| або 0", "Невалідні коефіцієнти", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableMenuItems();
            }
            else if (!Validation.IsSolvable(equation))
            {
                MessageBox.Show("Система має нуль або безліч розв'язків", "Нульовий визначник", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableMenuItems();
            }
            else
            {
                try
                {
                    SolveEquation(selectedMethod);
                    SolveToolStripMenuItem.Enabled = false;
                    ChangeToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка в обчисленнях", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisableMenuItems();
                }
            }
            ClearToolStripMenuItem.Enabled = true;
        }
        /// <summary>
        /// Handles the TextChanged event of the DimensionInput control to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void DimensionInput_TextChanged(object sender, EventArgs e)
        {
            UpdateSolveMenuState(); 
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ComboBox to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSolveMenuState();
        }
        /// <summary>
        /// Handles the TextChanged event of the TextBox controls to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSolveMenuState(); 
        }
        /// <summary>
        /// Handles the Click event of the ChangeToolStripMenuItem to enable the inputs and reset the result display.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableMenuItems();
            SolveToolStripMenuItem.Enabled = true;
            if(graphicalForm != null)
            {
                graphicalForm.Dispose();
            }
            DisableInputs();
            comboBoxMethods.Enabled = true;
            Controls.RemoveByKey("resultPanel");
            Controls.RemoveByKey("complexityLabel");
        }
        /// <summary>
        /// Handles the Click event of the ExportToolStripMenuItem to export the equation data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export export = new Export(equation);
            export.OpenExportFile();
        }
        /// <summary>
        /// Handles the Click event of the ComplexityToolStripMenuItem to display the complexity of the solution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        public void ComplexityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Label complexityLabel = new Label();
            complexityLabel.Text = $"Практична складність - {equation.IterationCounter}";
            complexityLabel.AutoSize = true;
            complexityLabel.Name = "complexityLabel";
            complexityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular);
            complexityLabel.Location = new System.Drawing.Point(18, resultPanel.Bottom + 15);
            this.Controls.Add(complexityLabel);
        }
    }
}
