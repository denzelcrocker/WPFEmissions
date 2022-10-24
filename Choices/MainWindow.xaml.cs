using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Choices
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool closing;
        public MainWindow()
        {
            InitializeComponent();
            closing = true;
            using (SqlConnection connection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43P_ZK_Emissions;User=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand commandd = new SqlCommand("select max(ID_Source) from Sourse", connection);
                int n = Convert.ToInt32(commandd.ExecuteScalar().ToString());
                string[] name = new string[n];
                string[] adress = new string[n];

                for (int i = 1; i <= n; i++)
                {
                    SqlCommand commandd1 = new SqlCommand("select Name FROM Sourse WHERE ID_Source=" + i + "", connection);
                    if (commandd1.ExecuteScalar() is null)
                    {

                    }
                    else
                    {

                        name[i - 1] = Convert.ToString(commandd1.ExecuteScalar().ToString());
                        SqlCommand commandd2 = new SqlCommand("select Address FROM Sourse WHERE ID_Source=" + i + "", connection);
                        adress[i - 1] = Convert.ToString(commandd2.ExecuteScalar().ToString());
                    }
                }
                List<Istochniki> istochnikList = new List<Istochniki>
                {

                };
                for (int i = 1; i <= n; i++)
                {
                    if (name[i - 1] == null)
                    {

                    }
                    else
                    {
                        istochnikList.Add(new Istochniki { ID_Souce = i, Name = name[i - 1], Adress = adress[i - 1] });
                    }
                }
                wind1.ItemsSource = istochnikList;
            }

        
            using (SqlConnection connection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43P_ZK_Emissions;User=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select max(ID_Emission) from Emission", connection);
                int n = Convert.ToInt32(command.ExecuteScalar().ToString());
                int[] id = new int[n];
                float[] count = new float[n];
                string[] text = new string[n];
                string[] Date = new string[n];

                for (int i = 1; i <= n; i++)
                {
                    SqlCommand command1 = new SqlCommand("select ID_Source FROM Emission WHERE ID_Emission=" + i + "", connection);
                    if (command1.ExecuteScalar() is null)
                    {

                    }
                    else
                    {
                        id[i - 1] = Convert.ToInt32(command1.ExecuteScalar().ToString());
                        SqlCommand command2 = new SqlCommand("select Count FROM Emission WHERE ID_Emission=" + i + "", connection);
                        count[i - 1] = float.Parse(command2.ExecuteScalar().ToString());
                        SqlCommand command3 = new SqlCommand("select Text FROM Emission WHERE ID_Emission=" + i + "", connection);
                        text[i - 1] = Convert.ToString(command3.ExecuteScalar().ToString());
                        SqlCommand command4 = new SqlCommand("select date FROM Emission WHERE ID_Emission=" + i + "", connection);
                        Date[i - 1] = Convert.ToString(command4.ExecuteScalar().ToString());
                    }
                }
                List<Vibros> vibrosList = new List<Vibros>
                {

                };
                for (int i = 1; i <= n; i++)
                {
                    if (Date[i - 1] == null)
                    {

                    }
                    else
                    {
                        vibrosList.Add(new Vibros { ID_Emission = i, ID_Souce = id[i - 1], Count = count[i - 1], Text = text[i - 1], date = Date[i - 1] });
                    }
                }

                wind2.ItemsSource = vibrosList;
            }
        }

        private void addSourceClick(object sender, RoutedEventArgs e)
        {
            AddSource AddS = new AddSource();
            AddS.Show();
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

        private void addEmissionClick(object sender, RoutedEventArgs e)
        {
            AddEmissions AddE = new AddEmissions();
            AddE.Show();
            closing = false;
            Close();
        }

        private void deleteSourceClick(object sender, RoutedEventArgs e)
        {
            DeleteSources DeleteS = new DeleteSources();
            DeleteS.Show();
            closing = false;
            Close();
        }

        private void deleteEmissionClick(object sender, RoutedEventArgs e)
        {
            DeleteEmissions DeleteE = new DeleteEmissions();
            DeleteE.Show();
            closing = false;
            Close();
        }

        private void editSourceClick(object sender, RoutedEventArgs e)
        {
            EditSources EditS = new EditSources();
            EditS.Show();
            closing = false;
            Close();
        }

        private void editEmissionClick(object sender, RoutedEventArgs e)
        {
            EditEmissions EditE = new EditEmissions();
            EditE.Show();
            closing = false;
            Close();
        }

        private void avgEmissions(object sender, RoutedEventArgs e)
        {
            AvgEmissions avgEmissions = new AvgEmissions();
            avgEmissions.Show();
            closing = false;
            Close();
        }

        private void minEmissions(object sender, RoutedEventArgs e)
        {
            MinEmissions minEmissions = new MinEmissions();
            minEmissions.Show();
            closing = false;
            Close();
        }

        private void maxEmissions(object sender, RoutedEventArgs e)
        {
            MaxEmissions maxEmissions = new MaxEmissions();
            maxEmissions.Show();
            closing = false;
            Close();
        }
        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase"); // создаём таблицу в приложении
                                                             // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=43p_rad_Sor_Man;User=33П;PWD=12357");
            sqlConnection.Open(); // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand(); // создаём команду
            sqlCommand.CommandText = selectSQL; // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close(); // возращаем таблицу с результатом
            return dataTable;
        }
        internal class Vibros
        {
            public int ID_Emission { get; set; }
            public int ID_Souce { get; set; }
            public float Count { get; set; }
            public string Text { get; set; }
            public string date { get; set; }

        }
        internal class Istochniki
        {
            public int ID_Souce { get; set; }
            public string Name { get; set; }
            public string Adress { get; set; }

        }
    }
}