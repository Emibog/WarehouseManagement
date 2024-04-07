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
    public partial class Items : Form
    {
        public List<string> Products;
        public List<int> Amount;
        public string mapName;
        public string cellName;

        public Items(List<string> products, List<int> amount, string mapName, string cellName)
        {
            InitializeComponent();
            Products = products;
            Amount = amount;
            showProducts();
            this.mapName = mapName;
            this.cellName = cellName;
            Text = "Товары в ячейке " + cellName;
        }

        private void showProducts()
        {
            int yOffset = 20; // Инициализировать смещение Y для позиционирования
            Font labelFont = new Font("Arial", 12, FontStyle.Regular);
            foreach (string product in Products)
            {
                // Создать новую метку для каждого продукта
                Label label = new Label();
                label.Name = product;
                label.Font = labelFont;
                label.Text = product + " - " + Amount[Products.IndexOf(product)];
                label.Width = 150;
                label.Location = new Point(20, yOffset); // Установить позицию метки
                yOffset += 50; // Увеличить смещение Y для следующей метки
                panelItems.Controls.Add(label);

                // Создать новую кнопку для удаления товара
                Button deleteButton = new Button();
                deleteButton.BackgroundImage = global::WarehouseManagement.Properties.Resources.deleteButton;
                deleteButton.BackgroundImageLayout = ImageLayout.Zoom; // Растянуть изображение для заполнения кнопки
                deleteButton.Width = 40;
                deleteButton.Height = 40;
                deleteButton.Tag = label; // Отметить кнопку соответствующей меткой
                deleteButton.Location = new Point(230, yOffset - 60); // Разместить кнопку удаления рядом с меткой
                deleteButton.Click += DeleteButton_Click; // Присоединить обработчик события нажатия
                panelItems.Controls.Add(deleteButton); // Добавить кнопку удаления на форму

                // Создать новую кнопку для перемещения метки
                Button moveButton = new Button();
                moveButton.BackgroundImage = global::WarehouseManagement.Properties.Resources.move;
                moveButton.BackgroundImageLayout = ImageLayout.Zoom; // Растянуть изображение для заполнения кнопки
                moveButton.Width = 40;
                moveButton.Height = 40;
                moveButton.Tag = label; // Отметить кнопку соответствующей меткой
                moveButton.Location = new Point(170, yOffset - 60); // Разместить кнопку перемещения рядом с меткой
                moveButton.Click += MoveButton_Click; // Присоединить обработчик события нажатия
                panelItems.Controls.Add(moveButton); // Добавить кнопку перемещения на форму
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Label correspondingLabel = (Label)deleteButton.Tag;
            int yOffset = correspondingLabel.Location.Y;
            formDeleteItem fDeleteItem = new formDeleteItem(mapName);
            if (fDeleteItem.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fDeleteItem.ItemToDelete);
            }
            // Удалить метку, связанную с кнопкой удаления
            panelItems.Controls.Remove(correspondingLabel);

            // Удалить саму кнопку удаления
            panelItems.Controls.Remove(deleteButton);

            // Сдвинуть нижние элементы вверх, чтобы заполнить пробел
            foreach (Control control in panelItems.Controls)
            {
                if (control.Location.Y > yOffset)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - 50);
                }
            }
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Label correspondingLabel = (Label)deleteButton.Tag;
            
            MoveItem moveItem = new MoveItem(mapName);

            DB db = new DB();
            db.openConnection();
            if (moveItem.ShowDialog() == DialogResult.OK)
            {
                MySqlCommand command = new MySqlCommand("UPDATE `items` SET `cell` = @cell WHERE `item` = @item AND `map` = @map", db.getConnection());
                command.Parameters.AddWithValue("@cell", moveItem.CellToMove);
                command.Parameters.AddWithValue("@item", correspondingLabel.Name);
                command.Parameters.AddWithValue("@map", mapName);
                command.ExecuteNonQuery();
            }
            else
            {
                return;
            }

            updateItems(db);
            db.closeConnection();
        }

        private void updateItems(DB db)
        {
            MySqlCommand updateItemCommand = new MySqlCommand("SELECT `item`, `amount` FROM `items` WHERE cell = @cell AND map = @map", db.getConnection());

            updateItemCommand.Parameters.AddWithValue("@cell", cellName);
            updateItemCommand.Parameters.AddWithValue("@map", mapName);

            Products.Clear();
            Amount.Clear();

            using (MySqlDataReader reader = updateItemCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    string itemValue = reader["item"].ToString();
                    int amountValue = Convert.ToInt32(reader["amount"]);
                    Products.Add(itemValue);
                    Amount.Add(amountValue);
                }
            }

            panelItems.Controls.Clear();
            showProducts();
        }
    }
}