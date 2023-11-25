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

        public InputDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EnteredText = textBoxInput.Text;
        }

        private void InputDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK && string.IsNullOrEmpty(textBoxInput.Text))
            {
                MessageBox.Show("Номер ячейки не введен!");
                e.Cancel = true; // Отменить закрытие формы
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
