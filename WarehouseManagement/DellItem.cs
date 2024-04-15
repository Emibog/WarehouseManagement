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
    public partial class formDeleteItem : Form
    {
        public string EnteredCell { get; set; }
        public decimal EnteredAmount { get; set; }
        private string mapName;

        public string ItemToDelete { get; set; }
        string parentCell;
        string parentItem;
        private DB db;

        public formDeleteItem(string mapName, string parentCell = "", string parentItem = "")
        {
            InitializeComponent();
            this.mapName = mapName;
            this.parentCell = parentCell;
            this.parentItem = parentItem;
            numericUpDownAmount.Maximum = decimal.MaxValue;

            db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT `cell` FROM `cells` WHERE `map` = @map", db.getConnection());
            command.Parameters.AddWithValue("@map", mapName);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string itemName = reader["cell"].ToString();
                    comboBoxCells.Items.Add(itemName);
                }
            }

            comboBoxCells.SelectedItem = parentCell;

        }

        private void formDellItem_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(comboBoxItems.Text) | string.IsNullOrEmpty(comboBoxCells.Text) | numericUpDownAmount.Value < 0)
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления и укажите количество.");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxItems.Text) | string.IsNullOrEmpty(comboBoxCells.Text)){ }
            else
            {
                EnteredCell = comboBoxCells.SelectedItem.ToString();
                EnteredAmount = numericUpDownAmount.Value;
                ItemToDelete = comboBoxItems.SelectedItem.ToString();
                //DeleteItem();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxCells_SelectedIndexChanged(object sender, EventArgs e)
        {
            db = new DB();
            comboBoxItems.Items.Clear();

            try
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT `item`, `category` FROM `items` WHERE `map` = @map AND `cell` = @cell", db.getConnection());
                command.Parameters.AddWithValue("@map", mapName);
                command.Parameters.AddWithValue("@cell", comboBoxCells.SelectedItem.ToString());

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string categoryValue = reader["category"].ToString();
                        string itemName = reader["item"].ToString() ;
                        comboBoxItems.Items.Add(itemName + " (" + categoryValue + ") ");
                    }
                }

                comboBoxItems.SelectedItem = parentItem;

                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке товаров.\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void setMaxAmount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxItems.Text) && !string.IsNullOrEmpty(comboBoxCells.Text))
            {
                db = new DB();
                db.openConnection();

                int startIndex = comboBoxItems.SelectedItem.ToString().IndexOf('(') + 1;
                int endIndex = comboBoxItems.SelectedItem.ToString().LastIndexOf(')');
                string category = comboBoxItems.SelectedItem.ToString().Substring(startIndex, endIndex - startIndex);
                string itemName = comboBoxItems.SelectedItem.ToString().Substring(0, startIndex - 2);
                MySqlCommand command = new MySqlCommand("SELECT `amount` FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
                command.Parameters.AddWithValue("@item", itemName);
                command.Parameters.AddWithValue("@cell", comboBoxCells.SelectedItem.ToString());
                command.Parameters.AddWithValue("@map", mapName);
                command.Parameters.AddWithValue("@category", category);

                int existingAmount = Convert.ToInt32(command.ExecuteScalar());
                
                numericUpDownAmount.Value = existingAmount;
                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Выберите товар");
            }
        }
    }
}