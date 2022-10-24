using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Choices
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private static bool closing;
        public Registration()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            closing = true;
        }
        public string regLogin;
        public string regParol;
        private void registrationClick(object sender, RoutedEventArgs e)
        {
            regLogin = loginText.Text;
            regParol = passwordText.Text;
            if (regLogin != "")
            {
                if (regParol != "")
                {
                    DataTable dt = Select("insert into Users (Login, Password) values ('" + regLogin + "','" + regParol + "');");
                    Login login = new Login();
                    login.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Заполните поле пароля");
                }
            }
            else
            {
                MessageBox.Show("Заполните поле логина");
            }
            closing = false;
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
            Login login = new Login();
            login.Show();
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
