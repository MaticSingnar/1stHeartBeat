
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
using Microsoft.Web.WebView2.Core;

namespace Final
{
    public partial class Upload : Form
    {
        public Upload()
        {
            InitializeComponent();
            InitializeWebView2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select File to Upload";
            openFileDialog.Filter = "All Files (*.*)|*.*"; // Allow all file types
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Open at desktop by default

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string filePath = openFileDialog.FileName;

                // Perform actions with the selected file path, such as uploading to a server or processing the file
                MessageBox.Show("Selected file: " + filePath);
                // Read the file content and display it
                DisplayFileContent(filePath);
            }
        }
        private void DisplayFileContent(string filePath)
        {
            try
            {
                // Determine the file type
                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".txt" || extension == ".log" || extension == ".csv")
                {
                }
                else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif")
                {
                    {
                        MessageBox.Show("File type not supported for display.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save Photo";
                saveFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Allow common image file types

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                }

                else
                {
                    MessageBox.Show("No photo to save.");
                }
            }

        }

        private async void InitializeWebView2()
        {
            // Ensure the WebView2 control is initialized
            await webView21.EnsureCoreWebView2Async(null);
            // Navigate to the desired URL
            webView21.CoreWebView2.Navigate("https://v2.akord.com/storage");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Navigate to a different URL
            webView21.CoreWebView2.Navigate("https://v2.akord.com/storage");



        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }
    }

}