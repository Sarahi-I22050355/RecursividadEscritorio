using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursividadEscritorio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a storage drive (excluding C:)";
                folderDialog.ShowNewFolderButton = false;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderDialog.SelectedPath;
                    string selectedDrive = Path.GetPathRoot(selectedPath);
                    MessageBox.Show("You have selected the drive: " + selectedDrive);

                    // Llamar a ListFiles con la unidad seleccionada
                    if (!IsDriveC(selectedDrive))
                    {
                        ListFiles(selectedPath);
                    }
                    else
                    {
                        richTextBox1.AppendText("You cannot list files on drive C:.");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private bool IsDriveC(string drive)
        {
            return drive.ToUpper() == "C:";
        }

        private void ListFiles(string directory)
        {
            try
            {
                // Listar todos los archivos en el directorio actual
                string[] files = Directory.GetFiles(directory);

                // Mostrar el nombre de cada archivo en el RichTextBox
                foreach (string file in files)
                {
                    richTextBox1.AppendText(Path.GetFileName(file) + Environment.NewLine);
                }

                // Listar todos los subdirectorios en el directorio actual
                string[] subdirectories = Directory.GetDirectories(directory);

                // Llamar al método recursivamente para cada subdirectorio
                foreach (string subdirectory in subdirectories)
                {
                    ListFiles(subdirectory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error listing files in {directory}: {ex.Message}", "Error");
            }
        }
    }
}