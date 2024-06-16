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
    public partial class DProfile : Form
    {
        private string connectionString = "@\"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sarki\\source\\repos\\Final\\Final\\Database1.mdf;Integrated Security=True\"";
        private string username;

    
        public DProfile(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void UserProfileForm_Load(object sender, EventArgs e)
        {
            LoadUserProfile();

        }

        private void LoadUserProfile()
        {

            {
                // Retrieve user information from data storage and populate labels
                string username = "Username";
                string password = "password";
                string email = "email";
                

                label5.Text = username;
                label6.Text = password;
                label7.Text = email;
                
            }

            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
            {
                string query = "SELECT * FROM Doctor WHERE Username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox1.Text = reader["Username"].ToString();
                        textBox2.Text = reader["Password"].ToString();
                        textBox3.Text = reader["Email"].ToString();
                        
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Save changes to user profile
            // You would implement code here to update the user profile in the database
            MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
    }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
