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

namespace DeliveryAppCol3
{
    public partial class WriteForm : Form
    {
        public WriteForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string customerName = textBox1.Text;
            string reviewText = textBox3.Text;
            int rating = Convert.ToInt32(textBox2.Text); // Предполагается, что в поле rating вводится числовое значение

            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Reviews (customer_name, review_text, rating) VALUES (@customerName, @reviewText, @rating)";

                    using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@customerName", customerName);
                        command.Parameters.AddWithValue("@reviewText", reviewText);
                        command.Parameters.AddWithValue("@rating", rating);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Отзыв отправлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении отзыва: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReviewForm reviewForm = new ReviewForm();
            reviewForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Hide();
        }
    }
}
