using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ivan
{
    public partial class auth : Form
    {
        SqlConnection sql_connection = null;
        public auth()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void auth_button_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                var user_phone = phone_auth_txt.Text;
                var user_passport = passport_auth_txt.Text;
                var user_status = status_auth.Text;
                if(user_status == "" || user_status == null)
                {
                    MessageBox.Show("Выбирите роль входа");
                }
                else if (user_status == "employee")
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    string query = $"select id from employees where phone_number = '{user_phone}' and passport = '{user_passport}'";
                    SqlCommand command = new SqlCommand(query, sql_connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        var user = new user(id: Convert.ToInt32(dt.Rows[0].ItemArray[0]), role: "employee");
                        MessageBox.Show($"Вход произведен успешно от роли {user.role}");
                        employee employee = new employee(user);
                        this.Hide();
                        employee.Show();
                    }
                    else
                    {
                        MessageBox.Show("Некорректный вход, неправильные данные");
                    }
                }
                else if(user_status == "customer")
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    string query = $"select id from customers where phone_number = '{user_phone}' and passport = '{user_passport}'";
                    SqlCommand command = new SqlCommand(query, sql_connection);
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        var user = new user(id: Convert.ToInt32(dt.Rows[0].ItemArray[0]), role: "customer");
                        MessageBox.Show($"Вход произведен успешно от роли {user.role}");
                        customer customer = new customer(user);
                        this.Hide();
                        customer.Show();
                    }
                    else
                    {
                        MessageBox.Show("Некорректный вход, неправильные данные");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Некорректный вход, неправильные данные");
            }
        }
    }
}
