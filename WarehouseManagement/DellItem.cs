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
        public string EnteredAmount { get; set; }
        private string mapName;

        public string ItemToDelete { get; set; }
        private DB db;

        public formDeleteItem(string mapName)
        {
            InitializeComponent();
            this.mapName = mapName;
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
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxItems.Text))
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.");
            }
            else
            {
                EnteredCell = comboBoxCells.SelectedItem.ToString();
                EnteredAmount = numericUpDownAmount.Value.ToString();
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
                MySqlCommand command = new MySqlCommand("SELECT `item` FROM `items` WHERE `map` = @map AND `cell` = @cell", db.getConnection());
                command.Parameters.AddWithValue("@map", mapName);
                command.Parameters.AddWithValue("@cell", comboBoxCells.SelectedItem.ToString());

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string itemName = reader["item"].ToString();
                        comboBoxItems.Items.Add(itemName);
                    }
                }

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
    }
}