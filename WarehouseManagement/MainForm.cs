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

namespace WarehouseManagement
{
    public partial class MainForm : Form
    {
        private List<StorageCell> storageCells = new List<StorageCell>();
        private bool isDragging = false;
        private bool isResizing = false;
        private string userPost;
        private int offsetX, offsetY;
        private int resizeStartX, resizeStartY;
        private Button selectedButton = null;
        private bool isEditing = false;
        private bool isEditingPrev = false;
        private ContextMenuStrip cmsButtonDelete = new ContextMenuStrip(); //контекстное меню
        private string imgFileName;
        private string datFileName;
        private List<string> Products = new List<string> { "Рулон", "Пакет" }; // ОТЛАДКА

        public MainForm(string userPost)
        {
            InitializeComponent();
            this.userPost = userPost;
        }

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
            }

            // Загрузка сохраненной карты склада
            // TODO: Реализовать загрузку данных из файла или базы данных
            ToolStripMenuItem menuDelete = new ToolStripMenuItem("Удалить"); //создание объекта-пункта меню
            menuDelete.Click += menuDeleteClick; //обработчик события удаления
            cmsButtonDelete.Items.Add(menuDelete);
            buttonAddCell.Visible = isEditing ? true : false;

            string imagePath = "..\\..\\Resources\\Map1.png";
            Image backgroundImage = Image.FromFile(imagePath);
            imgFileName = Path.GetFileName(imagePath);
            datFileName = Path.ChangeExtension(Path.GetFileNameWithoutExtension(imagePath), "dat");

            // Получаем размеры изображения
            int width = backgroundImage.Width;
            int height = backgroundImage.Height;

            panelWarehouse.Width = width;
            panelWarehouse.Height = height;

            // Устанавливаем изображение фоном для панели и подгружаем ячейки
            panelWarehouse.BackgroundImage = backgroundImage;
            LoadMapDataFromFile(datFileName);
        }

        /// <summary>
        /// Создание ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCell_Click(object sender, EventArgs e)
        {
            // Добавление новой ячейки
            StorageCell newCell = new StorageCell
            {
                //Создание начальных параметров
                Name = "Cell " + (storageCells.Count + 1),
                X = 10,
                Y = 10,
                Width = 50,
                Height = 50
            };

            storageCells.Add(newCell);

            DrawCell(newCell);
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
        /// Создание ячейки на карте с добавлением всех функций
        /// </summary>
        /// <param name="cell"></param>
        private void DrawCell(StorageCell cell)
        {
            if (isEditing)
            {
                InputDialog inputDialog = new InputDialog();
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    cell.Name = inputDialog.EnteredText;
                }
                else
                {
                    return; // Если пользователь отменил ввод, не создавать ячейку
                }
            }

            Button cellButton = new Button();
            cellButton.Text = cell.Name;
            cellButton.Size = new Size(cell.Width, cell.Height);
            cellButton.Location = new Point(cell.X, cell.Y);
            cellButton.MouseDown += CellButton_MouseDown;
            cellButton.MouseMove += CellButton_MouseMove;
            cellButton.MouseUp += CellButton_MouseUp;
            cellButton.Paint += CellButton_Paint;
            cellButton.Click += (sender, e) => ShowProducts(cell);
            cellButton.ContextMenuStrip = isEditing ? cmsButtonDelete : null;

            panelWarehouse.Controls.Add(cellButton);
        }

        private void CellButton_Paint(object sender, PaintEventArgs e)
        {
            Button resizedButton = (Button)sender;
            if (storageCells.Any(c => c.Name == resizedButton.Text && c.IsResizing))
            {
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, resizedButton.Width - 1, resizedButton.Height - 1);
            }
        }

        /// <summary>
        /// Удаление ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDeleteClick(object sender, EventArgs e)
        {
            if (isEditing)
            {
                Button btn = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl as Button;
                panelWarehouse.Controls.Remove(btn);
            }
        }


        /// Показать содержимое ячейки
        /// </summary>
        /// <param name="cell"></param>
        private void ShowProducts(StorageCell cell)
        {
            //Получаем номер ячейки
            string Nm = cell.Name;
            // Если ячейки не находятся в режиме редактирования
            if (!isEditing)
            {
                listBoxProducts.Items.Clear();
                listBoxProducts.Items.AddRange(Products.ToArray());
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
            buttonEditingMap.Text = isEditing ? "Отключить редактирование" : "Редактировать карту";
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
            Login logiForm = new Login();
            logiForm.Show();
            this.Hide();
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddUser_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser();
            if (newUser.ShowDialog() == DialogResult.OK)
            {
                DB db = new DB();
                try
                {
                    db.openConnection();
                    
                    MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `users` AUTO_INCREMENT = 1", db.getConnection());
                    MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `post`) VALUES (@login, @pass, @post)", db.getConnection());

                    // Замените значения параметров на реальные данные
                    command.Parameters.AddWithValue("@login", newUser.newLogin);
                    command.Parameters.AddWithValue("@pass", newUser.newPass);
                    command.Parameters.AddWithValue("@post", "Пользователь");


                    autoIncrement.ExecuteNonQuery();
                    command.ExecuteNonQuery();

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
        }

        /// <summary>
        /// Сохранение карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSaveMapData_Click(object sender, EventArgs e)
        {
            SaveMapDataToFile(datFileName);
        }

        /// <summary>
        /// Загрузка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLoadMapData_Click(object sender, EventArgs e)
        {
            LoadMapDataFromFile(datFileName);
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

                MessageBox.Show("Карта успешно сохранена.");
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
                timerMessageBox.Enabled = true;
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

        private void tsmiLoadMapImg_Click(object sender, EventArgs e)
        {
            if (openFileDialogSelectMapImg.ShowDialog() == DialogResult.OK)
            {
                Image backgroundImage = Image.FromFile(openFileDialogSelectMapImg.FileName);
                imgFileName = Path.GetFileName(openFileDialogSelectMapImg.FileName);
                datFileName = Path.ChangeExtension(Path.GetFileNameWithoutExtension(openFileDialogSelectMapImg.FileName), "dat");

                // Получаем размеры изображения
                int width = backgroundImage.Width;
                int height = backgroundImage.Height;

                panelWarehouse.Width = width;
                panelWarehouse.Height = height;

                // Устанавливаем изображение фоном для панели и подгружаем ячейки
                panelWarehouse.BackgroundImage = backgroundImage;
                panelWarehouse.Controls.Clear();
                LoadMapDataFromFile(datFileName);
            }
        }

        private void tsmiOpenDB_Click(object sender, EventArgs e)
        {
            // Создаем и открываем DBView
            DBView dBView = new DBView();
            dBView.Show();
        }

        /// <summary>
        /// Таймер для уведомлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMessageBox_Tick(object sender, EventArgs e)
        {
            textBoxMessage.Visible = false;
            timerMessageBox.Enabled = false;
        }

        /*
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Загрузка данных о ячейках из файла или базы данных
            // TODO: Реализовать загрузку данных
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сохранение данных о ячейках в файл или базу данных
            // TODO: Реализовать сохранение данных
        }
        */

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}