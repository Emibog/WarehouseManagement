
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
            this.listBoxProducts = new System.Windows.Forms.ListBox();
            this.buttonAddCell = new System.Windows.Forms.Button();
            this.buttonEditingMap = new System.Windows.Forms.Button();
            this.panelScroll = new System.Windows.Forms.Panel();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.товарыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialogSelectMapImg = new System.Windows.Forms.OpenFileDialog();
            this.timerMessageBox = new System.Windows.Forms.Timer(this.components);
            this.textBoxMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tsmChangeUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddUser = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеСкладомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadMapImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadMapData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveMapData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.panelScroll.SuspendLayout();
            this.menuStripMainForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            // listBoxProducts
            // 
            this.listBoxProducts.FormattingEnabled = true;
            this.listBoxProducts.Location = new System.Drawing.Point(3, 3);
            this.listBoxProducts.Name = "listBoxProducts";
            this.listBoxProducts.Size = new System.Drawing.Size(174, 160);
            this.listBoxProducts.TabIndex = 1;
            // 
            // buttonAddCell
            // 
            this.buttonAddCell.Location = new System.Drawing.Point(3, 211);
            this.buttonAddCell.Name = "buttonAddCell";
            this.buttonAddCell.Size = new System.Drawing.Size(174, 23);
            this.buttonAddCell.TabIndex = 2;
            this.buttonAddCell.Text = "Добавить ячейку";
            this.buttonAddCell.UseVisualStyleBackColor = true;
            this.buttonAddCell.Click += new System.EventHandler(this.btnAddCell_Click);
            // 
            // buttonEditingMap
            // 
            this.buttonEditingMap.Location = new System.Drawing.Point(3, 176);
            this.buttonEditingMap.Name = "buttonEditingMap";
            this.buttonEditingMap.Size = new System.Drawing.Size(174, 23);
            this.buttonEditingMap.TabIndex = 3;
            this.buttonEditingMap.Text = "Редактировать карту";
            this.buttonEditingMap.UseVisualStyleBackColor = true;
            this.buttonEditingMap.Click += new System.EventHandler(this.btnEditingMap_Click);
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.panelWarehouse);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(3, 3);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(602, 383);
            this.panelScroll.TabIndex = 4;
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmChangeUsers,
            this.управлениеСкладомToolStripMenuItem,
            this.товарыToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(918, 24);
            this.menuStripMainForm.TabIndex = 7;
            this.menuStripMainForm.Text = "Меню главной формы";
            // 
            // товарыToolStripMenuItem
            // 
            this.товарыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddItem,
            this.tsmiAddCategory});
            this.товарыToolStripMenuItem.Name = "товарыToolStripMenuItem";
            this.товарыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.товарыToolStripMenuItem.Text = "Товары";
            // 
            // tsmiAddItem
            // 
            this.tsmiAddItem.Name = "tsmiAddItem";
            this.tsmiAddItem.Size = new System.Drawing.Size(188, 22);
            this.tsmiAddItem.Text = "Добавить товар";
            this.tsmiAddItem.Click += new System.EventHandler(this.tsmiAddItem_Click);
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
            this.textBoxMessage.Location = new System.Drawing.Point(183, 0);
            this.textBoxMessage.MinimumSize = new System.Drawing.Size(100, 0);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(100, 16);
            this.textBoxMessage.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.33987F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.66013F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelScroll, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.20862F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.79138F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 441);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.375F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.625F));
            this.tableLayoutPanel2.Controls.Add(this.buttonEditingMap, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.listBoxProducts, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxMessage, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonAddCell, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(611, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.00654F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.99346F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(304, 239);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // tsmChangeUsers
            // 
            this.tsmChangeUsers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangeUser,
            this.tsmiAddUser});
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
            // управлениеСкладомToolStripMenuItem
            // 
            this.управлениеСкладомToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadMapImg,
            this.tsmiLoadMapData,
            this.tsmiSaveMapData,
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
            // tsmiLoadMapData
            // 
            this.tsmiLoadMapData.Image = global::WarehouseManagement.Properties.Resources.loadCells;
            this.tsmiLoadMapData.Name = "tsmiLoadMapData";
            this.tsmiLoadMapData.Size = new System.Drawing.Size(201, 22);
            this.tsmiLoadMapData.Text = "&Загрузить ячейки";
            this.tsmiLoadMapData.Click += new System.EventHandler(this.tsmiLoadMapData_Click);
            // 
            // tsmiSaveMapData
            // 
            this.tsmiSaveMapData.Image = global::WarehouseManagement.Properties.Resources.saveCells;
            this.tsmiSaveMapData.Name = "tsmiSaveMapData";
            this.tsmiSaveMapData.Size = new System.Drawing.Size(201, 22);
            this.tsmiSaveMapData.Text = "&Сохранить ячейки";
            this.tsmiSaveMapData.Click += new System.EventHandler(this.tsmiSaveMapData_Click);
            // 
            // tsmiOpenDB
            // 
            this.tsmiOpenDB.Image = global::WarehouseManagement.Properties.Resources.openDB;
            this.tsmiOpenDB.Name = "tsmiOpenDB";
            this.tsmiOpenDB.Size = new System.Drawing.Size(201, 22);
            this.tsmiOpenDB.Text = "Открыть базу данных";
            this.tsmiOpenDB.Click += new System.EventHandler(this.tsmiOpenDB_Click);
            // 
            // tsmiAddCategory
            // 
            this.tsmiAddCategory.Name = "tsmiAddCategory";
            this.tsmiAddCategory.Size = new System.Drawing.Size(188, 22);
            this.tsmiAddCategory.Text = "Добавить категорию";
            this.tsmiAddCategory.Click += new System.EventHandler(this.tsmiAddCategory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 465);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelWarehouse;
        private System.Windows.Forms.ListBox listBoxProducts;
        private System.Windows.Forms.Button buttonAddCell;
        private System.Windows.Forms.Button buttonEditingMap;
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
    }
}

