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
using static Choices.MainWindow;

namespace Choices
{
    /// <summary>
    /// Логика взаимодействия для AvgEmissions.xaml
    /// </summary>
    public partial class AvgEmissions : Window
    {
        private static bool closing;
        public AvgEmissions()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            closing = true;
            using (SqlConnection connection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43P_ZK_Emissions;User=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select avg(Count) from Emission", connection);
                string nn = command.ExecuteScalar().ToString();
                float n = float.Parse(nn);
                n = Convert.ToInt32(Math.Round(n, 1));
                int id = 0;
                int id1 = 0;
                string text = "";
                string Date = "";
                SqlCommand commandd1 = new SqlCommand("select ID_Emission FROM Emission WHERE Count=" + n + "", connection);
                if (commandd1.ExecuteScalar() is null)
                {

                }
                else
                {
                    SqlCommand command1 = new SqlCommand("select ID_Emission FROM Emission WHERE Count=" + n + "", connection);
                    id1 = Convert.ToInt32(command1.ExecuteScalar().ToString());
                    SqlCommand command2 = new SqlCommand("select ID_Source FROM Emission WHERE Count=" + n + "", connection);
                    id = Convert.ToInt32(command2.ExecuteScalar().ToString());
                    SqlCommand command3 = new SqlCommand("select Text FROM Emission WHERE Count=" + n + "", connection);
                    text = Convert.ToString(command3.ExecuteScalar().ToString());
                    SqlCommand command4 = new SqlCommand("select Date FROM Emission WHERE Count=" + n + "", connection);
                    Date = Convert.ToString(command4.ExecuteScalar().ToString());
                }
                List<Vibros> vibrosList = new List<Vibros>
                {

                };
                vibrosList.Add(new Vibros { ID_Emission = id1, ID_Souce = id, Count = n, Text = text, date = Date });
                avgEmissions.ItemsSource = vibrosList;
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
        private void backClick(object sender, RoutedEventArgs e)
        {
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
    }
}
