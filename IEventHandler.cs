using System;
using System.Windows.Forms;

namespace GUI
{
    public interface IEventHandler
    {
        void DimensionInput_KeyPress(object sender, KeyPressEventArgs e);
        void ClearToolStripMenuItem_Click(object sender, EventArgs e);
        void SolveToolStripMenuItem_Click(object sender, EventArgs e);
        void DimensionInput_TextChanged(object sender, EventArgs e);
        void ComboBox_SelectedIndexChanged(object sender, EventArgs e);
        void TextBox_TextChanged(object sender, EventArgs e);
        void ChangeToolStripMenuItem_Click(object sender, EventArgs e);
        void ExportToolStripMenuItem_Click(object sender, EventArgs e);
        void ComplexityToolStripMenuItem_Click(object sender, EventArgs e);
    }
}
