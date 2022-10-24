using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Choices
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string log;
        public string par;
        public string log1;
        public string par1;
        private static bool closing;
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            closing = true;
        }

        private void registrationClick(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            closing = false;
            Close();
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43P_ZK_Emissions;User=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select COUNT(*) from Users", connection);
                int n = Convert.ToInt32(command.ExecuteScalar().ToString());
                for (int i = 1; i <= n; i++)
                {
                    SqlCommand command1 = new SqlCommand("SELECT [Login] FROM [dbo].[Users] WHERE [ID_User] = " + i + "", connection);
                    log = command1.ExecuteScalar().ToString();
                    if (log == loginText.Text)
                    {
                        int h = i;
                        SqlCommand command2 = new SqlCommand("SELECT [Password] FROM [dbo].[Users] WHERE [ID_User] = " + h + "", connection);
                        par = command2.ExecuteScalar().ToString();
                        if (par == passwordText.Text)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                            closing = false;
                            break;

                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль");
                            break;
                        }
                    }
                    else
                    {
                    }
                }
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (closing)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
