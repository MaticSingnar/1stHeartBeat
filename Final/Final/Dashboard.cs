using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using ZXing;
using System.Drawing.Printing;


namespace Final
{


    public partial class Dashboard : Form
    {

        private string username;
        public Dashboard(string username)


        {
            InitializeComponent();
            this.username = username;

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Display the username in a label on the DoctorDashboard form
            label9.Text = $"Welcome, {username}";
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }






        private void InitializeComponents()
        {


            
            {
                // Other initialization code...

                // Button to trigger QR code generation
                Button button4 = new Button();
                button4.Text = "Generate QR Code";
                button4.Click += button4_Click;
                button4.Location = new Point(20, 100); // Adjust location as needed
                Controls.Add(button4);

                // Other initialization code...
            }



            // Other initialization code...

            // Button to trigger QR code scanning
            Button button5 = new Button();
            button5.Text = "Scan QR Code";
            button5.Click += button5_Click;
            button5.Location = new Point(20, 150); // Adjust location as needed
            Controls.Add(button5);

            // Other initialization code...


            Button button6 = new Button();
            button6.Text = "Download User Details";
            button6.Click += button6_Click;
            button6.Location = new Point(20, 150); // Adjust location as needed
            Controls.Add(button6);


            


        }


        public Bitmap GenerateQRCode(string text)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(6); // Adjust size as needed
            return qrCodeImage;
        }



        public class UserDetails
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Password { get; set; }

            // Add more properties as needed
        }







        public string ScanQRCode(Bitmap qrCodeImage)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(qrCodeImage);
            return result?.Text; // Returns the QR code content as text
        }






        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Appoinment (PatientName, Disease, Symptoms, Age, Gender, Contact, Address) VALUES (@PatientName, @Disease, @Symptoms, @Age, @Gender, @Contact, @Address)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@Disease", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@Symptoms", textBox3.Text.Trim());
                        command.Parameters.AddWithValue("@Age", textBox5.Text.Trim());
                        command.Parameters.AddWithValue("@Gender", comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "");
                        command.Parameters.AddWithValue("@Contact", textBox4.Text.Trim());
                        command.Parameters.AddWithValue("@Address", richTextBox1.Text.Trim());

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Submitted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Get selected date and time
                        DateTime appointmentDateTime = dateTimePicker1.Value;

                        // Example: Store the appointment (here, just displaying it)
                        string message = $"Appointment booked for {appointmentDateTime.ToString("dd/MM/yyyy HH:mm")}";
                        MessageBox.Show(message, "Appointment Booked", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Optionally, you can store the appointment in a database or another storage medium
                        // Example: Insert into database using SQL queries or Entity Framework
                        // Example: AppointmentRepository.Save(new Appointment { DateTime = appointmentDateTime, ... });


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create an instance of the DestinationForm
            Patient destinationForm = new Patient();

            // Show the DestinationForm
            destinationForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create an instance of the DestinationForm
            Patient_booking destinationForm = new Patient_booking();

            // Show the DestinationForm
            destinationForm.Show();


        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of the DestinationForm
            UserP destinationForm = new UserP();

            // Show the DestinationForm
            destinationForm.Show();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch data from the SQL database
                string patientInfo = FetchLoginInfoFromSQL();

                if (!string.IsNullOrEmpty(patientInfo))
                {
                    // Generate QR code image
                    Bitmap qrCodeImage = GenerateQRCode(patientInfo);

                    // Display the QR code image in a PictureBox
                    pictureBox2.Image = qrCodeImage; // Assuming pictureBox2 exists on your form
                }
                else
                {
                    MessageBox.Show("No data found to generate QR code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FetchLoginInfoFromSQL()
        {
            string data = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Login"; // Adjust query as needed

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Concatenate data from multiple columns if needed
                                data = $"{reader["username"].ToString()} {reader["password"].ToString()}"; // Adjust column names
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return data;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }











        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch data from the SQL database for the current user
                UserDetails userDetails = FetchUserDetailsFromSQL(username);

                if (userDetails != null)
                {
                    // Save user details to a text file
                    SaveUserDetailsToFile(userDetails);
                    MessageBox.Show("User details downloaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No data found to download.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FetchLoginInfoFromSQL(string username)
        {
            string data = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Login WHERE username = @username"; // Adjust query as needed

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Concatenate data from multiple columns if needed
                                data = $"{reader["username"].ToString()} {reader["password"].ToString()}"; // Adjust column names
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return data;
        }

        private UserDetails FetchUserDetailsFromSQL(string username)
        {
            UserDetails userDetails = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Login WHERE username = @username"; // Adjust query as needed

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userDetails = new UserDetails
                                {
                                    UserName = reader["username"].ToString(),
                                    Email = reader["email"].ToString(), // Adjust column names
                                    Gender = reader["gender"].ToString(),
                                    Password = reader["password"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return userDetails;
        }

        private void SaveUserDetailsToFile(UserDetails userDetails)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save User Details";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine($"UserName: {userDetails.UserName}");
                        writer.WriteLine($"Email: {userDetails.Email}");
                        writer.WriteLine($"Gender: {userDetails.Gender}");
                        writer.WriteLine($"Password: {userDetails.Password}");
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                UserDetails userDetails = FetchUserDetailsFromSQL(username);

                if (userDetails != null)
                {
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += (s, ev) => PrintUserDetails(ev, userDetails);

                    PrintDialog printDialog = new PrintDialog
                    {
                        Document = printDocument
                    };

                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDocument.Print();
                    }
                }
                else
                {
                    MessageBox.Show("No data found to print.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintUserDetails(PrintPageEventArgs e, UserDetails userDetails)
        {
            Font font = new Font("Arial", 12);
            float lineHeight = font.GetHeight(e.Graphics);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            e.Graphics.DrawString($"UserName: {userDetails.UserName}", font, Brushes.Black, x, y);
            y += lineHeight;
            e.Graphics.DrawString($"Email: {userDetails.Email}", font, Brushes.Black, x, y);
            y += lineHeight;
            e.Graphics.DrawString($"Gender: {userDetails.Gender}", font, Brushes.Black, x, y);
            y += lineHeight;
            e.Graphics.DrawString($"Password: {userDetails.Password}", font, Brushes.Black, x, y);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Create an instance of the DestinationForm
            Voice destinationForm = new Voice();

            // Show the DestinationForm
            destinationForm.Show();
        }
    }
    }

