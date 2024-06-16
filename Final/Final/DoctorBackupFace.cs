using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Final
{
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
            {
                string query = "SELECT * FROM DOCTOR WHERE username = '" + textBox1.Text.Trim() + "'AND password = '" + textBox2.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                if (dta.Rows.Count == 1)
                {
                    string username = textBox1.Text.Trim(); // Retrieve the username directly from the TextBox
                    DoctorDashboard dashboard = new DoctorDashboard(username);
                    this.Hide();
                    dashboard.Show();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True");
            connection.Open();
            string insertQuery = "insert into DOCTOR VALUES ( @username, @password, @gender, @email)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@username", textBox3.Text.Trim());
            command.Parameters.AddWithValue("@password", textBox4.Text.Trim());

            command.Parameters.AddWithValue("@email", textBox5.Text.Trim());
            command.Parameters.AddWithValue("@gender",  comboBox1.SelectedItem.ToString());
            command.ExecuteNonQuery();
            MessageBox.Show("Register Successfully");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            

            
            {
                // Email address you want to send the email to
                string emailAddress = "example@example.com";

                // Subject of the email
                string subject = "Subject of the email";

                // Body of the email
                string body = "Body of the email";

                // Format the mailto URI
                string uri = $"mailto:{emailAddress}?subject={subject}&body={body}";

                // Start the default email client with the pre-filled email
                Process.Start(uri);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Image originalImage = Image.FromFile("C:\\Users\\sarki\\source\\repos\\Final\\Final\\Resources\\Dr.-Dinesh-Bhatia.jpg");
            int desiredWidth = 100; // Specify the desired width
            int desiredHeight = 100; // Specify the desired height
            Image resizedImage = new Bitmap(originalImage, desiredWidth, desiredHeight);

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Assuming you have a textbox named "passwordTextBox" on your form
            textBox2.PasswordChar = '*';

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (detectedFaceId == null)
            {
                MessageBox.Show("Please capture your face first.");
                return;
            }
    }

        private void button8_Click(object sender, EventArgs e)
        {
            Image faceImage = CaptureFaceImage();
            if (faceImage == null)
            {
                MessageBox.Show("No image captured.");
                return;
            }
            pictureBox.Image = faceImage;

            MemoryStream faceImageStream = new MemoryStream();
            faceImage.Save(faceImageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            faceImageStream.Seek(0, SeekOrigin.Begin);

            IList<DetectedFace> detectedFaces = await faceClient.Face.DetectWithStreamAsync(faceImageStream);

            if (detectedFaces.Count == 0)
            {
                MessageBox.Show("No faces detected.");
                return;
            }

            DetectedFace detectedFace = detectedFaces[0];
            detectedFaceId = detectedFace.FaceId.Value;

        }
