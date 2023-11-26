
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
            this.panelWarehouse = new System.Windows.Forms.Panel();
            this.listBoxProducts = new System.Windows.Forms.ListBox();
            this.buttonAddCell = new System.Windows.Forms.Button();
            this.buttonEditingMap = new System.Windows.Forms.Button();
            this.panelScroll = new System.Windows.Forms.Panel();
            this.panelScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWarehouse
            // 
            this.panelWarehouse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelWarehouse.Location = new System.Drawing.Point(3, 0);
            this.panelWarehouse.Name = "panelWarehouse";
            this.panelWarehouse.Size = new System.Drawing.Size(288, 238);
            this.panelWarehouse.TabIndex = 0;
            // 
            // listBoxProducts
            // 
            this.listBoxProducts.FormattingEnabled = true;
            this.listBoxProducts.Location = new System.Drawing.Point(609, 29);
            this.listBoxProducts.Name = "listBoxProducts";
            this.listBoxProducts.Size = new System.Drawing.Size(202, 160);
            this.listBoxProducts.TabIndex = 1;
            // 
            // buttonAddCell
            // 
            this.buttonAddCell.Location = new System.Drawing.Point(609, 248);
            this.buttonAddCell.Name = "buttonAddCell";
            this.buttonAddCell.Size = new System.Drawing.Size(202, 23);
            this.buttonAddCell.TabIndex = 2;
            this.buttonAddCell.Text = "Добавить ячейку";
            this.buttonAddCell.UseVisualStyleBackColor = true;
            this.buttonAddCell.Click += new System.EventHandler(this.btnAddCell_Click);
            // 
            // buttonEditingMap
            // 
            this.buttonEditingMap.Location = new System.Drawing.Point(609, 206);
            this.buttonEditingMap.Name = "buttonEditingMap";
            this.buttonEditingMap.Size = new System.Drawing.Size(202, 23);
            this.buttonEditingMap.TabIndex = 3;
            this.buttonEditingMap.Text = "Редактировать карту";
            this.buttonEditingMap.UseVisualStyleBackColor = true;
            this.buttonEditingMap.Click += new System.EventHandler(this.btnEditingMap_Click);
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.panelWarehouse);
            this.panelScroll.Location = new System.Drawing.Point(31, 29);
            this.panelScroll.MaximumSize = new System.Drawing.Size(500, 300);
            this.panelScroll.MinimumSize = new System.Drawing.Size(500, 300);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(500, 300);
            this.panelScroll.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.buttonEditingMap);
            this.Controls.Add(this.buttonAddCell);
            this.Controls.Add(this.listBoxProducts);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление складом";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelScroll.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWarehouse;
        private System.Windows.Forms.ListBox listBoxProducts;
        private System.Windows.Forms.Button buttonAddCell;
        private System.Windows.Forms.Button buttonEditingMap;
        private System.Windows.Forms.Panel panelScroll;
    }
}

