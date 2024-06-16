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
    public partial class Profile : Form


    {
        private int userId;

        // Constructor with userId parameter

      

        public Profile(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserProfile(userId);
        }

        private void LoadUserProfile(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string selectQuery = "SELECT Name, Email, Password FROM Users WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string email = reader["Email"].ToString();
                            string password = reader["Password"].ToString();

                            // Display the retrieved information in your form controls
                            textBox1.Text = name;
                            textBox2.Text = email;
                            textBox3.Text = password;
                        }
                        else
                        {
                            MessageBox.Show("User not found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string newName = textBox1.Text;
            string newEmail = textBox2.Text;
            string newPassword = textBox3.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sarki\source\repos\Final\Final\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    string updateQuery = "UPDATE Users SET Name = @Name, Email = @Email, Password = @Password WHERE UserId = @UserId";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@Email", newEmail);
                        command.Parameters.AddWithValue("@Password", newPassword);
                        command.Parameters.AddWithValue("@UserId", userId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Optionally, provide functionality to cancel changes or close the form
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label1 click event if needed
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Code to execute when the text in textBox1 changes
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            // Optionally perform additional actions when the form is loaded
        }
    }
}
