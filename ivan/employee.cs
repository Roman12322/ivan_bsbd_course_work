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
    public partial class employee : Form
    {
        private readonly user _user;
        SqlConnection sql_connection = null;
        public employee(user user)
        {
            InitializeComponent();
            _user = user;
        }

        private void employee_Load(object sender, EventArgs e)
        {
            switch (_user.role)
            {
                case "customer":
                    //this.employeesTableAdapter.FillByCUSTOMER(this.database1DataSet.employees, _user.id);
                    update.Visible = false;
                    break;
                case "employee":
                    this.employeesTableAdapter.FillByEmployee(this.database1DataSet.employees, _user.id);
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

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer customer = new customer(_user);
            this.Hide();
            customer.Show();
        }

        private void diagnosticCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diagnostic_card diagnostic_card = new diagnostic_card(_user);
            this.Hide();
            diagnostic_card.Show();
        }
        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            car car = new car(_user);
            this.Hide();
            car.Show();
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
                string sqlcommand = "UPDATE employees SET phone_number = @phone_number WHERE passport = @passport";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@phone_number", phone_empl_txt.Text);
                command.Parameters.AddWithValue("@passport", empl_passport_txt.Text);

                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView("SELECT id, phone_number, passport, first_name, last_name " +
                    $"FROM dbo.employees\nWHERE id={_user.id}");
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
