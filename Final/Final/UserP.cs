using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Final
{
    public partial class UserP : Form
    {
        public UserP()
        {
            InitializeComponent();
        }

        // Assuming you have a PictureBox named pictureBoxProfile and a Button named buttonBrowse
        // Place these controls on your form using Visual Studio designer

        private void InitializeComponents()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();

            // Set properties of pictureBoxProfile
            this.pictureBox1.Location = new System.Drawing.Point(50, 50);
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Set properties of buttonBrowse
            this.button1.Location = new System.Drawing.Point(50, 220);
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.Text = "Browse...";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // Add controls to the form
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);

            // Set other form properties as needed
            this.Text = "Profile Picture Example";
        }



        private void UserP_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Profile Picture";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display selected image in PictureBox
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Assuming you have saved the image path or byte array to a database or file system
            // For simplicity, let's assume saving to a file

            if (pictureBox1.Image != null)
            {
                string imagePath = @"C:\Users\sarki\source\repos\Final\Final\.jpg"; // Specify your desired path

                pictureBox1.Image.Save(imagePath, ImageFormat.Jpeg);

                // Optionally, you can store imagePath in a database or configuration file
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string imagePath = @"C:\Users\sarki\source\repos\Final\Final\.jpg"; // Path where image is saved

            if (File.Exists(imagePath))
            {
                pictureBox1.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Profile picture not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
