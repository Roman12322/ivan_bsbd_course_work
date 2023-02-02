using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ivan
{
    public partial class customer : Form
    {
        private readonly user _user;
        SqlConnection sql_connection = null;
        public customer(user user)
        {
            InitializeComponent();
            _user = user;
        }

        private void customer_Load(object sender, EventArgs e)
        {
            switch (_user.role)
            {
                case "customer":
                    this.customersTableAdapter.FillByCUSTOMER(this.database1DataSet.customers, _user.id);
                    update.Visible = false;
                    break;
                case "employee":
                    this.customersTableAdapter.Fill(this.database1DataSet.customers);
                    break;
                default:
                    break;
            }

        }
        DataTable FillDataGridView(string sqlSelect)
        {
            //Создаем объект command для SQL команды
            SqlCommand command = sql_connection.CreateCommand();
            //Заносим текст SQL запроса через параметр sqlSelect
            command.CommandText = sqlSelect;
            //Создаем объект adapter класса SqlDataAdapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Задаем адаптеру нужную команду, в данном случае команду Select
            adapter.SelectCommand = command;
            //Создаем объект table для последующего отображения результата запроса
            DataTable table = new DataTable();
            //заполним набор данных результатом запроса
            adapter.Fill(table);
            return table;
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Изофатов Иван Александрович, 740-1, 2022", "О программе",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            car car = new car(_user);
            this.Hide();
            car.Show();
        }

        private void diagnosticCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diagnostic_card diagnostic_card = new diagnostic_card(_user);
            this.Hide();
            diagnostic_card.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                string sqlcommand = "UPDATE customers SET phone_number = @phone_number WHERE passport = @passport";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@phone_number", phone_cust_txt.Text);
                command.Parameters.AddWithValue("@passport", cust_passport_txt.Text);

                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView("select * FROM customers");
                MessageBox.Show("Succesfully updated", "Success", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error query.\n" + err.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
