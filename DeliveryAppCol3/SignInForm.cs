using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeliveryAppCol3
{
    public partial class SignInForm : Form
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=111;Database=csharp;";
        private string email = "";
        private string password = "";
        private string role = "";
        
        public SignInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredEmail = textBox1.Text;
            string enteredPassword = textBox2.Text;
            string selectedRole = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(enteredEmail) || string.IsNullOrEmpty(enteredPassword) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM person WHERE email = @Email AND password = @Password AND role = @Role";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", enteredEmail);
                        command.Parameters.AddWithValue("@Password", enteredPassword);
                        command.Parameters.AddWithValue("@Role", selectedRole);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                    

                        if (count > 0)
                        {
                            if (selectedRole == "delivery")
                            {
                                this.Hide();
                                DeliveryForm DeliveryForm = new DeliveryForm();
                                DeliveryForm.Show();
                            }
                            else if (selectedRole == "client")
                            {
                                this.Hide();
                                ClientForm ClientForm = new ClientForm();
                                ClientForm.Show();
                            }
                           

                            else
                            {
                                MessageBox.Show("Неизвестная роль или отсутствует обработчик для данной роли.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка. Попробуйте снова!.");

                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

      
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
            textBox2.UseSystemPasswordChar = true;
            textBox2.PasswordChar = '*';
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            role = comboBox1.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            email = textBox1.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Регистрация_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
