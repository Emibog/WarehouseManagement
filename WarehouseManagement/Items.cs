using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagement
{
    public partial class Items : Form
    {
        public List<string> Products;
        public List<int> Amount;
        public string mapName;
        public string cellName;

        public Items(List<string> products, List<int> amount, string mapName, string cellName)
        {
            InitializeComponent();
            Products = products;
            Amount = amount;
            showProducts();
            this.mapName = mapName;
            this.cellName = cellName;
            Text = "Товары в ячейке " + cellName;
        }

        private void showProducts()
        {
            int yOffset = 20; // Инициализировать смещение Y для позиционирования
            Font labelFont = new Font("Arial", 12, FontStyle.Regular);
            foreach (string product in Products)
            {
                // Создать новую метку для каждого продукта
                Label label = new Label();
                label.Name = product;
                label.Font = labelFont;
                label.Text = product + " - " + Amount[Products.IndexOf(product)];
                label.Location = new Point(20, yOffset); // Установить позицию метки
                yOffset += 50; // Увеличить смещение Y для следующей метки
                panelItems.Controls.Add(label);

                // Создать новую кнопку для удаления метки
                Button deleteButton = new Button();
                deleteButton.BackgroundImage = global::WarehouseManagement.Properties.Resources.deleteButton;
                deleteButton.BackgroundImageLayout = ImageLayout.Zoom; // Растянуть изображение для заполнения кнопки
                deleteButton.Width = 40;
                deleteButton.Height = 40;
                deleteButton.Tag = label; // Отметить кнопку соответствующей меткой
                deleteButton.Location = new Point(120, yOffset - 60); // Разместить кнопку удаления рядом с меткой
                deleteButton.Click += DeleteButton_Click; // Присоединить обработчик события нажатия
                panelItems.Controls.Add(deleteButton); // Добавить кнопку удаления на форму
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button deleteButton = (Button)sender;
            Label correspondingLabel = (Label)deleteButton.Tag;
            int yOffset = correspondingLabel.Location.Y;
            formDeleteItem fDeleteItem = new formDeleteItem(mapName);
            if (fDeleteItem.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(fDeleteItem.ItemToDelete);
            }
            // Удалить метку, связанную с кнопкой удаления
            panelItems.Controls.Remove(correspondingLabel);

            // Удалить саму кнопку удаления
            panelItems.Controls.Remove(deleteButton);

            // Сдвинуть нижние элементы вверх, чтобы заполнить пробел
            foreach (Control control in panelItems.Controls)
            {
                if (control.Location.Y > yOffset)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y - 50);
                }
            }
        }
    }
}