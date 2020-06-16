using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace lab6
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        

        public Form1()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                string select = "SELECT * FROM Blanks";
                SqlDataAdapter adapter = new SqlDataAdapter(select,connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }

            catch (SqlException ex)
            {
                richTextBox1.Text=ex.Message;
            }

            finally
            {            
                connection.Close();
            }

        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text)
               )
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Students (Name, Surname, Patronymic, Telephone) VALUES (@n, @s, @p, @t)", connection);
                command.Parameters.AddWithValue("n", textBox1.Text);
                command.Parameters.AddWithValue("s", textBox2.Text);
                command.Parameters.AddWithValue("p", textBox3.Text);
                command.Parameters.AddWithValue("t", textBox4.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label7.Text = "Fill in all fields please!";
                label7.Visible = true;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
                label8.Visible = false;

            if(!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text)
                && !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("UPDATE Students SET Telephone=@t WHERE Id=@i", connection);
                command.Parameters.AddWithValue("t", textBox8.Text);
                command.Parameters.AddWithValue("i", textBox5.Text);
               
                await command.ExecuteNonQueryAsync();
            }
            
            else
            {
                label8.Visible = true;
                label8.Text = "Fill in all fields please!";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label10.Visible)
                label10.Visible = false;

            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Id=@i", connection);
                command.Parameters.AddWithValue("i", textBox6.Text);
                
                await command.ExecuteNonQueryAsync();
            }

            else
            {
                label10.Visible = true;
                label10.Text = "Fill in all fields please!";
            }
        }


    }
}
