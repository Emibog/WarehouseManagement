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
    public partial class MoveItem : Form
    {
        public string CellToMove { get; set; }
        private DB db;

        public MoveItem(string mapName)
        {
            InitializeComponent();
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
            if (string.IsNullOrEmpty(comboBoxCells.Text)) { }
            else
            {
                CellToMove = comboBoxCells.SelectedItem.ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void MoveItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(comboBoxCells.Text))
            {
                MessageBox.Show("Выберите ячейку для перемещения");
                e.Cancel = true; // Отменить закрытие формы
            }
        }
    }
}
