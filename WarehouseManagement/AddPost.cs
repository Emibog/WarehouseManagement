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
        string user;
        public AddPost(string user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string inputPost = textBoxNewPost.Text;

            //TODO: add validation
            if (!string.IsNullOrEmpty(inputPost))
            {
                DB db = new DB();
                db.openConnection();

                MySqlCommand autoIncrement = new MySqlCommand("ALTER TABLE `posts` AUTO_INCREMENT = 1", db.getConnection());
                MySqlCommand command = new MySqlCommand("SELECT * FROM `posts`", db.getConnection());

                autoIncrement.ExecuteNonQuery();
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
                    MySqlCommand autoIncrementPosts = new MySqlCommand("ALTER TABLE `posts` AUTO_INCREMENT = 1", db.getConnection());
                    MySqlCommand insertCommand = new MySqlCommand("INSERT INTO `posts` (`post`) VALUES (@post)", db.getConnection());
                    insertCommand.Parameters.AddWithValue("@post", inputPost);

                    autoIncrementPosts.ExecuteNonQuery();
                    insertCommand.ExecuteNonQuery();

                    // Запись в историю
                    MySqlCommand autoIncrementHistory = new MySqlCommand("ALTER TABLE `history` AUTO_INCREMENT = 1", db.getConnection());
                    MySqlCommand historyCommand = new MySqlCommand("INSERT INTO `history` (`operation`, `user`, `date`) VALUES (@operation, @user, NOW())", db.getConnection());
                    historyCommand.Parameters.AddWithValue("@operation", "Добавление должности: " + inputPost);
                    historyCommand.Parameters.AddWithValue("@user", user);

                    autoIncrementHistory.ExecuteNonQuery();
                    historyCommand.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Такая должность уже существует");
                }

                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Введите должность");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
