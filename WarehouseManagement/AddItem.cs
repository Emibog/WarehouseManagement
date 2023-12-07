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
    public partial class formAddItem : Form
    {
        public string EnteredItemName { get; set; }
        public string EnteredCategory { get; set; }
        public string EnteredCell { get; set; }
        public string EnteredAmount { get; set; }

        public formAddItem(List<StorageCell> storageCell)
        {
            InitializeComponent();
            foreach (StorageCell control in storageCell)
            {
                comboBoxCells.Items.Add(control.Name);
            }
            DB db = new DB();
            try
            {
                db.openConnection();

                MySqlCommand command = new MySqlCommand("SELECT `category` FROM `categories`", db.getConnection());

                // Используем ExecuteReader для выполнения запроса SELECT
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    // Перебираем результаты запроса
                    while (reader.Read())
                    {
                        // Получаем значение из столбца "item"
                        string category = reader["category"].ToString();

                        // Добавляем значение в коллекцию Products
                        comboBoxCategories.Items.Add(category);
                    }
                }

                // Закрываем соединение
                db.closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отображении ячеек.\n\n" + ex.Message, "Ошибка отображения ячеек", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void formAddItem_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(textBoxItemName.Text) | string.IsNullOrEmpty(comboBoxCells.Text) 
                | string.IsNullOrEmpty(comboBoxCategories.Text) | numericUpDownAmount.Value <= 0)
            {
                MessageBox.Show("Не все данные указаны корректно!");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxCells.Text) | string.IsNullOrEmpty(comboBoxCategories.Text)){  }
            else
            {
                EnteredItemName = textBoxItemName.Text;
                EnteredCell = comboBoxCells.SelectedItem.ToString();
                EnteredCategory = comboBoxCategories.SelectedItem.ToString();
                EnteredAmount = numericUpDownAmount.Value.ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
