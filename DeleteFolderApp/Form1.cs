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

namespace DeleteFolderApp
{
    public partial class Form1 : Form
    {
        string path = @"local path";
        public Form1()
        {
            InitializeComponent();
            txtPath.Text = path;
        }

        public static void DeepDelete(string targetDirectory)
        {
            try
            {
                string[] files = Directory.GetFiles(targetDirectory);
                string[] dirs = Directory.GetDirectories(targetDirectory);
                foreach (string file in files)
                {
                    // Remove read-only access attributes from the files right before delete them
                    // Otherwise that will raise an exception
                    if (file.IndexOf("bin") > -1 || file.IndexOf("obj") > -1)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                }
                foreach (string dir in dirs)
                {
                    //Directory.Delete(dir);
                    //if (dir.IndexOf("bin") > -1 || dir.IndexOf("obj") > -1)
                    //{
                        DeepDelete(dir);
                    //}
                }
                if (targetDirectory.IndexOf("bin") > -1 || targetDirectory.IndexOf("obj") > -1)
                {
                    Directory.Delete(targetDirectory, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeepDelete(path);

                MessageBox.Show("done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
