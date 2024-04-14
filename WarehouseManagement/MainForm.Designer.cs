
namespace WarehouseManagement
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelWarehouse = new System.Windows.Forms.Panel();
            this.panelScroll = new System.Windows.Forms.Panel();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.tsmChangeUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddPost = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеСкладомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadMapImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveMapData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadMapData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDB = new System.Windows.Forms.ToolStripMenuItem();
            this.товарыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьТоварToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПриходаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетРасходаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogSelectMapImg = new System.Windows.Forms.OpenFileDialog();
            this.timerMessageBox = new System.Windows.Forms.Timer(this.components);
            this.textBoxMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonEditingMap = new System.Windows.Forms.PictureBox();
            this.buttonAddCell = new System.Windows.Forms.PictureBox();
            this.panelScroll.SuspendLayout();
            this.menuStripMainForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditingMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAddCell)).BeginInit();
            this.SuspendLayout();
            // 
            // panelWarehouse
            // 
            this.panelWarehouse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelWarehouse.Location = new System.Drawing.Point(3, 0);
            this.panelWarehouse.Name = "panelWarehouse";
            this.panelWarehouse.Size = new System.Drawing.Size(222, 110);
            this.panelWarehouse.TabIndex = 0;
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.panelWarehouse);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(3, 3);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(912, 427);
            this.panelScroll.TabIndex = 4;
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.BackColor = System.Drawing.Color.Khaki;
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmChangeUsers,
            this.управлениеСкладомToolStripMenuItem,
            this.товарыToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(918, 24);
            this.menuStripMainForm.TabIndex = 7;
            this.menuStripMainForm.Text = "Меню главной формы";
            // 
            // tsmChangeUsers
            // 
            this.tsmChangeUsers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangeUser,
            this.tsmiAddUser,
            this.tsmiAddPost});
            this.tsmChangeUsers.Image = global::WarehouseManagement.Properties.Resources.user;
            this.tsmChangeUsers.Name = "tsmChangeUsers";
            this.tsmChangeUsers.Size = new System.Drawing.Size(195, 20);
            this.tsmChangeUsers.Text = "&Управление пользователями";
            // 
            // tsmiChangeUser
            // 
            this.tsmiChangeUser.Image = global::WarehouseManagement.Properties.Resources.changeUser;
            this.tsmiChangeUser.Name = "tsmiChangeUser";
            this.tsmiChangeUser.Size = new System.Drawing.Size(204, 22);
            this.tsmiChangeUser.Text = "&Сменить пользователя";
            this.tsmiChangeUser.Click += new System.EventHandler(this.tsmiChangeUser_Click);
            // 
            // tsmiAddUser
            // 
            this.tsmiAddUser.Image = global::WarehouseManagement.Properties.Resources.addUser;
            this.tsmiAddUser.Name = "tsmiAddUser";
            this.tsmiAddUser.Size = new System.Drawing.Size(204, 22);
            this.tsmiAddUser.Text = "&Добавить пользователя";
            this.tsmiAddUser.Click += new System.EventHandler(this.tsmiAddUser_Click);
            // 
            // tsmiAddPost
            // 
            this.tsmiAddPost.Name = "tsmiAddPost";
            this.tsmiAddPost.Size = new System.Drawing.Size(204, 22);
            this.tsmiAddPost.Text = "Добавить должность";
            this.tsmiAddPost.Click += new System.EventHandler(this.tsmiAddPost_Click);
            // 
            // управлениеСкладомToolStripMenuItem
            // 
            this.управлениеСкладомToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadMapImg,
            this.tsmiSaveMapData,
            this.tsmiLoadMapData,
            this.tsmiOpenDB});
            this.управлениеСкладомToolStripMenuItem.Image = global::WarehouseManagement.Properties.Resources.warehouse;
            this.управлениеСкладомToolStripMenuItem.Name = "управлениеСкладомToolStripMenuItem";
            this.управлениеСкладомToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.управлениеСкладомToolStripMenuItem.Text = "Управление &складом";
            // 
            // tsmiLoadMapImg
            // 
            this.tsmiLoadMapImg.Image = global::WarehouseManagement.Properties.Resources.loadImg;
            this.tsmiLoadMapImg.Name = "tsmiLoadMapImg";
            this.tsmiLoadMapImg.Size = new System.Drawing.Size(201, 22);
            this.tsmiLoadMapImg.Text = "Загрузить к&арту склада";
            this.tsmiLoadMapImg.Click += new System.EventHandler(this.tsmiLoadMapImg_Click);
            // 
            // tsmiSaveMapData
            // 
            this.tsmiSaveMapData.Image = global::WarehouseManagement.Properties.Resources.saveCells;
            this.tsmiSaveMapData.Name = "tsmiSaveMapData";
            this.tsmiSaveMapData.Size = new System.Drawing.Size(201, 22);
            this.tsmiSaveMapData.Text = "&Сохранить ячейки";
            this.tsmiSaveMapData.Click += new System.EventHandler(this.tsmiSaveMapData_Click);
            // 
            // tsmiLoadMapData
            // 
            this.tsmiLoadMapData.Image = global::WarehouseManagement.Properties.Resources.loadCells;
            this.tsmiLoadMapData.Name = "tsmiLoadMapData";
            this.tsmiLoadMapData.Size = new System.Drawing.Size(201, 22);
            this.tsmiLoadMapData.Text = "&Загрузить ячейки";
            this.tsmiLoadMapData.Click += new System.EventHandler(this.tsmiLoadMapData_Click);
            // 
            // tsmiOpenDB
            // 
            this.tsmiOpenDB.Image = global::WarehouseManagement.Properties.Resources.openDB;
            this.tsmiOpenDB.Name = "tsmiOpenDB";
            this.tsmiOpenDB.Size = new System.Drawing.Size(201, 22);
            this.tsmiOpenDB.Text = "Открыть базу данных";
            this.tsmiOpenDB.Click += new System.EventHandler(this.tsmiOpenDB_Click);
            // 
            // товарыToolStripMenuItem
            // 
            this.товарыToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.товарыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddItem,
            this.удалитьТоварToolStripMenuItem,
            this.tsmiAddCategory});
            this.товарыToolStripMenuItem.Image = global::WarehouseManagement.Properties.Resources.item;
            this.товарыToolStripMenuItem.Name = "товарыToolStripMenuItem";
            this.товарыToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.товарыToolStripMenuItem.Text = "Товары";
            // 
            // tsmiAddItem
            // 
            this.tsmiAddItem.Name = "tsmiAddItem";
            this.tsmiAddItem.Size = new System.Drawing.Size(188, 22);
            this.tsmiAddItem.Text = "Добавить товар";
            this.tsmiAddItem.Click += new System.EventHandler(this.tsmiAddItem_Click);
            // 
            // удалитьТоварToolStripMenuItem
            // 
            this.удалитьТоварToolStripMenuItem.Name = "удалитьТоварToolStripMenuItem";
            this.удалитьТоварToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.удалитьТоварToolStripMenuItem.Text = "Удалить товар";
            this.удалитьТоварToolStripMenuItem.Click += new System.EventHandler(this.tsmiDeleteItem_Click);
            // 
            // tsmiAddCategory
            // 
            this.tsmiAddCategory.Name = "tsmiAddCategory";
            this.tsmiAddCategory.Size = new System.Drawing.Size(188, 22);
            this.tsmiAddCategory.Text = "Добавить категорию";
            this.tsmiAddCategory.Click += new System.EventHandler(this.tsmiAddCategory_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчетПриходаToolStripMenuItem,
            this.отчетРасходаToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // отчетПриходаToolStripMenuItem
            // 
            this.отчетПриходаToolStripMenuItem.Name = "отчетПриходаToolStripMenuItem";
            this.отчетПриходаToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.отчетПриходаToolStripMenuItem.Text = "Отчет прихода";
            // 
            // отчетРасходаToolStripMenuItem
            // 
            this.отчетРасходаToolStripMenuItem.Name = "отчетРасходаToolStripMenuItem";
            this.отчетРасходаToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.отчетРасходаToolStripMenuItem.Text = "Отчет расхода";
            // 
            // openFileDialogSelectMapImg
            // 
            this.openFileDialogSelectMapImg.FileName = "MapImg";
            // 
            // timerMessageBox
            // 
            this.timerMessageBox.Interval = 4000;
            this.timerMessageBox.Tick += new System.EventHandler(this.timerMessageBox_Tick);
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.AutoSize = true;
            this.textBoxMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.textBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.textBoxMessage.Location = new System.Drawing.Point(682, 12);
            this.textBoxMessage.MinimumSize = new System.Drawing.Size(100, 0);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(100, 16);
            this.textBoxMessage.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.92593F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.07407F));
            this.tableLayoutPanel1.Controls.Add(this.panelScroll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.47619F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.52381F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 525);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.40648F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.59352F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 233F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxMessage, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonEditingMap, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonAddCell, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 436);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.95349F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.04651F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(912, 86);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // buttonEditingMap
            // 
            this.buttonEditingMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEditingMap.Image = global::WarehouseManagement.Properties.Resources.editingMap;
            this.buttonEditingMap.Location = new System.Drawing.Point(3, 15);
            this.buttonEditingMap.Name = "buttonEditingMap";
            this.buttonEditingMap.Size = new System.Drawing.Size(377, 68);
            this.buttonEditingMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonEditingMap.TabIndex = 9;
            this.buttonEditingMap.TabStop = false;
            this.buttonEditingMap.Click += new System.EventHandler(this.btnEditingMap_Click);
            // 
            // buttonAddCell
            // 
            this.buttonAddCell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddCell.Image = global::WarehouseManagement.Properties.Resources.addCell;
            this.buttonAddCell.Location = new System.Drawing.Point(386, 15);
            this.buttonAddCell.Name = "buttonAddCell";
            this.buttonAddCell.Size = new System.Drawing.Size(290, 68);
            this.buttonAddCell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buttonAddCell.TabIndex = 10;
            this.buttonAddCell.TabStop = false;
            this.buttonAddCell.Click += new System.EventHandler(this.btnAddCell_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 549);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStripMainForm);
            this.MainMenuStrip = this.menuStripMainForm;
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление складом";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelScroll.ResumeLayout(false);
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditingMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonAddCell)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelWarehouse;
        private System.Windows.Forms.Panel panelScroll;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem tsmChangeUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddUser;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeUser;
        private System.Windows.Forms.OpenFileDialog openFileDialogSelectMapImg;
        private System.Windows.Forms.ToolStripMenuItem управлениеСкладомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadMapData;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveMapData;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadMapImg;
        private System.Windows.Forms.Timer timerMessageBox;
        private System.Windows.Forms.Label textBoxMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenDB;
        private System.Windows.Forms.ToolStripMenuItem товарыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCategory;
        private System.Windows.Forms.ToolStripMenuItem удалитьТоварToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddPost;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПриходаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетРасходаToolStripMenuItem;
        private System.Windows.Forms.PictureBox buttonEditingMap;
        private System.Windows.Forms.PictureBox buttonAddCell;
    }
}

