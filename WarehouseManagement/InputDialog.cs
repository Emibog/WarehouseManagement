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
    public partial class InputDialog : Form
    {
        public string EnteredText { get; private set; }
        public Color EnteredColor { get; private set; }
        private string color;

        public InputDialog()
        {
            InitializeComponent();
            comboBoxColor.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EnteredText = textBoxName.Text;
            color = comboBoxColor.SelectedItem.ToString();
            EnteredColor = Color.FromName(color);
        }

        private void InputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Номер ячейки не введен!");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            comboBoxColor.SelectedIndex = 0;
        }
    }
}
