using System.Windows.Forms;

namespace GUI
{
    public class ResultPanel : Panel
    {
        private double[] Results { get; set; }
        public ResultPanel(double[] results)
        {
            Results = results;
            this.AutoScroll = true;
        }
        public void UpdatePanelContent()
        {
            this.Controls.Clear();

            int labelWidth = 100;  // Width of each label, adjust as needed
            int labelHeight = 30;  // Height of each label
            int labelsPerRow = 3;  // Number of labels per row

            for (int i = 0; i < Results.Length; i++)
            {
                Label resultLabel = new Label();
                string resultsToPrint = Results[i].ToString("0.000");
                resultLabel.Text = $"x{i + 1} = {resultsToPrint}";
                resultLabel.AutoSize = true;
                resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular);

                int row = i / labelsPerRow;
                int column = i % labelsPerRow;

                resultLabel.Location = new System.Drawing.Point(column * labelWidth, row * labelHeight);
                this.Controls.Add(resultLabel);
            }
        }

    }
}
