using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projekt_WPF_Solution.DataBaseClasses;
using Projekt_WPF_Solution.Commands;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace Projekt_WPF_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListCollectionView carsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Cars); } }
        private ListCollectionView clientsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Clients); } }
        private ListCollectionView rentsView { get { return (ListCollectionView)CollectionViewSource.GetDefaultView(SqlDataGetters.Rents); } }

        public MainWindow()
        {
            InitializeComponent();
            SqlDataGetters.GetAll();
            CarListBox.ItemsSource = SqlDataGetters.Cars;
            ClientListBox.ItemsSource = SqlDataGetters.Clients;
            RentListBox.ItemsSource = SqlDataGetters.Rents;

            Loaded += delegate
            {
                MyCommands.BindCommands(this);
            };
        }

        private void ContactMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            contactWindow.ShowDialog();
        }

        #region AddButtons
        private void AddNewCar_Click(object sender, RoutedEventArgs e)
        {
            Car newCar = new Car();
            CarWindow newCarWindow = new CarWindow(newCar);
            if (newCarWindow.ShowDialog() == true)
            {
                if (!newCar.SqlInsert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }
        private void AddNewClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client newClient = new Client();
            ClientWindow newClientWindow = new ClientWindow(newClient);
            if (newClientWindow.ShowDialog() == true)
            {
                if (!newClient.SqlInsert())
                {
                    MessageBox.Show("BŁĄD DODAWANIA");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }
        private void AddNewRentalButton_Click(object sender, RoutedEventArgs e)
        {
            Rent newRent = new Rent();
            RentalWindow newRenatlWindow = new RentalWindow(newRent);
            if(newRenatlWindow.ShowDialog() == true)
            {
                if(!newRent.SqlInsert())
                {
                    MessageBox.Show("Błąd dodawania");
                }
                else
                {
                    SqlDataGetters.GetAll();
                }
            }
        }

        #endregion



        private void SearchClientButton_Click(object sender, RoutedEventArgs e)
        {
            SearchClientWindow searchClientWindow = new SearchClientWindow();
            searchClientWindow.ShowDialog();
        }
        
    }
}
