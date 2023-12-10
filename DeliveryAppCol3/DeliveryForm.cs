using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DeliveryAppCol3
{
    public partial class DeliveryForm : Form
    {
        public DeliveryForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(DeliveryForm_Load);
            this.Load += new EventHandler(DisplayProcessData);


        }
        private void DeliveryForm_Load(object sender, EventArgs e)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT order_id, food_name, customer_address, customer_phone FROM orders";

                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string orderId = reader["order_id"].ToString();
                                string foodName = reader["food_name"].ToString();
                                string customerAddress = reader["customer_address"].ToString();
                                string customerPhone = reader["customer_phone"].ToString();

                                string orderInfo = $"Order ID: {orderId}, Food: {foodName}, Address: {customerAddress}, Phone: {customerPhone}";
                                listBox1.Items.Add(orderInfo);
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
            if (listBox1.SelectedItems.Count > 0)
            {
                string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Перебираем выбранные элементы в ListBox
                        foreach (var selectedItem in listBox1.SelectedItems)
                        {
                            string selectedOrderInfo = selectedItem.ToString();

                            // Выполняем SQL-запрос для вставки выбранных данных в таблицу 'process'
                            string insertQuery = "INSERT INTO process (food_name, customer_address, customer_phone) VALUES ( @foodName, @customerAddress, @customerPhone)";


                            // Парсим информацию о заказе для получения необходимых данных
                            string[] orderDetails = selectedOrderInfo.Split(new string[] { ", " }, StringSplitOptions.None);
                            
                            string foodName = orderDetails[1].Split(':')[1].Trim();
                            string customerAddress = orderDetails[2].Split(':')[1].Trim();
                            string customerPhone = orderDetails[3].Split(':')[1].Trim();

                            // Создаем команду для выполнения запроса
                            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                            {
                                
                                command.Parameters.AddWithValue("@foodName", foodName);
                                command.Parameters.AddWithValue("@customerAddress", customerAddress);
                                command.Parameters.AddWithValue("@customerPhone", customerPhone);
                                command.ExecuteNonQuery();
                            }
                        }

                        for (int i = listBox1.SelectedIndices.Count - 1; i >= 0; i--)
                        {
                            listBox1.Items.RemoveAt(listBox1.SelectedIndices[i]);
                        }

                        DisplayProcessData(sender, e);

                        MessageBox.Show("Заказ в процессе");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ.");
            }
        }

        private void DisplayProcessData(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT order_id, food_name, customer_address, customer_phone FROM process";

                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string orderId = reader["order_id"].ToString();
                                string foodName = reader["food_name"].ToString();
                                string customerAddress = reader["customer_address"].ToString();
                                string customerPhone = reader["customer_phone"].ToString();

                                string orderInfo = $"Order ID: {orderId}, Food: {foodName}, Address: {customerAddress}, Phone: {customerPhone}";
                                listBox2.Items.Add(orderInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }


        private void DisplayCompletedOrders(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string selectQuery = "SELECT order_id, food_name, customer_address, customer_phone FROM completed_order";

                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string orderId = reader["order_id"].ToString();
                                string foodName = reader["food_name"].ToString();
                                string customerAddress = reader["customer_address"].ToString();
                                string customerPhone = reader["customer_phone"].ToString();

                                string orderInfo = $"Номер заказа: {orderId}, Позиция: {foodName}, Адрес: {customerAddress}, Номер: {customerPhone}";
                                listBox3.Items.Add(orderInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {

            {
             
            }


            if (listBox2.SelectedItems.Count > 0)
            {
                string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Перебираем выбранные элементы в ListBox
                        foreach (var selectedItem in listBox2.SelectedItems)
                        {
                            string selectedOrderInfo = selectedItem.ToString();

                            string insertQuery = "INSERT INTO completed_order (food_name, customer_address, customer_phone) VALUES ( @foodName, @customerAddress, @customerPhone)";


                            // Парсим информацию о заказе для получения необходимых данных
                            string[] orderDetails = selectedOrderInfo.Split(new string[] { ", " }, StringSplitOptions.None);

                            string foodName = orderDetails[1].Split(':')[1].Trim();
                            string customerAddress = orderDetails[2].Split(':')[1].Trim();
                            string customerPhone = orderDetails[3].Split(':')[1].Trim();

                            // Создаем команду для выполнения запроса
                            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                            {

                                command.Parameters.AddWithValue("@foodName", foodName);
                                command.Parameters.AddWithValue("@customerAddress", customerAddress);
                                command.Parameters.AddWithValue("@customerPhone", customerPhone);
                                command.ExecuteNonQuery();
                            }
                        }

                        for (int i = listBox2.SelectedIndices.Count - 1; i >= 0; i--)
                        {
                            listBox2.Items.RemoveAt(listBox2.SelectedIndices[i]);
                        }
                        DisplayCompletedOrders(sender, e);

                        MessageBox.Show("Заказ выполнен!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка" + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteForm writeForm = new WriteForm();
            writeForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm();
            reviewForm.Show();
            this.Hide();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
