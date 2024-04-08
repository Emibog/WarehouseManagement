using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagement
{
    [TestClass]
    public class MainFormTests
    {
        private MainForm mainForm;

        [TestInitialize]
        public void TestInitialize()
        {
            // Инициализация формы перед каждым тестом
            mainForm = new MainForm("Администратор", "log");
            mainForm.Show(); // Показываем форму, чтобы контролы были инициализированы
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Закрытие формы после каждого теста
            mainForm.Close();
        }
        
        [TestMethod]
        public void btnAddCell_Click_ShouldAddStorageCell()
        {
            // Arrange
            int initialStorageCellCount = mainForm.storageCells.Count;

            // Act
            mainForm.btnAddCell_Click(null, null);

            // Assert
            Assert.AreEqual(initialStorageCellCount + 1, mainForm.storageCells.Count);

            // Проверка, что новая ячейка была добавлена визуально
            Button addedButton = mainForm.Controls.Find("Cell " + (initialStorageCellCount + 1), true).FirstOrDefault() as Button;
            Assert.IsNotNull(addedButton);
        }
        
        [TestMethod]
        public void menuDeleteClick_ShouldRemoveStorageCell()
        {
            // Arrange
            mainForm.btnAddCell_Click(null, null); // Добавляем ячейку
            int initialStorageCellCount = mainForm.storageCells.Count;

            // Act
            mainForm.menuDeleteClick(mainForm.cmsButtonDelete.Items[0], null);

            // Assert
            Assert.AreEqual(initialStorageCellCount - 1, mainForm.storageCells.Count);
            // Проверка, что ячейка была удалена визуально
            Button deletedButton = mainForm.Controls.Find("Cell " + (initialStorageCellCount), true).FirstOrDefault() as Button;
            Assert.IsNull(deletedButton);
        }

        [TestMethod]
        public void btnOK_Click_ShouldSetNewLoginAndNewPass()
        {
            // Arrange
            var formAddUser = new formAddUser();
            formAddUser.TextBoxNewLogin = "TestLogin";
            formAddUser.TextBoxNewPass = "TestPass";

            // Act
            formAddUser.btnOK_Click(null, EventArgs.Empty);

            // Assert
            Assert.AreEqual("TestLogin", formAddUser.newLogin);
            Assert.AreEqual("TestPass", formAddUser.newPass);
        }

        [TestMethod]
        public void btnOK_Click_ShouldNotSetNewLoginAndNewPassIfInvalid()
        {
            // Arrange
            var formAddUser = new formAddUser();
            formAddUser.TextBoxNewLogin = ""; // Пустое значение, что считается недопустимым
            formAddUser.TextBoxNewPass = "TestPass";

            // Act
            formAddUser.btnOK_Click(null, EventArgs.Empty);

            // Assert
            Assert.IsNull(formAddUser.newLogin);
            Assert.IsNull(formAddUser.newPass);
        }

        [TestMethod]
        public void btnCancel_Click_ShouldNotSetNewLoginAndNewPass()
        {
            // Arrange
            var formAddUser = new formAddUser();
            formAddUser.TextBoxNewLogin = ""; // Пустое значение, что считается недопустимым
            formAddUser.TextBoxNewPass = "";

            // Act
            formAddUser.btnOK_Click(null, EventArgs.Empty);

            // Assert
            Assert.IsNull(formAddUser.newLogin);
            Assert.IsNull(formAddUser.newPass);
        }

    }

}
