using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WarehouseManagement
{
    public partial class formAddUser : Form
    {
        internal object TextBoxNewLogin;
        internal object TextBoxNewPass;

        public string newLogin { get; private set; }
        public string newPass { get; private set; }

        public formAddUser()
        {
            InitializeComponent();
        }

        private void AddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(textBoxNewLogin.Text) | string.IsNullOrEmpty(textBoxNewPass.Text))
            {
                MessageBox.Show("Не все данные указаны!");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            newLogin = textBoxNewLogin.Text;
            newPass = HashPassword(textBoxNewPass.Text);
        }

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

        // TODO: Добавить выбор должности пользователя

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
