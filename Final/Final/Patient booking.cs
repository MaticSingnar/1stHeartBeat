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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final
{
    public partial class Patient_booking : Form
    {
        public Patient_booking()
        {
            InitializeComponent();
        }

        private void Patient_booking_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet4.Appoinment' table. You can move, or remove it, as needed.
            this.appoinmentTableAdapter.Fill(this.database1DataSet4.Appoinment);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Ensure the selected row has the ID column you want
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells["UserId"].Value != null) // Change "UserId" to the actual column name
                {
                    int bookingId = Convert.ToInt32(selectedRow.Cells["UserId"].Value); // Change "UserId" to the actual column name

                    // Confirm cancellation
                    var confirmResult = MessageBox.Show("Are you sure you want to cancel this booking?", "Confirm Cancel", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        CancelBooking(bookingId);
                        // Refresh the DataGridView after deletion
                        this.appoinmentTableAdapter.Fill(this.database1DataSet4.Appoinment);
                    }
                }
                else
                {
                    MessageBox.Show("The selected booking does not have a valid ID.");
                }
            }
            else
            {
                MessageBox.Show("Please select a booking to cancel.");
            }
        }

        private void CancelBooking(int bookingId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    conn.Open();
                    string query = "DELETE FROM Appoinment WHERE Id = @bookingId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@bookingId", bookingId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Booking canceled successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Booking ID not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string username = textBox1.Text.Trim(); // Retrieve the username directly from the TextBox
                Dashboard dashboard = new Dashboard(username);
                this.Hide();
                dashboard.Show();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Debugging step to see the actual input value

                if (!int.TryParse(textBox1.Text, out int PatientName))
                {
                    MessageBox.Show("Booking Cancel.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    string query = "DELETE FROM Appoinment WHERE PatientName = @PatientName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PatientName", PatientName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment canceled successfully.");
                            this.appoinmentTableAdapter.Fill(this.database1DataSet4.Appoinment);
                        }
                        else
                        {
                            MessageBox.Show("User ID not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }






        }
    }
}