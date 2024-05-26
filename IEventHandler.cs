using System;
using System.Windows.Forms;

namespace GUI
{
    public interface IEventHandler
    {
        /// <summary>
        /// Handles the KeyPress event of the DimensionInput control to set up the equations input fields.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void DimensionInput_KeyPress(object sender, KeyPressEventArgs e);
        /// <summary>
        /// Handles the Click event of the ClearToolStripMenuItem to clear the input fields and reset the form.
        /// </summary>        
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void ClearToolStripMenuItem_Click(object sender, EventArgs e);
        /// <summary>
        /// Handles the Click event of the SolveToolStripMenuItem to solve the equations.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void SolveToolStripMenuItem_Click(object sender, EventArgs e);
        /// <summary>
        /// Handles the TextChanged event of the DimensionInput control to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void DimensionInput_TextChanged(object sender, EventArgs e);
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ComboBox to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void ComboBox_SelectedIndexChanged(object sender, EventArgs e);
        /// <summary>
        /// Handles the TextChanged event of the TextBox controls to update the solve menu state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void TextBox_TextChanged(object sender, EventArgs e);
        /// <summary>
        /// Handles the Click event of the ChangeToolStripMenuItem to enable the inputs and reset the result display.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void ChangeToolStripMenuItem_Click(object sender, EventArgs e);
        /// <summary>
        /// Handles the Click event of the ExportToolStripMenuItem to export the equation data.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void ExportToolStripMenuItem_Click(object sender, EventArgs e);
        /// <summary>
        /// Handles the Click event of the ComplexityToolStripMenuItem to display the complexity of the solution.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        void ComplexityToolStripMenuItem_Click(object sender, EventArgs e);
    }
}
