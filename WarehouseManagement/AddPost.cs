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
    public partial class AddPost : Form
    {
        string newPost;
        public AddPost()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string inputPost = textBoxNewPost.Text;

            DB db = new DB();
            db.openConnection();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `posts`", db.getConnection());
            MySqlDataReader reader = command.ExecuteReader();

            bool postExists = false;

            while (reader.Read())
            {
                string post = reader["post"].ToString();
                if (post == inputPost)
                {
                    postExists = true;
                    break;
                }
            }

            reader.Close();

            if (!postExists)
            {
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `posts` (`post`) VALUES (@post)", db.getConnection());
                insertCommand.Parameters.AddWithValue("@post", inputPost);
                insertCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Такая должность уже существует");
            }

            db.closeConnection();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
