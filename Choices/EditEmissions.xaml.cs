using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Choices
{
    /// <summary>
    /// Логика взаимодействия для EditEmissions.xaml
    /// </summary>
    public partial class EditEmissions : Window
    {
        private static bool closing;
        public EditEmissions()
        {
            InitializeComponent();
            closing = true;
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            closing = false;
            Close();
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            int nomer_vib = Convert.ToInt32(idEmission.Text);
            int nomer_ist = Convert.ToInt32(numberEmission.Text);
            int kol_ist = Convert.ToInt32(countEmission.Text);
            string com_ist = comEmission.Text;
            string date_ist_ist = dateEmission.Text;
            DataTable dt1 = Select("update Emission set ID_Source = " + nomer_ist + ", Count = " + kol_ist + ", Text='" + com_ist + "', date='" + date_ist_ist + "' where  ID_Emission =" + nomer_vib + "");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            closing = false;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (closing)
            {
                Application.Current.Shutdown();
            }
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase"); // создаём таблицу в приложении
                                                             // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43P_ZK_Emissions;User=33П;PWD=12357");
            sqlConnection.Open(); // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand(); // создаём команду
            sqlCommand.CommandText = selectSQL; // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close(); // возращаем таблицу с результатом
            return dataTable;
        }
    }
}
