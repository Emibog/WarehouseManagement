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
            /*DB db = new DB();
            try
            {
                db.openConnection();

                MySqlCommand command = new MySqlCommand("SELECT `cell` FROM `cells`", db.getConnection());

                // Замените значения параметров на реальные данные
                //command.Parameters.AddWithValue("@cell", cellName);

                // Используем ExecuteReader для выполнения запроса SELECT
                using (MySqlDataReader reader = command.ExecuteReader())
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
            }*/
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            EnteredItemName = textBoxItemName.Text;
            EnteredCategory = textBoxCategory.Text;
            EnteredCell = comboBoxCells.SelectedItem.ToString();
            EnteredAmount = textBoxAmount.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
