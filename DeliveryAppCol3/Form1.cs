using Npgsql;
using System;
using System.Windows.Forms;

namespace DeliveryAppCol3
{
    public partial class Form1 : Form
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=csharp;";

        private string username = "";
        private string email = "";
        private string password = "";
        private string confirmPassword = "";
        private string role = "client";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            string query = "INSERT INTO person (name, email, password,role) VALUES (@Name, @Email, @Password,@Role)";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Пользователь зарегистрирован.");
                            // Clear text boxes after successful registration
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка регистрации.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            confirmPassword = textBox4.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            password = textBox3.Text;
            textBox3.UseSystemPasswordChar = true;
            textBox3.PasswordChar = '*';
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            email = textBox2.Text;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            username = textBox1.Text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Hide();
        }
    }
}
