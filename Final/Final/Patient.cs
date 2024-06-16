using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final
{
    public partial class Patient : Form
    {

        public Patient()
        {
            InitializeComponent();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
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

        private void Patient_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet1.Show_Doctor' table. You can move, or remove it, as needed.
            this.show_DoctorTableAdapter.Fill(this.database1DataSet1.Show_Doctor);

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an instance of the DestinationForm
            Patient_booking destinationForm = new Patient_booking();

            // Show the DestinationForm
            destinationForm.Show();

            // Optionally, hide the current form
            // this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}







