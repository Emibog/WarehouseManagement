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
    public partial class NewUser : Form
    {
        public string newLogin { get; private set; }
        public string newPass { get; private set; }

        public NewUser()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            newLogin = textBoxNewLogin.Text;
            newPass = textBoxNewPass.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
