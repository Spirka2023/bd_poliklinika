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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bd_poliklinika
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nameClient = textBox1.Text;
            string nameDoctor = textBox2.Text;
            string diagnosis = textBox3.Text;
            string therapy = textBox4.Text;

            if (!string.IsNullOrEmpty(nameClient) && !string.IsNullOrEmpty(nameDoctor) && !string.IsNullOrEmpty(diagnosis) && !string.IsNullOrEmpty(therapy))
            {
                DataBase dataBase = new DataBase();
                dataBase.openConnection();

                string query = "INSERT INTO reception (nameClient, nameDoctor, diagnosis, therapy) VALUES (@nameClient, @nameDoctor, @diagnosis, @therapy)";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@nameClient", nameClient);
                command.Parameters.AddWithValue("@nameDoctor", nameDoctor);
                command.Parameters.AddWithValue("@diagnosis", diagnosis);
                command.Parameters.AddWithValue("@therapy", therapy);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно добавлена!");
                    form1.LoadData(); // Обновление данных на Form1
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении записи: " + ex.Message);
                }

                dataBase.closeConnection();
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(textBox5.Text, out id) && id > 0)
            {
                DataBase dataBase = new DataBase();
                dataBase.openConnection();

                string query = "DELETE FROM reception WHERE reception_id = @Id";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена!");
                    form1.LoadData(); // Обновление данных на Form1
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении записи: " + ex.Message);
                }

                dataBase.closeConnection();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректный ID для удаления.");
            }
        }
    }
}