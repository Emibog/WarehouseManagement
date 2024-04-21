using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=warehousemanagement");

        /// <summary>
        /// Открывает соединение с базой данных
        /// </summary>
        /// <returns></returns>
        public bool openConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    return true; // Возвращаем true в случае успешного открытия соединения
                }
                return false; // Возвращаем false, если соединение уже было открыто
            }
            catch (Exception ex)
            {
                return false; // Возвращаем false в случае ошибки
            }
        }

        /// <summary>
        /// Закрывает соединение с базой данных
        /// </summary>
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Получает соединение с базой данных
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
