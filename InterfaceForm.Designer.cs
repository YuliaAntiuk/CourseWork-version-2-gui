namespace GUI
{
    partial class InterfaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComplexityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimaryPanel = new System.Windows.Forms.Panel();
            this.SelectLabel = new System.Windows.Forms.Label();
            this.comboBoxMethods = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DimensionInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.PrimaryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearToolStripMenuItem,
            this.SolveToolStripMenuItem,
            this.ChangeToolStripMenuItem,
            this.ExportToolStripMenuItem,
            this.ComplexityToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Enabled = false;
            this.ClearToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(99, 27);
            this.ClearToolStripMenuItem.Text = "Очистити";
            this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // SolveToolStripMenuItem
            // 
            this.SolveToolStripMenuItem.Enabled = false;
            this.SolveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.SolveToolStripMenuItem.Name = "SolveToolStripMenuItem";
            this.SolveToolStripMenuItem.Size = new System.Drawing.Size(108, 27);
            this.SolveToolStripMenuItem.Text = "Розв\'язати";
            this.SolveToolStripMenuItem.Click += new System.EventHandler(this.SolveToolStripMenuItem_Click);
            // 
            // ChangeToolStripMenuItem
            // 
            this.ChangeToolStripMenuItem.Enabled = false;
            this.ChangeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ChangeToolStripMenuItem.Name = "ChangeToolStripMenuItem";
            this.ChangeToolStripMenuItem.Size = new System.Drawing.Size(138, 27);
            this.ChangeToolStripMenuItem.Text = "Змінити метод";
            this.ChangeToolStripMenuItem.Click += new System.EventHandler(this.ChangeToolStripMenuItem_Click);
            // 
            // ExportToolStripMenuItem
            // 
            this.ExportToolStripMenuItem.Enabled = false;
            this.ExportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            this.ExportToolStripMenuItem.Size = new System.Drawing.Size(86, 27);
            this.ExportToolStripMenuItem.Text = "Експорт";
            this.ExportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // ComplexityToolStripMenuItem
            // 
            this.ComplexityToolStripMenuItem.Enabled = false;
            this.ComplexityToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComplexityToolStripMenuItem.Name = "ComplexityToolStripMenuItem";
            this.ComplexityToolStripMenuItem.Size = new System.Drawing.Size(197, 27);
            this.ComplexityToolStripMenuItem.Text = "Обчислити складність";
            this.ComplexityToolStripMenuItem.Click += new System.EventHandler(this.ComplexityToolStripMenuItem_Click);
            // 
            // PrimaryPanel
            // 
            this.PrimaryPanel.Controls.Add(this.SelectLabel);
            this.PrimaryPanel.Controls.Add(this.comboBoxMethods);
            this.PrimaryPanel.Controls.Add(this.label2);
            this.PrimaryPanel.Controls.Add(this.DimensionInput);
            this.PrimaryPanel.Controls.Add(this.label1);
            this.PrimaryPanel.Location = new System.Drawing.Point(0, 34);
            this.PrimaryPanel.Name = "PrimaryPanel";
            this.PrimaryPanel.Size = new System.Drawing.Size(882, 124);
            this.PrimaryPanel.TabIndex = 1;
            // 
            // SelectLabel
            // 
            this.SelectLabel.AutoSize = true;
            this.SelectLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.SelectLabel.Location = new System.Drawing.Point(12, 94);
            this.SelectLabel.Name = "SelectLabel";
            this.SelectLabel.Size = new System.Drawing.Size(193, 23);
            this.SelectLabel.TabIndex = 4;
            this.SelectLabel.Text = "Заповніть коефіцієнти ";
            // 
            // comboBoxMethods
            // 
            this.comboBoxMethods.DropDownHeight = 110;
            this.comboBoxMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethods.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxMethods.FormattingEnabled = true;
            this.comboBoxMethods.IntegralHeight = false;
            this.comboBoxMethods.ItemHeight = 23;
            this.comboBoxMethods.Items.AddRange(new object[] {
            "LUP-метод",
            "Метод обертання",
            "Метод квадратного кореня"});
            this.comboBoxMethods.Location = new System.Drawing.Point(256, 50);
            this.comboBoxMethods.Name = "comboBoxMethods";
            this.comboBoxMethods.Size = new System.Drawing.Size(234, 31);
            this.comboBoxMethods.TabIndex = 3;
            this.comboBoxMethods.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Оберіть метод розв\'язання";
            // 
            // DimensionInput
            // 
            this.DimensionInput.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.DimensionInput.Location = new System.Drawing.Point(372, 13);
            this.DimensionInput.Name = "DimensionInput";
            this.DimensionInput.Size = new System.Drawing.Size(118, 25);
            this.DimensionInput.TabIndex = 1;
            this.DimensionInput.TextChanged += new System.EventHandler(this.DimensionInput_TextChanged);
            this.DimensionInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DimensionInput_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введіть розмірність системи (2 -10):";
            // 
            // InterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.PrimaryPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1970, 1800);
            this.MinimumSize = new System.Drawing.Size(850, 600);
            this.Name = "InterfaceForm";
            this.Text = "Розв\'язання СЛАР точними методами";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PrimaryPanel.ResumeLayout(false);
            this.PrimaryPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SolveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ComplexityToolStripMenuItem;
        private System.Windows.Forms.Panel PrimaryPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DimensionInput;
        private System.Windows.Forms.Label SelectLabel;
        private System.Windows.Forms.ComboBox comboBoxMethods;
    }
}

