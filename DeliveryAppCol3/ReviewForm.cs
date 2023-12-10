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
    public partial class ReviewForm : Form
    {
        public ReviewForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(DisplayReviews);
        }

        private void DisplayReviews(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // Очистка списка перед отображением новых отзывов

            string connectionString = "Host=localhost;Username=postgres;Password=111;Database=csharp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT customer_name, review_text, rating FROM Reviews";

                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string customerName = reader["customer_name"].ToString();
                                string reviewText = reader["review_text"].ToString();
                                int rating = Convert.ToInt32(reader["rating"]);

                                string reviewInfo = $"Customer: {customerName}, Rating: {rating}, Review: {reviewText}";
                                listBox1.Items.Add(reviewInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке отзывов: " + ex.Message);
                }
            }
        }

        // Вызов метода DisplayReviews для отображения отзывов при загрузке формы или другого события
        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            WriteForm writeForm = new WriteForm();
            writeForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Hide();
        }
    }
}
