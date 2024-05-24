using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public class Export
    {
        /// <summary>
        /// Gets or sets the data to be exported as a string.
        /// </summary>
        private string Data { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Export"/> class with the given equation.
        /// Constructs the export data string from the equation's coefficients, constants, and results.
        /// </summary>
        /// <param name="equation">The equation to export.</param>
        public Export(Equation equation)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Матриця коефіцієнтів:");
            sb.AppendLine("[");
            for (int i = 0; i < equation.Size; i++)
            {
                sb.Append("[");
                for (int j = 0; j < equation.Size; j++)
                {
                    sb.Append(equation.Coefficients[i, j]);
                    if (j < equation.Size - 1)
                        sb.Append(", ");
                }
                sb.AppendLine("]");
            }
            sb.AppendLine("]\nВектор вільних членів:");
            sb.Append("[");
            for (int i = 0; i < equation.Size; i++)
            {
                sb.Append(equation.Constants[i]);
                if (i < equation.Size - 1)
                    sb.Append(", ");
            }
            sb.AppendLine("]");
            sb.Append("Розв'язок: [");
            for (int i = 0; i < equation.Size; i++)
            {
                sb.Append(equation.Result[i]);
                if (i < equation.Size - 1)
                    sb.Append(", ");
            }
            sb.AppendLine("]");
            Data = sb.ToString();
        }
        /// <summary>
        /// Exports the data to a specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to export to.</param>
        private void ExportToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(Data);
                }
                MessageBox.Show("Дані успішно експортовано до файлу: " + fileName, "Експорт завершено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при експорті даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Opens a file dialog to select a file for export and appends the current data to the selected file.
        /// </summary>
        public void OpenExportFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt";
            openFileDialog.Title = "Вибрати файл для експорту";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                string existingFileContent;
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        existingFileContent = reader.ReadToEnd();
                    }
                    Data = existingFileContent + "\n\n" + Data;
                    ExportToFile(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
