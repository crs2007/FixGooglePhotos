using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FixGooglePhotos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(directoryPath.Text))
            {
                MessageBox.Show("Please make sure directory path have value", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string vdirectoryPath;
            string vFileType = "json";
            vdirectoryPath = directoryPath.Text;
            if(!directoryPath.Text.EndsWith(@"\"))
                vdirectoryPath += @"\";

            DialogResult res = MessageBox.Show(String.Concat("Delete All ", vFileType.ToUpper(), " Files?"), "Delete Verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                deleteAllFilesByType(vdirectoryPath, vFileType);
                moveAllFilesByType(vdirectoryPath, vFileType);
                deleteAllEmptyDirectory(vdirectoryPath);
            }
        }

        private void deleteAllFilesByType(string directoryPath,string extension)
        {
            if (!Directory.Exists(directoryPath))
                return;

            var fileGenerationDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), directoryPath));
            fileGenerationDir.GetFiles(String.Concat("*.", extension), SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
           
        }

        private void moveAllFilesByType(string directoryPath, string extension)
        {
            if (!Directory.Exists(directoryPath))
                return;

            var fileGenerationDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), directoryPath));
            fileGenerationDir.GetFiles(String.Concat("*.", extension), SearchOption.AllDirectories).ToList().ForEach(file => file.MoveTo(directoryPath));

        }


        private void deleteAllEmptyDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            foreach (var directory in Directory.GetDirectories(directoryPath))
            {
                deleteAllEmptyDirectory(directoryPath);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }
    }
}
