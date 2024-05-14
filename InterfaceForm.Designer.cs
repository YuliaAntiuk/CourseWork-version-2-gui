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
            this.очиститиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.розвязатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.змінитиМетодToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.експортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обчислитиСкладністьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.інформаціяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimaryPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
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
            this.очиститиToolStripMenuItem,
            this.розвязатиToolStripMenuItem,
            this.змінитиМетодToolStripMenuItem,
            this.експортToolStripMenuItem,
            this.обчислитиСкладністьToolStripMenuItem,
            this.інформаціяToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // очиститиToolStripMenuItem
            // 
            this.очиститиToolStripMenuItem.Enabled = false;
            this.очиститиToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.очиститиToolStripMenuItem.Name = "очиститиToolStripMenuItem";
            this.очиститиToolStripMenuItem.Size = new System.Drawing.Size(99, 27);
            this.очиститиToolStripMenuItem.Text = "Очистити";
            // 
            // розвязатиToolStripMenuItem
            // 
            this.розвязатиToolStripMenuItem.Enabled = false;
            this.розвязатиToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.розвязатиToolStripMenuItem.Name = "розвязатиToolStripMenuItem";
            this.розвязатиToolStripMenuItem.Size = new System.Drawing.Size(108, 27);
            this.розвязатиToolStripMenuItem.Text = "Розв\'язати";
            // 
            // змінитиМетодToolStripMenuItem
            // 
            this.змінитиМетодToolStripMenuItem.Enabled = false;
            this.змінитиМетодToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.змінитиМетодToolStripMenuItem.Name = "змінитиМетодToolStripMenuItem";
            this.змінитиМетодToolStripMenuItem.Size = new System.Drawing.Size(138, 27);
            this.змінитиМетодToolStripMenuItem.Text = "Змінити метод";
            // 
            // експортToolStripMenuItem
            // 
            this.експортToolStripMenuItem.Enabled = false;
            this.експортToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.експортToolStripMenuItem.Name = "експортToolStripMenuItem";
            this.експортToolStripMenuItem.Size = new System.Drawing.Size(86, 27);
            this.експортToolStripMenuItem.Text = "Експорт";
            // 
            // обчислитиСкладністьToolStripMenuItem
            // 
            this.обчислитиСкладністьToolStripMenuItem.Enabled = false;
            this.обчислитиСкладністьToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.обчислитиСкладністьToolStripMenuItem.Name = "обчислитиСкладністьToolStripMenuItem";
            this.обчислитиСкладністьToolStripMenuItem.Size = new System.Drawing.Size(197, 27);
            this.обчислитиСкладністьToolStripMenuItem.Text = "Обчислити складність";
            // 
            // інформаціяToolStripMenuItem
            // 
            this.інформаціяToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.інформаціяToolStripMenuItem.Name = "інформаціяToolStripMenuItem";
            this.інформаціяToolStripMenuItem.Size = new System.Drawing.Size(115, 27);
            this.інформаціяToolStripMenuItem.Text = "Інформація";
            // 
            // PrimaryPanel
            // 
            this.PrimaryPanel.Controls.Add(this.label3);
            this.PrimaryPanel.Controls.Add(this.comboBoxMethods);
            this.PrimaryPanel.Controls.Add(this.label2);
            this.PrimaryPanel.Controls.Add(this.DimensionInput);
            this.PrimaryPanel.Controls.Add(this.label1);
            this.PrimaryPanel.Location = new System.Drawing.Point(0, 34);
            this.PrimaryPanel.Name = "PrimaryPanel";
            this.PrimaryPanel.Size = new System.Drawing.Size(882, 115);
            this.PrimaryPanel.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Заповніть коефіцієнти:";
            // 
            // comboBoxMethods
            // 
            this.comboBoxMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethods.FormattingEnabled = true;
            this.comboBoxMethods.Items.AddRange(new object[] {
            "LUP-метод",
            "Метод обертання",
            "Метод квадратного кореня"});
            this.comboBoxMethods.Location = new System.Drawing.Point(256, 48);
            this.comboBoxMethods.Name = "comboBoxMethods";
            this.comboBoxMethods.Size = new System.Drawing.Size(234, 24);
            this.comboBoxMethods.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Оберіть метод розв\'язання";
            // 
            // DimensionInput
            // 
            this.DimensionInput.Location = new System.Drawing.Point(372, 15);
            this.DimensionInput.Name = "DimensionInput";
            this.DimensionInput.Size = new System.Drawing.Size(118, 22);
            this.DimensionInput.TabIndex = 1;
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
        private System.Windows.Forms.ToolStripMenuItem очиститиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem розвязатиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem змінитиМетодToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem експортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обчислитиСкладністьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem інформаціяToolStripMenuItem;
        private System.Windows.Forms.Panel PrimaryPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DimensionInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMethods;
    }
}

