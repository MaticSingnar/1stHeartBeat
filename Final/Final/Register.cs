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

namespace Final
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Create an instance of the DestinationForm
            Form1 destinationForm = new Form1();

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True");
            connection.Open();
            string insertQuery = "insert into Login VALUES ( @username, @password, @gender, @email)";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@username", textBox1.Text.Trim());
            command.Parameters.AddWithValue("@password", textBox2.Text.Trim());
            
            command.Parameters.AddWithValue("@gender", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@email", textBox5.Text.Trim());
            command.ExecuteNonQuery();
            MessageBox.Show("Register Successfully");

            
            {
                // After successful registration, open the login form
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide(); // Hide the current form
            }




        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
