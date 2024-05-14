using GUI_Demo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class InterfaceForm : Form
    {
        private EquationPanel equationPanel;
        private Equation equation;
        public InterfaceForm()
        {
            InitializeComponent();
            equationPanel = new EquationPanel();
        }
        private void DimensionInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (int.TryParse(DimensionInput.Text, out int dimension))
                {
                    if (dimension < 2 || dimension > 10)
                    {
                        MessageBox.Show("Розмірність системи повинна бути між 2 та 10", "Помилка введення", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        equationPanel.DisplayEquationsInput(dimension);
                        equationPanel.Location = new Point(15, PrimaryPanel.Bottom + 15);
                        equationPanel.Width = PrimaryPanel.Width;
                        Controls.Add(equationPanel);
                    }
                }
                else
                {
                    MessageBox.Show("Будь ласка, введіть коректну розмірність системи.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (dimension == 2)
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
    }
}
