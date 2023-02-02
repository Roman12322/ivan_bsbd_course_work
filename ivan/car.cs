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
    public partial class car : Form
    {
        private readonly user _user;
        SqlConnection sql_connection = null;
        public car(user user)
        {
            InitializeComponent();
            _user = user;
        }

        private void car_Load(object sender, EventArgs e)
        {
            switch (_user.role)
            {
                case "customer":
                    this.carTableAdapter.FillByCustomer(this.database1DataSet.car, _user.id);
                    delete.Visible= false;
                    insert.Visible= false;
                    update.Visible= false;
                    break;
                case "employee":
                    this.carTableAdapter.FillByEmployee(this.database1DataSet.car, _user.id);
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
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee employee = new employee(_user);
            this.Hide();
            employee.Show();
        }

        private void diagnosticCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diagnostic_card diagnostic_card = new diagnostic_card(_user);
            this.Hide();
            diagnostic_card.Show();
        }

        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer customer = new customer(_user);
            this.Hide();
            customer.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("(C) ТУСУР, БИС, Изофатов Иван Александрович, 740-1, 2022", "О программе",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sqlcommand = "UPDATE car SET model = @model, mark = @mark  WHERE security_number = @security_number";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@model", car_model_txt.Text);
                command.Parameters.AddWithValue("@mark", car_mark_txt.Text);
                command.Parameters.AddWithValue("@security_number", security_number_txt.Text);

                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView($"SELECT * from car");
                MessageBox.Show("Succesfully updated", "Success", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error query.\n" + err.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                string sqlcommand = "INSERT INTO car (model, mark, security_number, id_customer) VALUES (@model, @mark, @security_number, @id_customer)";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@model", car_model_txt.Text);
                command.Parameters.AddWithValue("@mark", car_mark_txt.Text);
                command.Parameters.AddWithValue("@security_number", security_number_txt.Text);
                command.Parameters.AddWithValue("@id_customer", Convert.ToInt32(id_cust.Text));
                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView($"SELECT * from car");
                MessageBox.Show("Succesfully inserted", "Success", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error query.\n" + err.Message,
                "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                string sqlcommand = "DELETE FROM car WHERE security_number = @security_number";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@security_number", security_number_txt.Text);
                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView($"SELECT * from car");
                MessageBox.Show("Succesfully deleted", "Success", MessageBoxButtons.OK);
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
