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
    public partial class formAddCategory : Form
    {
        public string categoryName { get; private set; }
        public formAddCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При закрытии формы проверяем, что поле "Название категории" не пустое
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(textBoxNameCategory.Text))
            {
                MessageBox.Show("Название категории не указано!");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            categoryName = textBoxNameCategory.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
