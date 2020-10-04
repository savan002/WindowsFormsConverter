using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.Design;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "CSV files (*.csv)|*.csv";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                label1.Text = fdlg.FileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void add_an_element_to_xml(string FileName, string[] header, string[] row)
        {
            using (StreamWriter sw = File.AppendText(FileName))
            {
                for (int i = 0; i < header.Length; i++)
                {
                    sw.WriteLine("    <"+header[i]+">"+row[i]+"</"+header[i]+">");
                }
            }
        }

    private void button3_Click(object sender, EventArgs e)
        {
            int currentrow = 0;
            string[] headerXML= new string[0];
            string [] row;
            int RowCount = File.ReadLines(label1.Text).Count();
            string XML_filename = label1.Text.Replace("csv","xml");

            if (File.Exists(XML_filename))
            {
                MessageBox.Show("The File "+XML_filename+ " already exists. will be overwritten");
                File.Delete(XML_filename);         }
                        
            foreach (string line in File.ReadLines(label1.Text))
            {   currentrow++;
                if (currentrow == 1) {
                    string [] tempheaderXML = line.Split(textBox1.Text);
                    Array.Resize(ref headerXML, tempheaderXML.Length);
                    headerXML = tempheaderXML;
                   
                    using (StreamWriter sw = File.AppendText(XML_filename))
                    {   
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
                        sw.WriteLine("<FormerSCV>");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(XML_filename)) { if (radioButton1.Checked) { sw.WriteLine("  <tablerow>"); } else { sw.WriteLine("  <Element>"); } }
                    row = line.Split(textBox1.Text);
                    add_an_element_to_xml(XML_filename, headerXML, row);
                    using (StreamWriter sw = File.AppendText(XML_filename)) { if (radioButton1.Checked) { sw.WriteLine("  </tablerow>"); } else { sw.WriteLine("  </Element>"); } }
                }
                progressBar1.Value = currentrow / RowCount*100;
            }
            using (StreamWriter sw = File.AppendText(XML_filename)) {sw.WriteLine("</FormerSCV>");}
            MessageBox.Show("The File " + XML_filename + " created. Converting is fineshed.");

        }
    }
}
