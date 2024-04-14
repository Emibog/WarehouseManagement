using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagement
{
    public partial class Login : Form
    {
        public string userLogin;
        public string userPass;
        public string userPost { get; private set; }
        public string userName { get; private set; }
        private string mampPath = @"..\..\MAMP\MAMP";

        public Login()
        {
            InitializeComponent();

            // Запустить MAMP, если он не запущен
            if (!IsMampRunning())
            {
                try
                {
                    //Установка параметров запуска
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        //Путь до программы
                        FileName = mampPath,
                        //Установка параметра для запуска в свернутом режиме
                        WindowStyle = ProcessWindowStyle.Minimized
                    };

                    Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при запуске MAMP: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Проверка на наличие запущенного MAMP
        /// </summary>
        /// <returns></returns>
        private bool IsMampRunning()
        {
            Process[] processes = Process.GetProcessesByName("MAMP");
            return processes.Length > 0;
        }

        /// <summary>
        /// Вход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            userLogin = textBoxLogin.Text;
            userPass = HashPassword(textBoxPass.Text);

            DB db = new DB();

            // Проверка подключения к БД
            if (db.openConnection())
            {
                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND BINARY `pass` = @uP", db.getConnection());

                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userLogin;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = userPass;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    userPost = table.Rows[0]["post"].ToString();
                    userName = table.Rows[0]["login"].ToString();
                    // Создаем и открываем MainForm
                    MainForm mainForm = new MainForm(userPost, userName);
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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
