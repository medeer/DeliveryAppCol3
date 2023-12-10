using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeliveryAppCol3
{
    public partial class ClientForm : Form
    {


       
        public ClientForm()
        {
            InitializeComponent();
            
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sqlQuery = "SELECT name, price FROM Food"; // SQL-запрос для выборки данных о еде
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string foodInfo = $"{reader.GetString(0)} - {reader.GetDecimal(1)} Сом"; // Форматирование информации о еде и цене
                                listBox1.Items.Add(foodInfo); // Добавление информации о еде в ListBox
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (listBox1.SelectedItems.Count > 0)
                    {
                        string address = textBox1.Text;
                        string phoneNumber = textBox2.Text;

                        if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(phoneNumber))
                        {
                            foreach (var selectedItem in listBox1.SelectedItems)
                            {
                                string selectedProduct = selectedItem.ToString();

                                // Добавляем данные в таблицу "Orders" (product_name, address, phone_number)
                                string insertQuery = "INSERT INTO Orders (food_name, customer_address, customer_phone) VALUES (@productName, @address, @phoneNumber)";
                                using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@productName", selectedProduct);
                                    command.Parameters.AddWithValue("@address", address);
                                    command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                                    command.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Заказ успешно оформлен!");
                        }
                        else
                        {
                            MessageBox.Show("Введите адрес и номер телефона для заказа.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите для заказа.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при оформлении заказа: " + ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteForm writeForm = new WriteForm();
            writeForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm();
            reviewForm.Show();
            this.Hide();
        }
    }
}


