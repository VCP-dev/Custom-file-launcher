using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLauncher
{
    public partial class Form1 : Form
    {

        string filepath = @"savedpath.txt";
        List<string> listfiles = new List<string>();

        public Form1()
        {
            InitializeComponent();

            try
            {                
                if (!File.Exists(filepath))
                {
                    StreamWriter file = new StreamWriter(filepath);
                    File.WriteAllText(filepath, string.Empty);
                }    
                else
                {
                    string path = File.ReadAllLines(filepath)[0];
                    txtPath.Text = path;                    
                    foreach (string item in Directory.GetFiles(path))
                    {
                        imageList1.Images.Add(Icon.ExtractAssociatedIcon(item));
                        FileInfo fi = new FileInfo(item);
                        listfiles.Add(fi.FullName);
                        listView1.Items.Add(fi.Name, imageList1.Images.Count - 1);
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            listfiles.Clear();
            listView1.Items.Clear();
            imageList1.Images.Clear();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description="Select required Path"})
            {
                try
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        txtPath.Text = fbd.SelectedPath;
                        File.WriteAllText(filepath, fbd.SelectedPath);                        
                        foreach (string item in Directory.GetFiles(fbd.SelectedPath))
                        {
                            imageList1.Images.Add(Icon.ExtractAssociatedIcon(item));
                            FileInfo fi = new FileInfo(item);
                            listfiles.Add(fi.FullName);
                            listView1.Items.Add(fi.Name, imageList1.Images.Count - 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            return;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                Process.Start(listfiles[listView1.FocusedItem.Index]);
                Application.Exit();
            }
        }
    }
}
