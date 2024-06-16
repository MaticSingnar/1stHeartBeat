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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final
{
    public partial class DoctorDashboard : Form
    {
        private string username;
        public DoctorDashboard(string username)

        {
            InitializeComponent();
            this.username = username;
           
        }

        private void DoctorDashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet2.Appoinment' table. You can move, or remove it, as needed.
            this.appoinmentTableAdapter.Fill(this.database1DataSet2.Appoinment);
            // TODO: This line of code loads data into the 'database1DataSet.Show_Doctor' table. You can move, or remove it, as needed.
            this.show_DoctorTableAdapter.Fill(this.database1DataSet.Show_Doctor);
            // Display the username in a label on the DoctorDashboard form
            label1.Text = $"Welcome, {username}!";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO SHOW_DOCTOR (Doctor_Name, Qualification, Department, Address) VALUES (@doctor_name, @qualification, @department, @address)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@doctor_name", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@qualification", comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "");
                        command.Parameters.AddWithValue("@department", comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "");
                        command.Parameters.AddWithValue("@address", richTextBox1.Text);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Submitted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox2.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM [dbo].[Show Doctor] WHERE [Doctor Name] LIKE @SearchQuery OR [Address] LIKE @SearchQuery OR [Department] LIKE @SearchQuery OR [Qualification] LIKE @SearchQuery";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            else
            {
                MessageBox.Show("Please enter a search query.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox3.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM [dbo].[Show Doctor] WHERE [Doctor Name] LIKE @SearchQuery OR [Address] LIKE @SearchQuery OR [Department] LIKE @SearchQuery OR [Qualification] LIKE @SearchQuery";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            else
            {
                MessageBox.Show("Please enter a search query.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox4.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM [dbo].[Show Doctor] WHERE [Doctor Name] LIKE @SearchQuery OR [Address] LIKE @SearchQuery OR [Department] LIKE @SearchQuery OR [Qualification] LIKE @SearchQuery";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            else
            {
                MessageBox.Show("Please enter a search query.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create an instance of the DestinationForm
            DProfile destinationForm = new DProfile(username);

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create an instance of the DestinationForm
            Upload destinationForm = new Upload();

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
    }

