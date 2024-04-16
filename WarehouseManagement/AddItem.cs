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
        private string mapName;
        private formAddItem form;

        public formAddItem(string mapName, string parentCell = "")
        {
            InitializeComponent();
            this.mapName = mapName;
            form = this;

            DB db = new DB();
            try
            {
                db.openConnection();

                MySqlCommand commandGetCell = new MySqlCommand("SELECT `cell` FROM `cells` WHERE `map` = @map", db.getConnection());
                commandGetCell.Parameters.AddWithValue("@map", mapName);

                // Используем ExecuteReader для выполнения запроса SELECT
                using (MySqlDataReader reader = commandGetCell.ExecuteReader())
                {
                    // Перебираем результаты запроса
                    while (reader.Read())
                    {
                        // Получаем значение из столбца "item"
                        string cell = reader["cell"].ToString();

                        // Добавляем значение в коллекцию Products
                        comboBoxCells.Items.Add(cell);
                    }
                }

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
                if (!string.IsNullOrEmpty(parentCell))
                {
                    comboBoxCells.SelectedItem = parentCell;
                    comboBoxCells.Enabled = false;
                }
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

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            formAddCategory fAddCategory = new formAddCategory();

            if (fAddCategory.ShowDialog() == DialogResult.OK)
            {
                DB db = new DB();
                try
                {
                    db.openConnection();
                    dbAddCategory(db, fAddCategory);

                    MessageBox.Show("Категория успешно добавлена.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении категории в базу данных.\n\n" + ex.Message, "Ошибка добавления категории", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.closeConnection();

                }
            }
            fAddCategory.Close();
        }

        private void dbAddCategory(DB db, formAddCategory fAddCategory)
        {
            MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `categories` AUTO_INCREMENT = 1", db.getConnection());
            MySqlCommand command = new MySqlCommand("INSERT INTO `categories` (`category`) VALUES (@category)", db.getConnection());

            command.Parameters.AddWithValue("@category", fAddCategory.categoryName);

            autoIncrement.ExecuteNonQuery();
            command.ExecuteNonQuery();
            
            MySqlCommand updateCategories = new MySqlCommand("SELECT `category` FROM `categories`", db.getConnection());

            // Обновление категорий
            // Используем ExecuteReader для выполнения запроса SELECT
            using (MySqlDataReader reader = updateCategories.ExecuteReader())
            {
                // Очистка категорий
                comboBoxCategories.Items.Clear();

                // Перебираем результаты запроса
                while (reader.Read())
                {
                    // Получаем значение из столбца "item"
                    string category = reader["category"].ToString();

                    // Добавляем значение в коллекцию Products
                    comboBoxCategories.Items.Add(category);
                }
            }
        }
    }
}
