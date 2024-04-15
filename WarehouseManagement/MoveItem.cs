using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagement
{
    public partial class MoveItem : Form
    {
        public string CellToMove { get; set; }
        public decimal EnteredAmount { get; set; }
        private string mapName;
        private string item;
        private string cell;
        private int existingAmount;
        private DB db;

        public MoveItem(string mapName, string item, string cell, int existingAmount)
        {
            InitializeComponent();
            this.mapName = mapName;
            this.item = item;
            this.cell = cell;
            this.existingAmount = existingAmount;
            Text = "Перемещение товара (" + item + ") из ячейки " + cell;

            db = new DB();
            numericUpDownAmount.Maximum = decimal.MaxValue;
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT `cell` FROM `cells` WHERE `map` = @map", db.getConnection());
            command.Parameters.AddWithValue("@map", mapName);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string itemName = reader["cell"].ToString();
                    if (itemName != cell)
                    {
                        comboBoxCells.Items.Add(itemName);
                    }
                }
            }
        }

        private void setMaxAmount_Click(object sender, EventArgs e)
        {
            db = new DB();
            db.openConnection();

            int startIndex = item.IndexOf('(') + 1;
            int endIndex = item.LastIndexOf(')');
            string category = item.Substring(startIndex, endIndex - startIndex);
            string itemName = item.Substring(0, startIndex - 2);
            MySqlCommand command = new MySqlCommand("SELECT `amount` FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
            command.Parameters.AddWithValue("@item", itemName);
            command.Parameters.AddWithValue("@cell", cell);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", category);

            int existingAmount = Convert.ToInt32(command.ExecuteScalar());

            numericUpDownAmount.Value = existingAmount;
            db.closeConnection();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBoxCells.Text)) { }
            else
            {
                CellToMove = comboBoxCells.SelectedItem.ToString();
                EnteredAmount = numericUpDownAmount.Value;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MoveItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(comboBoxCells.Text) | numericUpDownAmount.Value <= 0)
            {
                MessageBox.Show("Выберите ячейку и количество товара для перемещения");
                e.Cancel = true; // Отменить закрытие формы
            }
            else if (DialogResult == DialogResult.OK && existingAmount < numericUpDownAmount.Value)
            {
                MessageBox.Show("Указанное колчество больше имеющегося.");
                e.Cancel= true;
            }
        }
    }
}
