using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace LZWAlgorithms
{
    public partial class MainForm : Form
    {

        
        string encodedFile = "TestOutput.exe";
        string decodedFile = "TestDecodedOutput.exe";

        private string fileContent = string.Empty;
        private string fileName = string.Empty;
        public MainForm()
        {
            InitializeComponent();
           textBox2.Text +="Generate ANSI table ..."+Environment.NewLine;

            ANSI ascii = new ANSI();
            ascii.WriteToFile();

            textBox2.Text += "ANSI table generated"+Environment.NewLine;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileContent != string.Empty)
            {
                string text = fileContent;

                LZWEncoder encoder = new LZWEncoder();
                byte[] b = encoder.EncodeToByteList(text);
                File.WriteAllBytes(encodedFile, b);

                textBox2.Text += "File " + fileName + " encoded to " + encodedFile + Environment.NewLine;
            }
            else
            {
                MessageBox.Show(
                "Choose file",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fileContent != string.Empty)
            {
                string text = fileContent;

                textBox2.Text += "Start decoding " + encodedFile + Environment.NewLine;

                LZWDecoder decoder = new LZWDecoder();
                byte[] bo = File.ReadAllBytes(encodedFile);
                string decodedOutput = decoder.DecodeFromCodes(bo);
                File.WriteAllText(decodedFile, decodedOutput, System.Text.Encoding.Default);

                textBox2.Text += "File " + encodedFile + " decoded to " + decodedFile + Environment.NewLine;
            }
            else
            {
                MessageBox.Show(
                "Choose file",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fileName = string.Empty;
            fileContent = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the name and path of specified file
                    fileName = openFileDialog.FileName;
                    fileContent = File.ReadAllText(fileName, System.Text.ASCIIEncoding.Default);

                    textBox2.Text += "File "+fileName+" is opened" + Environment.NewLine;
                    textBox2.Text += "File " + fileName + " has length="+ fileContent.Length.ToString() + Environment.NewLine;
                }
            }
        }
    }
}
