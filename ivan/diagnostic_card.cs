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
    public partial class diagnostic_card : Form
    {
        private readonly user _user;
        SqlConnection sql_connection = null;
        public diagnostic_card(user user)
        {
            InitializeComponent();
            _user = user;
        }

        private void diagnostic_card_Load(object sender, EventArgs e)
        {
            switch (_user.role)
            {
                case "customer":
                    this.diagnostic_cardTableAdapter.FillByCUSTOMER(this.database1DataSet.diagnostic_card, _user.id);
                    delete.Visible = false;
                    insert.Visible = false;
                    update.Visible = false;
                    break;
                case "employee":
                    this.diagnostic_cardTableAdapter.FillByEMPLOYEE(this.database1DataSet.diagnostic_card, _user.id);
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

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            car car = new car(_user);
            this.Hide();
            car.Show();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee employee = new employee(_user);
            this.Hide();
            employee.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void insert_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                string sqlcommand = "INSERT INTO diagnostic_card (state, id_car, id_employee) VALUES (@state, @id_car, @id_employee)";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@state", state.Text);
                command.Parameters.AddWithValue("@id_car", Convert.ToInt32(idcar_txt.Text));
                command.Parameters.AddWithValue("@id_employee", Convert.ToInt32(idempl_txt.Text));
                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView("SELECT id, state, id_employee, id_car FROM dbo.diagnostic_card\n" +
                    $"WHERE id_employee = {_user.id}");
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
                string sqlcommand = "DELETE FROM diagnostic_card WHERE id_car = @id_car";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@id_car", Convert.ToInt32(idcar_txt.Text));

                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView("SELECT id, state, id_employee, id_car FROM dbo.diagnostic_card\n" +
                    $"WHERE id_employee = {_user.id}");
                MessageBox.Show("Succesfully deleted", "Success", MessageBoxButtons.OK);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error query.\n" + err.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                sql_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database1"].ConnectionString);
                sql_connection.Open();
                string sqlcommand = "UPDATE diagnostic_card SET state = @state WHERE id_car = @id_car";
                SqlCommand command = sql_connection.CreateCommand();
                command.CommandText = sqlcommand;
                command.Parameters.AddWithValue("@state", state.Text);
                command.Parameters.AddWithValue("@id_car", Convert.ToInt32(idcar_txt.Text));

                command.ExecuteNonQuery();
                dataGridView1.DataSource = FillDataGridView("SELECT id, state, id_employee, id_car FROM dbo.diagnostic_card\n" +
                    $"WHERE id_employee = {_user.id}");
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
