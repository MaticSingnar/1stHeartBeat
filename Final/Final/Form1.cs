

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Final
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public string ScanQRCode(Bitmap qrCodeImage)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(qrCodeImage);
            return result?.Text; // Returns the QR code content as text
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
            {
                string query = "SELECT * FROM LOGIN WHERE username = '" + textBox1.Text.Trim() + "'AND password = '" + textBox2.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                if (dta.Rows.Count == 1)
                {
                    string username = textBox1.Text.Trim(); // Retrieve the username directly from the TextBox
                    Dashboard dashboard = new Dashboard(username);
                    this.Hide();
                    dashboard.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the DestinationForm
            Register destinationForm = new Register();

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create an instance of the DestinationForm
            Doctor destinationForm = new Doctor();

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Open file dialog to select a QR code image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            openFileDialog.Title = "Select QR Code Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load selected image file as Bitmap
                    Bitmap qrCodeImage = new Bitmap(openFileDialog.FileName);

                    // Scan QR code
                    string scannedData = ScanQRCode(qrCodeImage);

                    if (!string.IsNullOrEmpty(scannedData))
                    {
                        // Show details in a popup dialog
                        MessageBox.Show($"Scanned Data:\n{scannedData}", "QR Code Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No QR Code found or could not decode.", "QR Code Scan Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error scanning QR Code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }
