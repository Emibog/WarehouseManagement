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
        private int existingAmount;
        public string mapName;
        public string cellName;
        string parentItem;
        public string userName;
        public string category;
        string itemName;

        public Items(string mapName, string cellName, string userName)
        {
            InitializeComponent();
            this.mapName = mapName;
            this.cellName = cellName;
            this.userName = userName;
            Text = "Товары в ячейке " + cellName;
            showProducts();
        }

        private void showProducts()
        {
            int yOffset = 20; // Инициализировать смещение Y для позиционирования
            Font labelFont = new Font("Arial", 12, FontStyle.Regular);
            
            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `item`, `category`, `amount` FROM `items` WHERE cell = @cell AND map = @map", db.getConnection());

            command.Parameters.AddWithValue("@cell", cellName);
            command.Parameters.AddWithValue("@map", mapName);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string categoryValue = reader["category"].ToString();
                    string itemValue = reader["item"].ToString() + " (" + categoryValue + ") ";
                    int amountValue = Convert.ToInt32(reader["amount"]);

                    // Создать новую метку для каждого продукта
                    Label label = new Label();
                    label.Name = itemValue;
                    label.Font = labelFont;
                    label.Text = itemValue + amountValue;
                    label.Width = 200;
                    label.Location = new Point(20, yOffset); // Установить позицию метки
                    yOffset += 50; // Увеличить смещение Y для следующей метки
                    panelItems.Controls.Add(label);

                    // Создать новую кнопку для удаления товара
                    Button deleteButton = new Button();
                    deleteButton.BackgroundImage = Properties.Resources.deleteButton;
                    deleteButton.BackgroundImageLayout = ImageLayout.Zoom; // Растянуть изображение для заполнения кнопки
                    deleteButton.Width = 40;
                    deleteButton.Height = 40;
                    deleteButton.Name = itemValue;
                    deleteButton.Tag = label; // Отметить кнопку соответствующей меткой
                    deleteButton.Location = new Point(300, yOffset - 60); // Разместить кнопку удаления рядом с меткой
                    deleteButton.Click += DeleteButton_Click; // Присоединить обработчик события нажатия
                    panelItems.Controls.Add(deleteButton); // Добавить кнопку удаления на форму

                    // Создать новую кнопку для перемещения метки
                    Button moveButton = new Button();
                    moveButton.BackgroundImage = Properties.Resources.move;
                    moveButton.BackgroundImageLayout = ImageLayout.Zoom; // Растянуть изображение для заполнения кнопки
                    moveButton.Width = 40;
                    moveButton.Height = 40;
                    moveButton.Name = itemValue;
                    moveButton.Tag = label; // Отметить кнопку соответствующей меткой
                    moveButton.Location = new Point(240, yOffset - 60); // Разместить кнопку перемещения рядом с меткой
                    moveButton.Click += MoveButton_Click; // Присоединить обработчик события нажатия
                    panelItems.Controls.Add(moveButton); // Добавить кнопку перемещения на форму
                }
            }
            db.closeConnection();
        }

        /// <summary>
        /// Добавление операции в историю
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <param name="user"></param>
        private void AddOperationToHistory(string table, string item, string amount, string user)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand autoIncrementHistory = new MySqlCommand("ALTER TABLE `" + table + "` AUTO_INCREMENT = 1", db.getConnection());
            autoIncrementHistory.ExecuteNonQuery();
            MySqlCommand historyCommand = new MySqlCommand("INSERT INTO `" + table + "` (`item`, `amount`, `user`, `map`, `date`) VALUES (@item, @amount, @user, @map, NOW())", db.getConnection());
            historyCommand.Parameters.AddWithValue("@item", item);
            historyCommand.Parameters.AddWithValue("@amount", amount);
            historyCommand.Parameters.AddWithValue("@user", user);
            historyCommand.Parameters.AddWithValue("@map", mapName);
            historyCommand.ExecuteNonQuery();
            db.closeConnection();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                parentItem = control.Name;
                string fullName = parentItem;
                int startIndex = fullName.IndexOf('(') + 1;
                int endIndex = fullName.LastIndexOf(')');
                category = fullName.Substring(startIndex, endIndex - startIndex);  
            }

            formDeleteItem fDeleteItem = new formDeleteItem(mapName, cellName, parentItem);

            DB db = new DB();
            db.openConnection();

            if (fDeleteItem.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DeleteItem(db, fDeleteItem, mapName, category);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при работе с базой данных.\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                fDeleteItem.Close();
            }

            updateItems(db);
            db.closeConnection();
            fDeleteItem.Close();
        }

        private void MoveButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Label correspondingLabel = (Label)deleteButton.Tag;

            if (sender is Control control)
            {
                parentItem = control.Name;
                string fullName = parentItem;
                int startIndex = fullName.IndexOf('(') + 1;
                int endIndex = fullName.LastIndexOf(')');
                category = fullName.Substring(startIndex, endIndex - startIndex);
                itemName = fullName.Substring(0, startIndex - 2);
            }

            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT `amount`, `category` FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
            command.Parameters.AddWithValue("@item", itemName);
            command.Parameters.AddWithValue("@cell", cellName);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", category);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    existingAmount = Convert.ToInt32(reader["amount"]);
                }
            }

            MoveItem moveItem = new MoveItem(mapName, itemName, cellName, existingAmount, category);

            if (moveItem.ShowDialog() == DialogResult.OK)
            {
                if (moveItem.EnteredAmount < existingAmount)
                {
                    decimal newAmount = existingAmount - moveItem.EnteredAmount;

                    MySqlCommand updateCommand = new MySqlCommand("UPDATE `items` SET `amount` = @newAmount, `date` = NOW() WHERE `item` = @item AND `cell` = @cell AND `map` = @map AND `category` = @category", db.getConnection());
                    updateCommand.Parameters.AddWithValue("@newAmount", newAmount);
                    updateCommand.Parameters.AddWithValue("@item", itemName);
                    updateCommand.Parameters.AddWithValue("@cell", cellName);
                    updateCommand.Parameters.AddWithValue("@map", mapName);
                    updateCommand.Parameters.AddWithValue("@category", category);
                    updateCommand.ExecuteNonQuery();

                    UpdateOrInsertItem(db, moveItem, itemName, category);
                }
                else if (existingAmount == moveItem.EnteredAmount)
                {
                    UpdateOrInsertItem(db, moveItem, itemName, category);

                    MySqlCommand deleteOldItemCommand = new MySqlCommand("DELETE FROM `items` WHERE `item` = @item AND `cell` = @oldCell AND `map` = @map AND `category` = @category", db.getConnection());
                    deleteOldItemCommand.Parameters.AddWithValue("@item", itemName);
                    deleteOldItemCommand.Parameters.AddWithValue("@oldCell", cellName);
                    deleteOldItemCommand.Parameters.AddWithValue("@map", mapName);
                    deleteOldItemCommand.Parameters.AddWithValue("@category", category);
                    deleteOldItemCommand.ExecuteNonQuery();

                    MessageBox.Show("Товар был перемещен полностью.");
                }
                else
                {
                    MessageBox.Show("Указанное колчество больше имеющегося.");
                }
            }
            else
            {
                return;
            }
            updateItems(db);
            db.closeConnection();
            moveItem.Close();
        }

        private void DeleteItem(DB db, formDeleteItem fDeleteItem, string mapName, string category)
        {
            MySqlCommand command = new MySqlCommand("SELECT `amount` FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
            command.Parameters.AddWithValue("@item", fDeleteItem.ItemToDelete);
            command.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", category);

            int existingAmount = Convert.ToInt32(command.ExecuteScalar());
            decimal requestedAmount = fDeleteItem.EnteredAmount;

            if (requestedAmount < existingAmount)
            {
                MySqlCommand updateCommand = new MySqlCommand("UPDATE `items` SET `amount` = `amount` - @requestedAmount WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
                updateCommand.Parameters.AddWithValue("@requestedAmount", requestedAmount);
                updateCommand.Parameters.AddWithValue("@item", fDeleteItem.ItemToDelete);
                updateCommand.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
                updateCommand.Parameters.AddWithValue("@map", mapName);
                updateCommand.Parameters.AddWithValue("@category", category);
                updateCommand.ExecuteNonQuery();

                AddOperationToHistory("consumption", fDeleteItem.ItemToDelete, fDeleteItem.EnteredAmount.ToString(), userName);

                MessageBox.Show("Указанное колчество товара было удалено.");
            }
            else if (requestedAmount == existingAmount)
            {
                MySqlCommand dellCommand = new MySqlCommand("DELETE FROM `items` WHERE `item` = @itemName AND `cell` = @cell AND `map` = @map AND `category` = @category", db.getConnection());
                dellCommand.Parameters.AddWithValue("@itemName", fDeleteItem.ItemToDelete);
                dellCommand.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
                dellCommand.Parameters.AddWithValue("@map", mapName);
                dellCommand.Parameters.AddWithValue("@category", category);
                dellCommand.ExecuteNonQuery();

                AddOperationToHistory("consumption", fDeleteItem.ItemToDelete, fDeleteItem.EnteredAmount.ToString(), userName);

                MessageBox.Show("Товар был удален.");
            }
            else
            {
                MessageBox.Show("Указанное количество больше имеющегося.");
            }
        }

        private void UpdateOrInsertItem(DB db, MoveItem moveItem, string item, string category)
        {
            if (ItemExists(db, moveItem.CellToMove, item, category))
            {
                MySqlCommand updateAmountCommand = new MySqlCommand("UPDATE `items` SET `amount` = `amount` + @newAmount, `date` = NOW() WHERE `item` = @item AND `cell` = @cell AND `map` = @map AND `category` = @category", db.getConnection());
                updateAmountCommand.Parameters.AddWithValue("@newAmount", moveItem.EnteredAmount);
                updateAmountCommand.Parameters.AddWithValue("@item", item);
                updateAmountCommand.Parameters.AddWithValue("@cell", moveItem.CellToMove);
                updateAmountCommand.Parameters.AddWithValue("@map", mapName);
                updateAmountCommand.Parameters.AddWithValue("@category", category);
                updateAmountCommand.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `items` AUTO_INCREMENT = 1", db.getConnection());
                MySqlCommand newItemCommand = new MySqlCommand("INSERT INTO `items` (`item`, `cell`, `amount`, `map`, `category`, `date`) VALUES (@item, @cell, @amount, @map, @category, NOW())", db.getConnection());
                newItemCommand.Parameters.AddWithValue("@item", item);
                newItemCommand.Parameters.AddWithValue("@cell", moveItem.CellToMove);
                newItemCommand.Parameters.AddWithValue("@amount", moveItem.EnteredAmount);
                newItemCommand.Parameters.AddWithValue("@map", mapName);
                newItemCommand.Parameters.AddWithValue("@category", category);
                autoIncrement.ExecuteNonQuery();
                newItemCommand.ExecuteNonQuery();
            }
        }

        private bool ItemExists(DB db, string cellToMove, string item, string category)
        {
            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
            command.Parameters.AddWithValue("@item", item);
            command.Parameters.AddWithValue("@cell", cellToMove);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", category);

            int count = Convert.ToInt32(command.ExecuteScalar());

            return count > 0;
        }

        private void updateItems(DB db)
        {
            db.openConnection();
            int yOffset = 20; // Инициализировать смещение Y для позиционирования
            Font labelFont = new Font("Arial", 12, FontStyle.Regular);

            MySqlCommand command = new MySqlCommand("SELECT `item`, `category`, `amount` FROM `items` WHERE cell = @cell AND map = @map", db.getConnection());

            command.Parameters.AddWithValue("@cell", cellName);
            command.Parameters.AddWithValue("@map", mapName);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string itemValue = reader["item"].ToString();
                    string categoryValue = reader["category"].ToString();
                    int amountValue = Convert.ToInt32(reader["amount"]);

                    // Создать новую метку для каждого продукта
                    Label label = new Label();
                    label.Name = itemValue;
                    label.Font = labelFont;
                    label.Text = itemValue + " (" + categoryValue + ")" + " - " + amountValue;
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
                    deleteButton.Name = itemValue;
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
                    deleteButton.Name = itemValue;
                    moveButton.Tag = label; // Отметить кнопку соответствующей меткой
                    moveButton.Location = new Point(170, yOffset - 60); // Разместить кнопку перемещения рядом с меткой
                    moveButton.Click += MoveButton_Click; // Присоединить обработчик события нажатия
                    panelItems.Controls.Add(moveButton); // Добавить кнопку перемещения на форму
                }
            }
            panelItems.Controls.Clear();
            showProducts();
            db.closeConnection();
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            formAddItem fAddItem = new formAddItem(mapName, cellName);

            if (fAddItem.ShowDialog() == DialogResult.OK)
            {
                DB db = new DB();
                try
                {
                    db.openConnection();

                    // Проверка наличия товара в БД
                    if (ItemExists(db, fAddItem, mapName))
                    {
                        // Товар уже существует, обновляем количество
                        UpdateItemAmount(db, fAddItem, mapName);
                        AddOperationToHistory("receipt", fAddItem.EnteredItemName, fAddItem.EnteredAmount, userName);

                        MessageBox.Show("Количество товара успешно обновлено.");
                    }
                    else
                    {
                        // Товар не существует, добавляем новую запись
                        InsertNewItem(db, fAddItem, mapName);
                        AddOperationToHistory("receipt", fAddItem.EnteredItemName, fAddItem.EnteredAmount, userName);

                        MessageBox.Show("Товар успешно добавлен.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при работе с базой данных.\n\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.closeConnection();
                }
                updateItems(db);
                fAddItem.Close();
            }
        }

        /// <summary>
        /// Проверка на существование товара
        /// </summary>
        /// <param name="db"></param>
        /// <param name="itemName"></param>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private bool ItemExists(DB db, Form form, string mapName)
        {
            // Проверка наличия товара в БД по заданным условиям

            if (form is formAddItem)
            {
                formAddItem fAddItem = (formAddItem)form;
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map AND BINARY `category` = @category", db.getConnection());
                command.Parameters.AddWithValue("@item", fAddItem.EnteredItemName);
                command.Parameters.AddWithValue("@cell", fAddItem.EnteredCell);
                command.Parameters.AddWithValue("@category", fAddItem.EnteredCategory);
                command.Parameters.AddWithValue("@map", mapName);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            else if (form is formDeleteItem)
            {
                formDeleteItem dellItem = (formDeleteItem)form;
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map ", db.getConnection());
                command.Parameters.AddWithValue("@item", dellItem.ItemToDelete);
                command.Parameters.AddWithValue("@cell", dellItem.EnteredCell);
                command.Parameters.AddWithValue("@map", mapName);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            return false;
        }

        /// <summary>
        /// Товар существует, обновляем его количество
        /// </summary>
        /// <param name="db"></param>
        /// <param name="itemName"></param>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <param name="category"></param>
        /// <param name="newAmount"></param>
        private void UpdateItemAmount(DB db, formAddItem fAddItem, string mapName)
        {
            // Обновление количества товара
            MySqlCommand command = new MySqlCommand("UPDATE `items` SET `amount` = `amount` + @newAmount, `date` = NOW() WHERE `item` = @item AND `cell` = @cell AND `map` = @map AND `category` = @category", db.getConnection());
            command.Parameters.AddWithValue("@item", fAddItem.EnteredItemName);
            command.Parameters.AddWithValue("@cell", fAddItem.EnteredCell);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", fAddItem.EnteredCategory);
            command.Parameters.AddWithValue("@newAmount", fAddItem.EnteredAmount);

            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Товар не существует, добавление нового товара
        /// </summary>
        /// <param name="db"></param>
        /// <param name="itemName"></param>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <param name="category"></param>
        /// <param name="amount"></param>
        private void InsertNewItem(DB db, formAddItem fAddItem, string mapName)
        {
            // Вставка новой записи о товаре
            MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `items` AUTO_INCREMENT = 1", db.getConnection());
            MySqlCommand command = new MySqlCommand("INSERT INTO `items` (`item`, `cell`, `map`, `category`, `amount`, `date`) VALUES (@item, @cell, @map, @category, @amount, NOW())", db.getConnection());

            command.Parameters.AddWithValue("@item", fAddItem.EnteredItemName);
            command.Parameters.AddWithValue("@cell", fAddItem.EnteredCell);
            command.Parameters.AddWithValue("@map", mapName);
            command.Parameters.AddWithValue("@category", fAddItem.EnteredCategory);
            command.Parameters.AddWithValue("@amount", fAddItem.EnteredAmount);

            autoIncrement.ExecuteNonQuery();
            command.ExecuteNonQuery();
        }
    }
}