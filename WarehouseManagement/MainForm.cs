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
        private ContextMenuStrip cmsButtonDelete = new ContextMenuStrip(); //контекстное меню
        private List<string> Products = new List<string> { "Рулон", "Пакет" }; // ОТЛАДКА

        public MainForm(string userPost)
        {
            InitializeComponent();
            this.userPost = userPost;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (userPost == "Администратор")
            {
                // Возможности для администратора
            }
            else if (userPost == "Пользователь")
            {
                buttonEditingMap.Visible = false;
                tsmiAddUser.Visible = false;
            }

            // Загрузка сохраненной карты склада
            // TODO: Реализовать загрузку данных из файла или базы данных
            ToolStripMenuItem menuDelete = new ToolStripMenuItem("Удалить"); //создание объекта-пункта меню
            menuDelete.Click += menuDeleteClick; //обработчик события удаления
            cmsButtonDelete.Items.Add(menuDelete);
            buttonAddCell.Visible = isEditing ? true : false;

            Image backgroundImage = Properties.Resources.Map;
            // Получаем размеры изображения
            int width = backgroundImage.Width;
            int height = backgroundImage.Height;

            panelWarehouse.Width = width;
            panelWarehouse.Height = height;

            // Устанавливаем изображение фоном для панели
            panelWarehouse.BackgroundImage = backgroundImage;
        }

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


        private void DrawCell(StorageCell cell)
        {
            if (isEditing)
            {
                InputDialog inputDialog = new InputDialog();
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    cell.Name = inputDialog.EnteredText;
                    Button cellButton = new Button();
                    cellButton.Text = inputDialog.EnteredText;
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
            }
        }


        private void menuDeleteClick(object sender, EventArgs e)
        {
            if (isEditing)
            {
                Button btn = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl as Button;
                panelWarehouse.Controls.Remove(btn);
            }
        }

        private void CellButton_Paint(object sender, PaintEventArgs e)
        {
            Button resizedButton = (Button)sender;
            if (storageCells.Any(c => c.Name == resizedButton.Text && c.IsResizing))
            {
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, resizedButton.Width - 1, resizedButton.Height - 1);
            }
        }


        private void ShowProducts(StorageCell cell)
        {
            //Получаем номер ячейки
            string Nm = cell.Name;
            // Отобразить список товаров в ячейке
            if (!isEditing)
            {
                listBoxProducts.Items.Clear();
                //MessageBox.Show(Nm);
                listBoxProducts.Items.AddRange(Products.ToArray());
            }
        }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сохранение данных о ячейках в файл или базу данных
            // TODO: Реализовать сохранение данных
        }

        private void tsmiChangeUser_Click(object sender, EventArgs e)
        {
            Login logiForm = new Login();
            logiForm.Show();
            this.Hide();
        }

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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Загрузка данных о ячейках из файла или базы данных
            // TODO: Реализовать загрузку данных
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}