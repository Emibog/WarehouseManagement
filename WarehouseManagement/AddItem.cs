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
    public partial class formAddItem : Form
    {
        public string EnteredItemName { get; set; }
        public string EnteredCategory { get; set; }
        public string EnteredCell { get; set; }
        public string EnteredAmount { get; set; }

        public formAddItem()
        {
            InitializeComponent();
            comboBoxCells.Items.Add("ав");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            EnteredItemName = textBoxItemName.Text;
            EnteredCategory = textBoxCategory.Text;
            EnteredCell = comboBoxCells.SelectedItem.ToString();
            EnteredAmount = textBoxAmount.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
