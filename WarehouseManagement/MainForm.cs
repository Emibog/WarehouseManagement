using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Xls;
using System.Diagnostics;

namespace WarehouseManagement
{
    public partial class MainForm : Form
    {
        public List<StorageCell> storageCells = new List<StorageCell>();
        private bool isDragging = false;
        private bool isResizing = false;
        private string userPost;
        private string userName;
        private int offsetX, offsetY;
        private int resizeStartX, resizeStartY;
        private Button selectedButton = null;
        private bool isEditing = false;
        private bool isEditingPrev = false;
        public ContextMenuStrip cmsButtonDelete = new ContextMenuStrip(); //контекстное меню
        private string imgFileName;
        private string datFileName;
        private string imagePath = "..\\..\\Resources\\Map1.png";
        private string mapName = "";
        private DBView dbView = new DBView();
        private Login loginForm = new Login();
        private List<string> Products = new List<string> { };

        public MainForm(string userPost, string userName)
        {
            InitializeComponent();
            this.userPost = userPost;
            this.userName = userName;
            Text = "Управление складом. Пользователь: " + userName + " (" + userPost + ")";
            //tableLayoutPanel1.RowStyles[1].Height = 10;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Скрытие запрещенных элементов для пользователя
            if (userPost == "Пользователь")
            {
                buttonEditingMap.Visible = false;
                buttonAddCell.Visible = false;
                tsmiAddUser.Visible = false;
                tsmiSaveMapData.Visible = false;
                tsmiOpenDB.Visible = false;
                tsmiAddPost.Visible = false;
            }

            // Загрузка сохраненной карты склада
            ToolStripMenuItem menuDelete = new ToolStripMenuItem("Удалить"); //создание объекта-пункта меню
            menuDelete.Click += menuDeleteClick; //обработчик события удаления
            cmsButtonDelete.Items.Add(menuDelete);
            buttonAddCell.Visible = isEditing ? true : false;


            Image backgroundImage = Image.FromFile(imagePath);
            imgFileName = Path.GetFileName(imagePath);
            mapName = Path.GetFileNameWithoutExtension(imagePath);
            datFileName = Path.ChangeExtension(mapName, "dat");

            // Получаем размеры изображения
            int width = backgroundImage.Width;
            int height = backgroundImage.Height;

            panelWarehouse.Width = width;
            panelWarehouse.Height = height;

            // Устанавливаем изображение фоном для панели и подгружаем ячейки
            panelWarehouse.BackgroundImage = backgroundImage;
            LoadMapDataFromFile(datFileName);
        }

        private void CellButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (isEditing && e.Button == MouseButtons.Left)
            {
                selectedButton = (Button)sender;
                StorageCell cell = storageCells.Find(c => c.Name == selectedButton.Text);

                if (e.X >= selectedButton.Width - 10 && e.Y >= selectedButton.Height - 10)
                {
                    isResizing = true;
                    resizeStartX = e.X;
                    resizeStartY = e.Y;
                }
                else
                {
                    isDragging = true;
                    offsetX = e.X;
                    offsetY = e.Y;
                }
            }
        }

        private void CellButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isEditing && e.Button == MouseButtons.Left)
            {
                if (isDragging)
                {
                    selectedButton.Left = e.X + selectedButton.Left - offsetX;
                    selectedButton.Top = e.Y + selectedButton.Top - offsetY;
                }

                if (isResizing)
                {
                    int newWidth = selectedButton.Width + (e.X - resizeStartX);
                    int newHeight = selectedButton.Height + (e.Y - resizeStartY);

                    if (newWidth > 20 && newHeight > 20)
                    {
                        selectedButton.Size = new Size(newWidth, newHeight);

                        StorageCell cell = storageCells.Find(c => c.Name == selectedButton.Text);
                        if (cell != null)
                        {
                            cell.ButtonWidth = newWidth;
                            cell.ButtonHeight = newHeight;
                        }
                    }

                    resizeStartX = e.X;
                    resizeStartY = e.Y;
                }
            }
        }

        private void CellButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (isEditing && e.Button == MouseButtons.Left)
            {
                isDragging = false;
                isResizing = false;

                StorageCell cell = storageCells.Find(c => c.Name == selectedButton.Text);
                if (cell != null)
                {
                    cell.ButtonX = selectedButton.Left;
                    cell.ButtonY = selectedButton.Top;
                }
            }
        }

        /// <summary>
        /// Создание ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAddCell_Click(object sender, EventArgs e)
        {
            // Добавление новой ячейки
            StorageCell newCell = new StorageCell
            {
                //Создание начальных параметров
                Name = "Cell " + (storageCells.Count + 1),
                X = 10,
                Y = 10,
                Color = Color.White,
                Width = 50,
                Height = 50
            };

            storageCells.Add(newCell);

            DrawCell(newCell);
        }

        /// <summary>
        /// Создание ячейки на карте с добавлением всех функций
        /// </summary>
        /// <param name="cell"></param>
        private void DrawCell(StorageCell cell)
        {
            if (isEditing)
            {
                formInputDialog inputDialog = new formInputDialog();
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    cell.Name = inputDialog.EnteredText;
                    cell.Color = inputDialog.EnteredColor;
                    DB db = new DB();
                    try
                    {
                        db.openConnection();

                        MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `cells` AUTO_INCREMENT = 1", db.getConnection());
                        MySqlCommand command = new MySqlCommand("INSERT INTO `cells` (`cell`, `map`) VALUES (@cell, @map)", db.getConnection());

                        // Замените значения параметров на реальные данные
                        command.Parameters.AddWithValue("@cell", cell.Name);
                        command.Parameters.AddWithValue("@map", mapName);

                        autoIncrement.ExecuteNonQuery();
                        command.ExecuteNonQuery();

                        // Закрываем соединение
                        db.closeConnection();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении ячейки.\n\n" + ex.Message, "Ошибка добавления ячейки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        db.closeConnection();
                    }
                    inputDialog.Close();
                }
                else
                {
                    return; // Если пользователь отменил ввод, не создавать ячейку
                }
            }

            Button cellButton = new Button();
            cellButton.Text = cell.Name;
            cellButton.Size = new Size(cell.Width, cell.Height);
            cellButton.BackColor = cell.Color;
            //cellButton.BackColor = Color.White;
            cellButton.Location = new Point(cell.X, cell.Y);
            cellButton.MouseDown += CellButton_MouseDown;
            cellButton.MouseMove += CellButton_MouseMove;
            cellButton.MouseUp += CellButton_MouseUp;
            cellButton.Paint += CellButton_Paint;
            cellButton.Click += (sender, e) => ShowProducts(cell);
            cellButton.ContextMenuStrip = isEditing ? cmsButtonDelete : null;

            panelWarehouse.Controls.Add(cellButton);
            if (isEditing)
            {
                SaveMapDataToFile(datFileName);
            }
        }

        /// <summary>
        /// Обрабатывает отрисовку кнопки ячейки
        /// </summary>
        /// <param name="sender">Кнопка, вызвавшая событие</param>
        /// <param name="e">PaintEventArgs, содержащий данные события</param>
        private void CellButton_Paint(object sender, PaintEventArgs e)
        {
            Button resizedButton = (Button)sender;
            bool isResizingCell = storageCells.Any(c => c.Name == resizedButton.Text && c.IsResizing);

            if (isResizingCell)
            {
                // Измените размер прямоугольника, чтобы он соответствовал кнопке
                int width = resizedButton.Width - 1;
                int height = resizedButton.Height - 1;

                // Нарисуйте черный прямоугольник вокруг кнопки
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, width, height);
            }
        }

        /// <summary>
        /// Удаление ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuDeleteClick(object sender, EventArgs e)
        {
            if (isEditing)
            {
                Button btn = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl as Button;
                DB db = new DB();
                try
                {
                    db.openConnection();

                    MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `cells` AUTO_INCREMENT = 1", db.getConnection());
                    MySqlCommand autoIncrementItems = new MySqlCommand("ALTER TABLE `cells` AUTO_INCREMENT = 1", db.getConnection());
                    MySqlCommand command = new MySqlCommand("DELETE FROM `cells` WHERE `cell` = @cell", db.getConnection());
                    MySqlCommand delItemsInCell = new MySqlCommand("DELETE FROM `items` WHERE `cell` = @cell", db.getConnection());

                    // Замените значения параметров на реальные данные
                    command.Parameters.AddWithValue("@cell", btn.Text);
                    delItemsInCell.Parameters.AddWithValue("@cell", btn.Text);

                    autoIncrement.ExecuteNonQuery();
                    autoIncrementItems.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    delItemsInCell.ExecuteNonQuery();

                    Products.Clear();

                    // Закрываем соединение
                    db.closeConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении ячейки.\n\n" + ex.Message, "Ошибка удалении ячейки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.closeConnection();
                }
                panelWarehouse.Controls.Remove(btn);
                SaveMapDataToFile(datFileName);
            }
        }

        /// <summary>
        /// Показать содержимое ячейки и количество товара
        /// </summary>
        /// <param name="cell"></param>
        private void ShowProducts(StorageCell cell)
        {
            string cellName = cell.Name;
            if (!isEditing)
            {
                try
                {
                    Items formItems = new Items(mapName, cellName, userName);
                    formItems.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при отображении товаров.\n\n" + ex.Message, "Ошибка отображения товаров", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Включение/отключение редактированиия карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditingMap_Click(object sender, EventArgs e)
        {
            isEditing = !isEditing;
            buttonEditingMap.Image = isEditing ? Properties.Resources.editingMapOff : Properties.Resources.editingMap;
            if (!isEditing)
            {
                SaveMapDataToFile(datFileName);
            }
            buttonAddCell.Visible = isEditing ? true : false;

            // Устанавливаем или снимаем ContextMenuStrip у существующих кнопок в зависимости от значения isEditing
            foreach (Control control in panelWarehouse.Controls)
            {
                if (control is Button button)
                {
                    button.ContextMenuStrip = isEditing ? cmsButtonDelete : null;
                }
            }
        }

        /// <summary>
        /// Смена пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChangeUser_Click(object sender, EventArgs e)
        {
            SaveMapDataToFile(datFileName);
            loginForm.Show();
            this.Hide();
            dbView.Hide();
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddUser_Click(object sender, EventArgs e)
        {
            formAddUser addUser = new formAddUser();

            if (addUser.ShowDialog() == DialogResult.OK)
            {
                DB db = new DB();
                try
                {
                    db.openConnection();
                    dbAddUser(db, addUser);

                    MessageBox.Show("Пользователь успешно добавлен.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении пользователя в базу данных.\n\n" + ex.Message, "Ошибка добавления пользователя", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.closeConnection();
                }
            }
            addUser.Close();
        }

        private void tsmiAddPost_Click(object sender, EventArgs e)
        {
            AddPost addPost = new AddPost(userName);
            if (addPost.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Должность успешно добавлена.");
            }
            addPost.Close();
        }

        /// <summary>
        /// Запрос добавления пользователя
        /// </summary>
        /// <param name="db"></param>
        /// <param name="addUser"></param>
        private void dbAddUser(DB db, formAddUser addUser)
        {
            MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `users` AUTO_INCREMENT = 1", db.getConnection());
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `post`) VALUES (@login, @pass, @post)", db.getConnection());

            command.Parameters.AddWithValue("@login", addUser.newLogin);
            command.Parameters.AddWithValue("@pass", addUser.newPass);
            command.Parameters.AddWithValue("@post", "Пользователь");

            autoIncrement.ExecuteNonQuery();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Сохранение карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSaveMapData_Click(object sender, EventArgs e)
        {
            SaveMapDataToFile(datFileName);
            disableEditingMod();
        }

        /// <summary>
        /// Загрузка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLoadMapData_Click(object sender, EventArgs e)
        {
            LoadMapDataFromFile(datFileName);
            disableEditingMod();
        }

        /// <summary>
        /// Обновление списка созданных ячеек для сериализации
        /// </summary>
        private void UpdateStorageCellsFromButtons()
        {
            storageCells.Clear();

            foreach (Control control in panelWarehouse.Controls)
            {
                if (control is Button button)
                {
                    StorageCell cell = new StorageCell
                    {
                        Name = button.Text,
                        X = button.Left,
                        Y = button.Top,
                        Color = button.BackColor,
                        Width = button.Width,
                        Height = button.Height
                    };
                    storageCells.Add(cell);
                }
            }
        }

        /// <summary>
        /// Сериализация карты. Сохранение в файл
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveMapDataToFile(string fileName)
        {
            UpdateStorageCellsFromButtons();

            List<StorageCell> cellDataList = storageCells;

            // Сериализация
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, cellDataList);
                }

                textBoxMessage.Text = "Карта успешно сохранена";
                textBoxMessage.Visible = true;
                timerMessageBox.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении карты.\n\n{ex.Message}", "Ошибка сохранения карты", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Десериализация. Загрузка из файла
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadMapDataFromFile(string fileName)
        {
            // Десериализация
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    storageCells = (List<StorageCell>)formatter.Deserialize(fs);

                    isEditingPrev = isEditing;
                    isEditing = false;
                    // Очищаем панель перед добавлением ячеек
                    panelWarehouse.Controls.Clear();

                    // Перерисовываем ячейки
                    foreach (StorageCell cell in storageCells)
                    {
                        DrawCell(cell);
                    }
                }

                isEditing = isEditingPrev;
                textBoxMessage.Text = "Карта успешно загружена";
                textBoxMessage.Visible = true;
                timerMessageBox.Start();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл с картой не найден.", "Ошибка загрузки карты", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке карты.\n\n{ex.Message}", "Ошибка загрузки карты", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка изображения склада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLoadMapImg_Click(object sender, EventArgs e)
        {
            if (openFileDialogSelectMapImg.ShowDialog() == DialogResult.OK)
            {
                SaveMapDataToFile(datFileName);

                Image backgroundImage = Image.FromFile(openFileDialogSelectMapImg.FileName);
                imgFileName = Path.GetFileName(openFileDialogSelectMapImg.FileName);
                mapName = Path.GetFileNameWithoutExtension(openFileDialogSelectMapImg.FileName);
                datFileName = Path.ChangeExtension(mapName, "dat");

                // Получаем размеры изображения
                int width = backgroundImage.Width;
                int height = backgroundImage.Height;

                panelWarehouse.Width = width;
                panelWarehouse.Height = height;

                // Устанавливаем изображение фоном для панели и подгружаем ячейки
                panelWarehouse.BackgroundImage = backgroundImage;
                panelWarehouse.Controls.Clear();

                disableEditingMod();

                LoadMapDataFromFile(datFileName);
            }
        }

        /// <summary>
        /// Открытие окна с БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenDB_Click(object sender, EventArgs e)
        {
            // Открываем DBView
            dbView.Show();
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

        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddItem_Click(object sender, EventArgs e)
        {
            formAddItem fAddItem = new formAddItem(mapName);

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

                fAddItem.Close();
            }
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDeleteItem_Click(object sender, EventArgs e)
        {
            formDeleteItem fDeleteItem = new formDeleteItem(mapName);

            if (fDeleteItem.ShowDialog() == DialogResult.OK)
            {
                DB db = new DB();
                try
                {
                    db.openConnection();

                    // Проверка наличия товара в БД
                    if (ItemExists(db, fDeleteItem, mapName))
                    {
                        // Товар существует, удаляем его
                        DeleteItem(db, fDeleteItem, mapName);
                    }
                    else
                    {
                        MessageBox.Show("Товар не найден.");
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
                fDeleteItem.Close();
            }
        }

        /// <summary>
        /// Удаление товара из базы данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="fDeleteItem"></param>
        /// <param name="mapName"></param>
        private void DeleteItem(DB db, formDeleteItem fDeleteItem, string mapName)
        {
            MySqlCommand command = new MySqlCommand("SELECT `amount` FROM `items` WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map", db.getConnection());
            command.Parameters.AddWithValue("@item", fDeleteItem.ItemToDelete);
            command.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
            command.Parameters.AddWithValue("@map", mapName);

            int existingAmount = Convert.ToInt32(command.ExecuteScalar());
            decimal requestedAmount = fDeleteItem.EnteredAmount;

            if (requestedAmount < existingAmount)
            {
                MySqlCommand updateCommand = new MySqlCommand("UPDATE `items` SET `amount` = `amount` - @requestedAmount WHERE BINARY `item` = @item AND BINARY `cell` = @cell AND BINARY `map` = @map", db.getConnection());
                updateCommand.Parameters.AddWithValue("@requestedAmount", requestedAmount);
                updateCommand.Parameters.AddWithValue("@item", fDeleteItem.ItemToDelete);
                updateCommand.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
                updateCommand.Parameters.AddWithValue("@map", mapName);
                updateCommand.ExecuteNonQuery();

                AddOperationToHistory("consumption", fDeleteItem.ItemToDelete, fDeleteItem.EnteredAmount.ToString(), userName);

                MessageBox.Show("Указанное колчество товара было удалено.");
            }
            else if (requestedAmount == existingAmount)
            {
                MySqlCommand dellCommand = new MySqlCommand("DELETE FROM `items` WHERE `item` = @itemName AND `cell` = @cell AND `map` = @map", db.getConnection());
                Console.WriteLine(fDeleteItem.ItemToDelete);
                dellCommand.Parameters.AddWithValue("@itemName", fDeleteItem.ItemToDelete);
                dellCommand.Parameters.AddWithValue("@cell", fDeleteItem.EnteredCell);
                dellCommand.Parameters.AddWithValue("@map", mapName);
                dellCommand.ExecuteNonQuery();

                AddOperationToHistory("consumption", fDeleteItem.ItemToDelete, fDeleteItem.EnteredAmount.ToString(), userName);

                MessageBox.Show("Товар был удален.");
            }
            else
            {
                MessageBox.Show("Указанное количество больше имеющегося.");
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

        /// <summary>
        /// Таймер для уведомлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMessageBox_Tick(object sender, EventArgs e)
        {
            textBoxMessage.Visible = false;
            timerMessageBox.Stop();
        }

        /// <summary>
        /// Добавление новой категории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddCategory_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Запрос добавления категории
        /// </summary>
        /// <param name="db"></param>
        /// <param name="fAddCategory"></param>
        private void dbAddCategory(DB db, formAddCategory fAddCategory)
        {
            MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `categories` AUTO_INCREMENT = 1", db.getConnection());
            MySqlCommand command = new MySqlCommand("INSERT INTO `categories` (`category`) VALUES (@category)", db.getConnection());

            command.Parameters.AddWithValue("@category", fAddCategory.categoryName);

            autoIncrement.ExecuteNonQuery();
            command.ExecuteNonQuery();
        }

        private void tsmiReceipt_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogReceipt = new SaveFileDialog();

            saveFileDialogReceipt.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialogReceipt.FilterIndex = 1;
            saveFileDialogReceipt.RestoreDirectory = true;

            if (saveFileDialogReceipt.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialogReceipt.FileName;

                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];

                // Retrieve data from the "receipt" table in the database
                DataTable receiptData = RetrieveDataFromDB("receipt");
                worksheet.Range[1, 1].Value = "Товар";
                worksheet.Range[1, 2].Value = "Количество";
                worksheet.Range[1, 3].Value = "Пользователь";
                worksheet.Range[1, 4].Value = "Дата";
                // Write data to specific cells in the Excel file
                for (int i = 0; i < receiptData.Rows.Count; i++)
                {
                    worksheet.Range[i + 2, 1].Value = receiptData.Rows[i]["item"].ToString();
                    worksheet.Range[i + 2, 2].Value = receiptData.Rows[i]["amount"].ToString();
                    worksheet.Range[i + 2, 3].Value = receiptData.Rows[i]["user"].ToString();
                    worksheet.Range[i + 2, 4].Value = "'" + receiptData.Rows[i]["date"].ToString() + "'";
                }


                CellStyle style = workbook.Styles.Add("newStyle");
                style.Font.IsBold = false;
                style.Font.FontName = "Times New Roman";
                style.Font.Size = 14;
                
                CellStyle styleBold = workbook.Styles.Add("newStyleBold");
                styleBold.Font.IsBold = true;
                styleBold.Font.FontName = "Times New Roman";
                styleBold.Font.Size = 14;

                int rowCount = worksheet.Rows.Length;
                int colCount = worksheet.Columns.Length;

                CellRange entireTable = worksheet.Range[1, 1, rowCount, colCount];
                entireTable.Style = style;
                worksheet.Range[1, 1, 1, 4].Style = styleBold;
                worksheet.AllocatedRange.AutoFitColumns();

                try
                {
                    workbook.SaveToFile(filePath); // Save the file to the selected path
                    MessageBox.Show("Файл успешно сохранен!"); // Show the saved file path
                    
                    
                    /*ProcessStartInfo info = new ProcessStartInfo(filePath);
                    info.Verb = "PrintTo";
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(info);

                    Process.Start(filePath);*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }   
        }

        private void tsmiConsumption_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogReceipt = new SaveFileDialog();

            saveFileDialogReceipt.Filter = "Excel files (*.xlsx)|*.xlsx";
            saveFileDialogReceipt.FilterIndex = 1;
            saveFileDialogReceipt.RestoreDirectory = true;

            if (saveFileDialogReceipt.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialogReceipt.FileName;

                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];

                // Retrieve data from the "receipt" table in the database
                DataTable receiptData = RetrieveDataFromDB("consumption");
                worksheet.Range[1, 1].Value = "Товар";
                worksheet.Range[1, 2].Value = "Количество";
                worksheet.Range[1, 3].Value = "Пользователь";
                worksheet.Range[1, 4].Value = "Дата";
                // Write data to specific cells in the Excel file
                for (int i = 0; i < receiptData.Rows.Count; i++)
                {
                    worksheet.Range[i + 2, 1].Value = receiptData.Rows[i]["item"].ToString();
                    worksheet.Range[i + 2, 2].Value = receiptData.Rows[i]["amount"].ToString();
                    worksheet.Range[i + 2, 3].Value = receiptData.Rows[i]["user"].ToString();
                    worksheet.Range[i + 2, 4].Value = "'" + receiptData.Rows[i]["date"].ToString() + "'";
                }


                CellStyle style = workbook.Styles.Add("newStyle");
                style.Font.IsBold = false;
                style.Font.FontName = "Times New Roman";
                style.Font.Size = 14;

                CellStyle styleBold = workbook.Styles.Add("newStyleBold");
                styleBold.Font.IsBold = true;
                styleBold.Font.FontName = "Times New Roman";
                styleBold.Font.Size = 14;

                int rowCount = worksheet.Rows.Length;
                int colCount = worksheet.Columns.Length;

                CellRange entireTable = worksheet.Range[1, 1, rowCount, colCount];
                entireTable.Style = style;
                worksheet.Range[1, 1, 1, 4].Style = styleBold;
                worksheet.AllocatedRange.AutoFitColumns();

                try
                {
                    workbook.SaveToFile(filePath); // Save the file to the selected path
                    MessageBox.Show("Файл успешно сохранен!"); // Show the saved file path


                    /*ProcessStartInfo info = new ProcessStartInfo(filePath);
                    info.Verb = "PrintTo";
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(info);

                    Process.Start(filePath);*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private DataTable RetrieveDataFromDB(string table)
        {
            DB db = new DB();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT item, amount, user, date FROM " + table, db.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            db.closeConnection();

            return dataTable;
        }

        /// <summary>
        /// Отключение режима редактирования (для событий не связанных с изменением карты)
        /// </summary>
        private void disableEditingMod()
        {
            if (isEditing)
            {
                isEditing = false;
                buttonEditingMap.Image = Properties.Resources.editingMap;
                buttonAddCell.Visible = false;

                // Устанавливаем или снимаем ContextMenuStrip у существующих кнопок в зависимости от значения isEditing
                foreach (Control control in panelWarehouse.Controls)
                {
                    if (control is Button button)
                    {
                        button.ContextMenuStrip = null;
                    }
                }
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMapDataToFile(datFileName);
            Application.Exit();
        }
    }
}