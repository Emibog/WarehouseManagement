
namespace WarehouseManagement
{
    partial class DBView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowserDB = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserDB
            // 
            this.webBrowserDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDB.Location = new System.Drawing.Point(0, 0);
            this.webBrowserDB.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDB.Name = "webBrowserDB";
            this.webBrowserDB.Size = new System.Drawing.Size(1084, 511);
            this.webBrowserDB.TabIndex = 0;
            this.webBrowserDB.Url = new System.Uri("http://localhost/phpMyAdmin/index.php?route=/database/structure&server=1&db=wareh" +
        "ousemanagement", System.UriKind.Absolute);
            // 
            // DBView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 511);
            this.Controls.Add(this.webBrowserDB);
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "DBView";
            this.Text = "DBView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserDB;
    }
}