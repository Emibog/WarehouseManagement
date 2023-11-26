using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagement
{
    public partial class Login : Form
    {
        public string userLogin;
        public string userPass;
        public string UserPost { get; private set; }
        public Login()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            userLogin = textBoxLogin.Text;
            userPass = textBoxPass.Text;

            DB db = new DB();

            if (db.openConnection())
            {
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.getConnection());

                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userLogin;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = userPass;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    UserPost = table.Rows[0]["post"].ToString();
                    // Создаем и открываем MainForm
                    MainForm mainForm = new MainForm(UserPost);
                    mainForm.Show();
                    // Закрываем текущую форму авторизации
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            else
            {
                MessageBox.Show("Не удалось поключиться к БД");
            }            
        }
    }
}
